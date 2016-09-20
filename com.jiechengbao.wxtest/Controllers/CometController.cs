using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class CometController:Controller
    {
        public ActionResult IsConsume()
        {
            if (System.Web.HttpContext.Current.Session["IsPay"] != null)
            {
                System.Web.HttpContext.Current.Session["IsPay"] = null;
                DeleteQRFile();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public void CreateSession()
        {
            System.Web.HttpContext.Current.Session["IsPay"] = "True";
        }

        private void DeleteQRFile()
        {
            #region 删除服务二维码
            if (System.Web.HttpContext.Current.Session["ServiceQRPath"] != null)
            {
                try
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(System.Web.HttpContext.Current.Session["ServiceQRPath"].ToString());
                    if (fi != null)
                    {
                        fi.Delete();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Write(ex.Message);
                    LogHelper.Log.Write(ex.StackTrace);
                }
            }
            #endregion

            #region 优惠券二维码 
            if (System.Web.HttpContext.Current.Session["FreeCouponQrPath"] != null)
            {
                try
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(System.Web.HttpContext.Current.Session["FreeCouponQrPath"].ToString());
                    if (fi != null)
                    {
                        fi.Delete();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Write(ex.Message);
                    LogHelper.Log.Write(ex.StackTrace);
                }
            }
            #endregion
        }
    }
}