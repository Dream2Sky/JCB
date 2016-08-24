using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Web.Hosting;

namespace com.jiechengbao.common
{
    public class ImageManager
    {

        #region 老方法
        
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="imageStream"></param>
        /// <returns></returns>
        public static Image GetImage(Stream imageStream)
        {
            Image image;
            
            try
            {
                image = Image.FromStream(imageStream);
                return image;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 判断是否是合法的扩展名
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static bool IsRightExtension(string imageName)
        {
            string imageExt = Path.GetExtension(imageName).ToLower();
            string[] extension = ConfigurationManager.AppSettings["Extension"].ToString().Split(',');
            foreach (var item in extension)
            {
                if (item == imageExt)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 生成指定大小的缩略图
        /// </summary>
        /// <param name="oImage"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap CreateThumbnail(Image oImage,int width, int height)
        {
            try
            {
                int oWidth = oImage.Width;
                int oHeight = oImage.Height;
                int tWidth = width;
                int tHeight = height;

                if (oWidth >= oHeight)
                {
                    tHeight = (int)Math.Floor(Convert.ToDouble(oHeight) * (Convert.ToDouble(tWidth) / Convert.ToDouble(oWidth)));
                }
                else
                {
                    tWidth = (int)Math.Floor(Convert.ToDouble(oWidth) * (Convert.ToDouble(tHeight) / Convert.ToDouble(oHeight)));
                }

                Bitmap tImage = new Bitmap(tWidth, tHeight);
                Graphics g = Graphics.FromImage(oImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(Color.Transparent);

                g.DrawImage(oImage, new Rectangle(0, 0, tWidth, tHeight), new Rectangle(0, 0, oWidth, oHeight), GraphicsUnit.Pixel);

                oImage.Dispose();
                g.Dispose();

                return tImage;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }

        }


        /// <summary>
        /// 保存Bitmap图片
        /// </summary>
        /// <param name="imageBit"></param>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string SaveImage(Bitmap imageBit, string fileName, string type)
        {
            string fullName = string.Empty;
            try
            {
                string path = ConfigurationManager.AppSettings[type].ToString();
                //path = HostingEnvironment.MapPath(path);

                fullName = path + fileName;
                imageBit.Save(fullName, System.Drawing.Imaging.ImageFormat.Jpeg);

                imageBit.Dispose();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                imageBit.Dispose();
                throw;
            }
            

            return fullName;
        }

        /// <summary>
        /// 保存image图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string SaveImage(Image image, string fileName, string type)
        {
            string fullName = string.Empty;
            try
            {
                string path = ConfigurationManager.AppSettings[type].ToString();
                LogHelper.Log.Write("path:" + path);
                //path = HostingEnvironment.MapPath(path);
                if (image==null)
                {
                    LogHelper.Log.Write("image == null");
                }
                fullName = path + fileName;
                image.Save(fullName);

                image.Dispose();
                
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                image.Dispose();
                throw;
            }

            return fullName;
        }
        #endregion


        #region 新方法

        public static void CreateThumnail(Image image, string savePath, int intHeight)
        {
            try
            {
                Bitmap old = new Bitmap(image);
                int intWidth = (intHeight * old.Width) / old.Height;
                Bitmap newbit = new Bitmap(old, intWidth, intHeight);
                newbit.Save(savePath);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public static void SaveImage(Image image, string fileName, int intHeight, string type)
        {
            string path = ConfigurationManager.AppSettings[type].ToString();
            CreateThumnail(image, path + fileName, intHeight);
        }

        #endregion
    }
}
