using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Collections.ObjectModel;

namespace Common
{
    public class NpoiExcelHelper
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        public static void ToExcel<T>(ObservableCollection<T> dataSource, string path, List<string> attries, List<string> headers)
        {
            if (dataSource == null || !dataSource.Any())
            {
                throw new Exception("dataSource is null.");
            }

            HSSFWorkbook wb = new HSSFWorkbook();//创建一个工作薄
            HSSFSheet sheet = wb.CreateSheet() as HSSFSheet;//在工作薄中创建一个工作表
            HSSFRow rw = sheet.CreateRow(0) as HSSFRow;
            var patriarch = sheet.CreateDrawingPatriarch();
            for (int i = 0; i < headers.Count; i++) //循环一个表头来创建第一行的表头
            {
                rw.CreateCell(i).SetCellValue(headers[i]);
            }
            Type t = typeof(T); //获取得泛型集合中的实体， 返回T的类型
            PropertyInfo[] properties = t.GetProperties(); //返回当前获得实体后 实体类型中的所有公共属性
            for (int i = 0; i < dataSource.Count; i++)//循环实体泛型集合
            {
                rw = sheet.CreateRow(i + 1) as HSSFRow;//创建一个新行，把传入集合中的每条数据创建一行
                foreach (PropertyInfo property in properties)//循环得到的所有属性（想要把里面指定的属性值导出到Excel文件中）
                {
                    for (int j = 0; j < attries.Count; j++)//循环需要导出属性值 的 属性名
                    {
                        string attry = attries[j];//获得一个需要导入的属性名；
                        if (string.Compare(property.Name.ToUpper(), attry.ToUpper()) == 0)//如果需要导出的属性名和当前循环实体的属性名一样，
                        {
                            object objValue = property.GetValue(dataSource[i], null);//获取当前循环的实体属性在当前实体对象（arr[i]）的值
                            if (objValue != null && (objValue.GetType().Name == "Bitmap" || objValue.GetType().Name == "Image"))
                            {
                                //- 插入图片到 Excel，并返回一个图片的标识   
                                var handle = (objValue as Bitmap).GetHbitmap();
                                using (Bitmap newBmp = Image.FromHbitmap(handle))
                                {
                                    MemoryStream ms = new MemoryStream();
                                    newBmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    byte[] bytes = ms.GetBuffer();
                                    ms.Close();
                                    var pictureIdx = wb.AddPicture(bytes, PictureType.JPEG);
                                    //- 创建图片的位置                      
                                    var anchor = new HSSFClientAnchor(
                                        0, 0,//- 上左 到 上右 的位置，是基于下面的行列位置   
                                        0, 0, //- 下左 到 下右 的位置，是基于下面的行列位置      
                                        j, i + 1,
                                        j + 1, i + 2);
                                    //- 图片输出的位置这么计算的： 
                                    //- 假设我们要将图片放置于第 5(E) 列的第 2 行  
                                    //- 对应索引为是 4 : 1 （默认位置）         
                                    //- 放置的位置就等于（默认位置）到（默认位置各自加上一行、一列）    
                                    var pic = patriarch.CreatePicture(anchor, pictureIdx);//- 使用绘画器绘画图片

                                    sheet.SetColumnWidth(j, 100 * 36);
                                    rw.HeightInPoints = 100 * 0.75f;
                                    bytes = null;

                                }
                                DeleteObject(handle);
                            }
                            else
                            {
                                rw.CreateCell(j).SetCellValue((objValue == null) ? string.Empty : objValue.ToString());//创建单元格并进行赋值
                                sheet.AutoSizeColumn(j);
                            }
                        }
                    }
                }
            }
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                wb.Write(fs);
            }
        }
    }
}
