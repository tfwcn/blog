using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 坐标系帮助类
    /// </summary>
    public static class CoordinateHelper
    {
        /// <summary>
        /// 获取两点距离
        /// </summary>
        /// <returns></returns>
        public static double GetDistance(PointD point1, PointD point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
        /// <summary>
        /// 获取两点距离
        /// </summary>
        /// <returns></returns>
        public static double GetDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
        /// <summary>
        /// 获取两点距离
        /// </summary>
        /// <returns></returns>
        public static double GetDistance(System.Windows.Point point1, System.Windows.Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
        /// <summary>
        /// 获取斜边长度
        /// </summary>
        /// <returns></returns>
        public static double GetDistance(double xDistance, double yDistance)
        {
            return Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
        }
        /// <summary>
        /// 解析字符串(0.001,1.002)
        /// </summary>
        /// <returns></returns>
        public static PointD StringToPoint(string pointStr)
        {
            PointD point = new PointD();
            string[] strValues = pointStr.Split(',');
            point.X = (float)Convert.ToDouble(strValues[0]);
            point.Y = (float)Convert.ToDouble(strValues[1]);
            return point;
        }
        /// <summary>
        /// 点转字符串(0.001,1.002)
        /// </summary>
        /// <returns></returns>
        public static string PointToString(double x, double y)
        {
            return $"{x},{y}";
        }
        /// <summary>
        /// 点转字符串(0.001,1.002)
        /// </summary>
        /// <returns></returns>
        public static string PointToString(PointD point)
        {
            return $"{point.X},{point.Y}";
        }
        /// <summary>
        /// 双精度点
        /// </summary>
        public class PointD
        {
            public PointD()
            {
                this.IsEmpty = true;
            }
            public PointD(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }
            [JsonPropIgnoreAttibute]
            public bool IsEmpty { get; private set; }
            public double X { get; set; }
            public double Y { get; set; }
        }
    }
}
