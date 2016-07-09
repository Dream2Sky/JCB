using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class CreditMollController:Controller
    {
        private IExchangeServiceBLL _exchangeServiceBLL;
        public CreditMollController(IExchangeServiceBLL exchangeServiceBLL)
        {
            _exchangeServiceBLL = exchangeServiceBLL;
        }
        public ActionResult List()
        {
            ViewData["ExchangeServiceList"] = _exchangeServiceBLL.GetAllNoDeletedExchangeServiceList();
            return View();
        }


        public ActionResult PreExchange(string code)
        {
            ExchangeService ex = _exchangeServiceBLL.GetNoDeletedExchangeServiceByCode(code);
            return View(ex);
        }

        [HttpPost]
        public ActionResult Exchange(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Json("False",JsonRequestBehavior.AllowGet);
            }



            return View();
        }

    }
}