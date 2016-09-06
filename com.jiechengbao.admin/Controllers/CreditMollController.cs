using com.jiechengbao.admin.Global;
using com.jiechengbao.admin.Models;
using com.jiechengbao.common;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.admin.Controllers
{
    [IsLogin]
    public class CreditMollController:Controller
    {
        private IExchangeServiceBLL _exchangeServiceBLL;
        public CreditMollController(IExchangeServiceBLL exchangeServiceBLL)
        {
            _exchangeServiceBLL = exchangeServiceBLL;
        }

        public ActionResult List()
        {
            List<ExchangeService> exchangeServiceList = new List<ExchangeService>();

            exchangeServiceList = _exchangeServiceBLL.GetAllNoDeletedExchangeServiceList().ToList();
            ViewData["ExchangeServiceList"] = exchangeServiceList;

            return View();
        }

        [HttpPost]
        public ActionResult List(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                return RedirectToAction("List");
            }

            List<ExchangeService> esList = new List<ExchangeService>();

            esList = _exchangeServiceBLL.GetExchangeServiceByName(condition).ToList();
            ViewData["ExchangeServiceList"] = esList;
            return View();
        }

        /// <summary>
        /// 添加新积分兑换商品
        /// </summary>
        /// <returns></returns>
        public ActionResult Add(string msg)
        {
            ViewBag.Msg = msg;
            return View();
        }

        [HttpPost]
        public ActionResult Add(ExchangeServiceModel model)
        {
            if (model == null)
            {
                return RedirectToAction("Add", new { msg = "提交的数据为空，请重新提交" });
            }

            ExchangeService es = new ExchangeService();
            es.Id = Guid.NewGuid();
            es.ImagePath = model.ImagePath;
            es.IsDeleted = false;
            es.Name = model.Name;
            es.Notes = model.Notes;
            es.Price = model.Price;
            es.Code = "ExchangeService_" + TimeManager.GetCurrentTimestamp();
            es.CreatedTime = DateTime.Now;
            es.DeletedTime = DateTime.MinValue.AddHours(8);
            es.Credit = model.Credit;

            if (_exchangeServiceBLL.Add(es))
            {
                return RedirectToAction("Add", new { msg = "添加成功" });
            }
            else
            {
                return RedirectToAction("Add", new { msg = "添加失败" });
            }
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                string fileName = EncryptManager.SHA1(file.FileName + TimeManager.GetCurrentTimestamp()) + ".jpg";
                string path = System.IO.Path.Combine(Server.MapPath("~/Uploads"), System.IO.Path.GetFileName(fileName));
                file.SaveAs(path);

                return Json(fileName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Update(string ExchangeServiceCode)
        {
            ExchangeService es = new ExchangeService();
            es = _exchangeServiceBLL.GetNoDeletedExchangeServiceByCode(ExchangeServiceCode);

            return View(es);
        }

        [HttpPost]
        public ActionResult Update(ExchangeServiceModel model)
        {
            if (model == null)
            {
                return RedirectToAction("List", new { msg = "更新失败" });
            }

            ExchangeService es = _exchangeServiceBLL.GetNoDeletedExchangeServiceByCode(model.Code);
            es.Credit = model.Credit;
            es.ImagePath = model.ImagePath;
            es.Name = model.Name;
            es.Notes = model.Notes;
            es.Price = model.Price;
            es.Credit = model.Credit;

            if (_exchangeServiceBLL.Update(es))
            {
                return RedirectToAction("List", new { msg = "更新成功" });
            }
            else
            {
                return RedirectToAction("List", new { msg = "更新失败" });
            }
        }

    }
}