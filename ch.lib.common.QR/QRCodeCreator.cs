using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;

namespace ch.lib.common.QR
{
    public class QRCodeCreator
    {
        /// <summary>
        /// 生成二维码文件
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static string Create(string sourceString, string dir)
        {
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 5;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                Bitmap img = qrCodeEncoder.Encode(sourceString);

                string fileName = Guid.NewGuid().ToString().ToLower().Replace("-", "") + ".jpg";
                LogHelper.Log.Write("QRFileName:" + fileName);
                img.Save(dir + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();

                return fileName;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }

        }
    }
}
