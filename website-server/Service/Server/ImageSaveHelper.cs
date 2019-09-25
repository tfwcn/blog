using Common;
using System;
using System.IO;

namespace Server
{
    public static class ImageSaveHelper
    {
        /// <summary>
        /// 保存图片到服务器
        /// </summary>
        /// <param name="baseDir"></param>
        /// <param name="id"></param>
        /// <param name="imageBase64"></param>
        /// <returns></returns>
        public static string CheckAndSavePath(string baseDir, string subDir, string id, string imageBase64)
        {
            if (imageBase64.IndexOf("{0}") == 0)
                return imageBase64;
            string imagePath = subDir + "/" + DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(baseDir + imagePath))
                Directory.CreateDirectory(baseDir + imagePath);
            imagePath = "{0}" + imagePath + "/" + id + "_" + DateTime.Now.Ticks.ToString() + ".jpg";
            var img = ImageHelper.Base64ToBitmap(imageBase64);
            var savePath = String.Format(imagePath, baseDir);
            img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            img.Dispose();
            return imagePath;
        }
    }
}
