using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Common
{
    public static class ImageHelper
    {
        public static BitmapImage GetURLImage(string url)
        {
            BitmapImage img = GetURLImage(new Uri(url));
            return img;
        }
        public static BitmapImage GetURLImage(Uri url)
        {
            BitmapImage img = new BitmapImage(url);
            return img;
        }
        public static Bitmap GetURLImageToBitmap(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            try
            {
                System.Net.WebRequest webreq = System.Net.WebRequest.Create(url);
                webreq.Timeout = 1000 * 10;
                System.Net.WebResponse webres = webreq.GetResponse();
                using (System.IO.Stream stream = webres.GetResponseStream())
                {
                    Bitmap tmpImg = new Bitmap(stream);
                    var img = new Bitmap(tmpImg);
                    stream.Close();
                    tmpImg.Dispose();
                    return img;
                }
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// 选择图片
        /// </summary>
        /// <returns>图片地址</returns>
        public static string SelectImage()
        {
            string filename = "";
            try
            {
                using (var ofd = new System.Windows.Forms.OpenFileDialog())
                {
                    ofd.Filter = "图像文件(JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif; *.tiff; *.png| JPeg 图像文件(*.jpg;*.jpeg)"
                                 +
                                 "|*.jpg;*.jpeg |GIF 图像文件(*.gif)|*.gif |BMP图像文件(*.bmp)|*.bmp|Tiff图像文件(*.tif;*.tiff)|*.tif;*.tiff|Png图像文件(*.png)"
                                 + "| *.png |所有文件(*.*)|*.*";

                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        filename = ofd.FileName;
                    }

                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.WriteErrorLog(typeof(ImageHelper), ex);
            }
            return filename;
        }
        /// <summary>
        /// 保存图片到本地
        /// </summary>
        /// <param name="image">保存的图片对象</param>
        /// <returns></returns>
        public static bool SaveImage(Bitmap image)
        {
            try
            {
                using (var saveImageDialog = new System.Windows.Forms.SaveFileDialog())
                {
                    saveImageDialog.Title = "图片保存";
                    saveImageDialog.Filter = @"jpeg|*.jpg|bmp|*.bmp|png|*.png";

                    if (saveImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string fileName = saveImageDialog.FileName.ToString();

                        if (fileName != "" && fileName != null)
                        {
                            string fileExtName = fileName.Substring(fileName.LastIndexOf(".") + 1).ToString();
                            System.Drawing.Imaging.ImageFormat imgformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                            if (fileExtName != "")
                            {
                                switch (fileExtName)
                                {
                                    case "jpg":
                                        imgformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                                        break;
                                    case "bmp":
                                        imgformat = System.Drawing.Imaging.ImageFormat.Bmp;
                                        break;
                                    case "png":
                                        imgformat = System.Drawing.Imaging.ImageFormat.Png;
                                        break;
                                }

                            }
                            Bitmap newImage = new Bitmap(image);//克隆复制新的图片对象
                            newImage.Save(fileName, imgformat);
                            newImage.Dispose();

                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.WriteErrorLog(typeof(ImageHelper), ex);
            }
            return false;
        }
        /// <summary>
        /// 选择图片文件夹
        /// </summary>
        /// <returns></returns>
        public static string[] SelectImageFolder(out string path)
        {
            List<string> fileList = new List<string>();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.SelectedPath;
                fileList.AddRange(Directory.GetFiles(path, "*.jpg", SearchOption.TopDirectoryOnly));
                fileList.AddRange(Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly));
                fileList.AddRange(Directory.GetFiles(path, "*.jpeg", SearchOption.TopDirectoryOnly));
                fileList.AddRange(Directory.GetFiles(path, "*.bmp", SearchOption.TopDirectoryOnly));
            }
            else
            {
                path = null;
                return null;
            }
            return fileList.ToArray();
        }

        /// <summary>
        /// 通过路径获取图片
        /// </summary>
        /// <returns>图片</returns>
        public static Bitmap GetImage(string imagePath)
        {
            Bitmap img = null;
            //try
            //{
            if (File.Exists(imagePath))
            {
                using (var fs = File.Open(imagePath, FileMode.Open))
                {
                    Bitmap tmpImg = new Bitmap(fs);
                    img = new Bitmap(tmpImg);
                    fs.Close();
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    Log.LogHelper.WriteErrorLog(typeof(ImageHelper), ex);
            //}
            return img;
        }

        /// <summary>
        /// Bitmap转BitmapImage
        /// </summary>
        /// <returns></returns>
        public static BitmapImage BitmapToBitmapImage(Bitmap img)
        {
            BitmapImage bitImg = null;
            try
            {
                if (img != null)
                {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] bytes = ms.ToArray();
                        bitImg = new BitmapImage();
                        bitImg.BeginInit();
                        bitImg.StreamSource = new MemoryStream(bytes);
                        bitImg.CacheOption = BitmapCacheOption.OnLoad;
                        bitImg.EndInit();
                        bitImg.Freeze();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.WriteErrorLog(typeof(ImageHelper), ex);
            }
            return bitImg;
        }

        /// <summary>
        /// BitmapImage转Bitmap
        /// </summary>
        /// <returns></returns>
        public static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            Bitmap img = null;
            try
            {
                if (bitmapImage != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(ms);

                    img = new Bitmap(ms);
                    ms.Close();
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.WriteErrorLog(typeof(ImageHelper), ex);
            }
            return img;
        }

        /// <summary>
        /// 通过路径获取图片
        /// </summary>
        /// <returns>WPF UI图片</returns>
        public static BitmapImage GetBitmapImage(string imagePath)
        {
            BitmapImage bitImg = null;
            try
            {
                if (File.Exists(imagePath))
                {
                    bitImg = new BitmapImage();
                    bitImg.BeginInit();
                    bitImg.CacheOption = BitmapCacheOption.OnLoad;

                    using (Stream ms = new MemoryStream(File.ReadAllBytes(imagePath)))
                    {
                        bitImg.StreamSource = ms;
                        bitImg.EndInit();
                        bitImg.Freeze();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.WriteErrorLog(typeof(ImageHelper), ex);
            }
            return bitImg;
        }

        /// <summary>
        /// Bitmap转Base64
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string BitmapToBase64(Bitmap img)
        {
            try
            {
                return BytesToBase64(BitmapToBytes(img));
            }
            catch (Exception ex)
            {
                Log.LogHelper.WriteErrorLog(typeof(ImageHelper), ex);
            }
            return null;
        }

        /// <summary>
        /// Bitmap转byte[]
        /// </summary>
        /// <param name="img"></param>
        /// <returns>byte[]</returns>
        public static byte[] BitmapToBytes(Bitmap img)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Png);
                    var buffer = ms.ToArray();
                    return buffer;
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.WriteErrorLog(typeof(ImageHelper), ex);
            }
            return null;
        }

        /// <summary>
        /// byte[]转Base64
        /// </summary>
        /// <param name="imgBytes"></param>
        /// <returns>string</returns>
        public static string BytesToBase64(byte[] imgBytes)
        {
            try
            {
                if (imgBytes != null)
                {
                    return Convert.ToBase64String(imgBytes);
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.WriteErrorLog(typeof(ImageHelper), ex);
            }
            return null;
        }

        /// <summary>
        /// Base64转byte[]
        /// </summary>
        /// <param name="imgBase64"></param>
        /// <returns>byte[]</returns>
        public static byte[] Base64ToBytes(string imgBase64)
        {
            try
            {
                if (!String.IsNullOrEmpty(imgBase64))
                {
                    return Convert.FromBase64String(imgBase64);
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.WriteErrorLog(typeof(ImageHelper), ex);
            }
            return null;
        }

        /// <summary>
        /// Base64转Bitmap
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Bitmap Base64ToBitmap(string imgBase64)
        {
            try
            {
                return BytesToBitmap(Base64ToBytes(imgBase64));
            }
            catch (Exception ex)
            {
                Log.LogHelper.WriteErrorLog(typeof(ImageHelper), ex);
            }
            return null;
        }

        /// <summary>
        /// byte[]转Bitmap
        /// </summary>
        /// <param name="img"></param>
        /// <returns>byte[]</returns>
        public static Bitmap BytesToBitmap(byte[] imgBytes)
        {
            try
            {
                using (var ms = new MemoryStream(imgBytes))
                {
                    using (Bitmap img = new Bitmap(ms))
                    {
                        Bitmap img2 = new Bitmap(img);
                        return img2;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.WriteErrorLog(typeof(ImageHelper), ex);
            }
            return null;
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="filePath"></param>
        public static void Save(BitmapSource image, string filePath)
        {
            PngBitmapEncoder PBE = new PngBitmapEncoder();
            PBE.Frames.Add(BitmapFrame.Create(image));
            using (System.IO.Stream stream = System.IO.File.Create(filePath))
            {
                PBE.Save(stream);
            }
        }
        /// <summary>
        /// 锁定边缘，缩放图片
        /// </summary>
        public static Bitmap MakeZoom(Bitmap img, int width, int height, int left, int top, int right, int bottom)
        {
            Bitmap zoomImg = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(zoomImg);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //绘制左上角
            g.DrawImage(img, new Rectangle(0, 0, left, top), 0, 0, left, top, GraphicsUnit.Pixel);
            //绘制上边
            g.DrawImage(img, new Rectangle(left, 0, width - left - right, top), left, 0, img.Width - left - right, top, GraphicsUnit.Pixel);
            //绘制右上角
            g.DrawImage(img, new Rectangle(width - right, 0, right, top), img.Width - right, 0, right, top, GraphicsUnit.Pixel);
            //绘制左边
            g.DrawImage(img, new Rectangle(0, top, left, height - top - bottom), 0, top, left, img.Height - top - bottom, GraphicsUnit.Pixel);
            //绘制中间
            g.DrawImage(img, new Rectangle(left, top, width - left - right, height - top - bottom), left, top, img.Width - left - right, img.Height - top - bottom, GraphicsUnit.Pixel);
            //绘制右边
            g.DrawImage(img, new Rectangle(width - right, top, right, height - top - bottom), img.Width - right, top, right, img.Height - top - bottom, GraphicsUnit.Pixel);
            //绘制左下角
            g.DrawImage(img, new Rectangle(0, height - bottom, left, bottom), 0, img.Height - bottom, left, bottom, GraphicsUnit.Pixel);
            //绘制下边
            g.DrawImage(img, new Rectangle(left, height - bottom, width - left - right, bottom), left, img.Height - bottom, img.Width - left - right, bottom, GraphicsUnit.Pixel);
            //绘制右下角
            g.DrawImage(img, new Rectangle(width - right, height - bottom, right, bottom), img.Width - right, img.Height - bottom, right, bottom, GraphicsUnit.Pixel);
            g.Dispose();
            return zoomImg;
        }


        /// <summary>
        /// 按比例缩放图片（返回图片与参数宽高一致，外围补黑边）
        /// </summary>
        /// <param name="image"></param>
        /// <param name="wndWidth"></param>
        /// <param name="wndHeigth"></param>
        /// <returns></returns>
        public static Bitmap BitmapResize(Bitmap image, int width, int height)
        {
            int nSourceWidth = image.Width;
            int nSourceHeigth = image.Height;

            float fDestWidth, fDestHight;

            if (nSourceWidth < width && nSourceHeigth < height)
            {
                fDestWidth = nSourceWidth;
                fDestHight = nSourceHeigth;
            }


            float fRatio = (float)(nSourceHeigth * 1.0 / nSourceWidth);
            if (fRatio * width < height)
            {
                fDestWidth = (float)width;
                fDestHight = fDestWidth * fRatio;
            }
            else
            {
                fDestHight = (float)height;
                fDestWidth = fDestHight / fRatio;
            }

            int sW = (int)fDestWidth;
            int sH = (int)fDestHight;

            Bitmap outBmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, new Rectangle((width - sW) / 2, (height - sH) / 2, sW, sH), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量     
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            image.Dispose();

            return outBmp;
        }

        /// <summary>
        /// 按比例缩放图片（不留黑边）
        /// </summary>
        /// <param name="image"></param>
        /// <param name="wndWidth"></param>
        /// <param name="wndHeigth"></param>
        /// <returns></returns>
        public static Bitmap BitmapResize2(Bitmap image, int width, int height)
        {
            int nSourceWidth = image.Width;
            int nSourceHeigth = image.Height;

            float fDestWidth, fDestHight;

            if (nSourceWidth < width && nSourceHeigth < height)
            {
                fDestWidth = nSourceWidth;
                fDestHight = nSourceHeigth;
            }


            float fRatio = (float)(nSourceHeigth * 1.0 / nSourceWidth);
            if (fRatio * width < height)
            {
                fDestWidth = (float)width;
                fDestHight = fDestWidth * fRatio;
            }
            else
            {
                fDestHight = (float)height;
                fDestWidth = fDestHight / fRatio;
            }

            int sW = (int)fDestWidth;
            int sH = (int)fDestHight;

            Bitmap outBmp = new Bitmap(sW, sH);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, new Rectangle(0, 0, sW, sH), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量     
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            image.Dispose();

            return outBmp;
        }

        /// <summary>
        /// 按比例缩放图片（返回图片与参数宽高一致，不留黑边）
        /// </summary>
        /// <param name="image"></param>
        /// <param name="wndWidth"></param>
        /// <param name="wndHeigth"></param>
        /// <returns></returns>
        public static Bitmap BitmapResize3(Bitmap image, int width, int height)
        {
            int nSourceWidth = image.Width;
            int nSourceHeigth = image.Height;

            float fDestWidth, fDestHight;

            if (nSourceWidth < width && nSourceHeigth < height)
            {
                fDestWidth = nSourceWidth;
                fDestHight = nSourceHeigth;
            }


            float fRatio = (float)(nSourceHeigth * 1.0 / nSourceWidth);
            if (fRatio * width > height)
            {
                fDestWidth = (float)width;
                fDestHight = fDestWidth * fRatio;
            }
            else
            {
                fDestHight = (float)height;
                fDestWidth = fDestHight / fRatio;
            }

            int sW = (int)fDestWidth;
            int sH = (int)fDestHight;

            Bitmap outBmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, new Rectangle(0, 0, width, height), (sW - width) / 2, (sH - height) / 2, width, height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量     
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            image.Dispose();
            return outBmp;
        }

        /// <summary>
        /// 图片快速复制
        /// </summary>
        /// <returns></returns>
        public static Bitmap BitmapClone(Bitmap bitmap)
        {
            if (bitmap == null)
                return null;
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            System.Drawing.Imaging.BitmapData bmpDataNew = newBitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            /* Get the pointer to the pixels */
            IntPtr pBmp = bmpData.Scan0;
            IntPtr pBmpNew = bmpDataNew.Scan0;
            int srcStride = bmpData.Stride;
            int pSize = srcStride * bitmap.Height;
            CopyMemory(pBmpNew, pBmp, pSize);
            bitmap.UnlockBits(bmpData);
            newBitmap.UnlockBits(bmpDataNew);
            return newBitmap;
        }

        /// <summary>
        /// 图片绑定
        /// </summary>
        /// <returns></returns>
        public static IntPtr BindingBitmap(ref InteropBitmap interopBitmap, IntPtr map, Bitmap bitmap)
        {
            if (bitmap == null)
                return map;
            if (interopBitmap == null)
            {
                //创建内存映射
                uint pcount = (uint)(bitmap.Width * bitmap.Height * System.Windows.Media.PixelFormats.Bgr32.BitsPerPixel / 8.0);
                IntPtr section = CreateFileMapping(new IntPtr(-1), IntPtr.Zero, 0x04, 0, pcount, null);
                map = MapViewOfFile(section, 0xF001F, 0, 0, pcount);
                interopBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromMemorySection(section, (int)bitmap.Width, (int)bitmap.Height, System.Windows.Media.PixelFormats.Bgr32,
                    (int)(bitmap.Width * System.Windows.Media.PixelFormats.Bgr32.BitsPerPixel / 8), 0) as InteropBitmap;
            }
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, (int)bitmap.Width, (int)bitmap.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            /* Get the pointer to the pixels */
            IntPtr pBmp = bmpData.Scan0;
            int srcStride = bmpData.Stride;
            int pSize = srcStride * (int)bitmap.Height;
            CopyMemory(map, pBmp, pSize);
            bitmap.UnlockBits(bmpData);
            if (interopBitmap != null)
            {
                interopBitmap.Invalidate();
            }
            return map;
        }

        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <returns></returns>
        public static Bitmap CutImage(Bitmap img, Rectangle rect)
        {
            Bitmap resultImg = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(resultImg);
            g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect.X, rect.Y, rect.Width, rect.Height, GraphicsUnit.Pixel);
            g.Dispose();
            return resultImg;
        }

        /// <summary>
        /// 从大图中截取一部分图片
        /// </summary>
        /// <param name="img">来源图片</param>
        /// <param name="offsetX">从偏移X坐标位置开始截取</param>
        /// <param name="offsetY">从偏移Y坐标位置开始截取</param>
        /// <param name="toImagePath">保存图片</param>
        /// <param name="width">保存图片的宽度</param>
        /// <param name="height">保存图片的高度</param>
        /// <returns></returns>
        public static Bitmap CaptureImage(Bitmap img, int offsetX, int offsetY, string toImagePath, int width, int height)
        {
            //创建新图位图
            Bitmap bitmap = new Bitmap(width, height);
            //创建作图区域
            Graphics graphic = Graphics.FromImage(bitmap);
            //截取原图相应区域写入作图区
            graphic.DrawImage(img, 0, 0, new Rectangle(offsetX, offsetY, width, height), GraphicsUnit.Pixel);
            //从作图区生成新图
            Bitmap saveImage = Image.FromHbitmap(bitmap.GetHbitmap());
            //释放资源   
            saveImage.Dispose();
            graphic.Dispose();
            bitmap.Dispose();
            return saveImage;
        }

        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        private static extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpFileMappingAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hObject);
    }
}
