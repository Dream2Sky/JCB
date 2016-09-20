/*
 老板说 不要积分商城

 所以此类不用

 */
using ch.lib.common.QR;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.wx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class CreditMollController : Controller
    {
        /// <summary>
        ///  异步生成的委托
        /// </summary>
        /// <param name="Id"></param>
        private delegate void GenerateQRDel(Guid Id, Guid memberId);

        /// <summary>
        /// 异步扣除用户总积分的委托
        /// </summary>
        /// <param name="memberId"></param>
        private delegate void ConsumeCreditDel(Guid memberId, double credit);

        private IExchangeServiceBLL _exchangeServiceBLL;
        private IMemberBLL _memberBLL;
        private IExchangeServiceRecordBLL _exchangeServiceRecordBLL;
        private IExchangeServiceQRBLL _exchangeServiceQRBLL;
        public CreditMollController(IExchangeServiceBLL exchangeServiceBLL,
            IMemberBLL memberBLL, IExchangeServiceRecordBLL exchangeServiceRecordBLL,
            IExchangeServiceQRBLL exchangeServiceQRBLL)
        {
            _exchangeServiceBLL = exchangeServiceBLL;
            _memberBLL = memberBLL;
            _exchangeServiceRecordBLL = exchangeServiceRecordBLL;
            _exchangeServiceQRBLL = exchangeServiceQRBLL;
        }

        /// <summary>
        /// 积分商城的首页 展示兑换商品的列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            ViewData["ExchangeServiceList"] = _exchangeServiceBLL.GetAllNoDeletedExchangeServiceList();
            return View();
        }

        /// <summary>
        /// 积分商城预兑换页 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult PreExchange(string code)
        {
            ExchangeService ex = _exchangeServiceBLL.GetNoDeletedExchangeServiceByCode(code);
            return View(ex);
        }

        /// <summary>
        /// 商品兑换的兑换方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Exchange(string exchangeCode)
        {
            // 先判断传递进来的 兑换商品的编号是否为空
            if (string.IsNullOrEmpty(exchangeCode))
            {
                // 为空 就返回 Fasle
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            // 再获取当前用户的对象
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            // 对象的空值判断
            if (member == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            ExchangeService es = _exchangeServiceBLL.GetNoDeletedExchangeServiceByCode(exchangeCode);

            if (es == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            if (es.Credit >= member.Credit)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            // 构造兑换服务记录
            ExchangeServiceRecord esr = new ExchangeServiceRecord();

            esr.Id = Guid.NewGuid();
            esr.IsDeleted = false;
            esr.IsUse = false;
            esr.MemberId = member.Id;
            esr.CreatedTime = DateTime.Now;
            esr.Credit = es.Credit;
            esr.DeletedTime = DateTime.MinValue.AddHours(8);
            esr.ExchangeSerivceId = es.Id;

            // 兑换服务的二维码 先空出来 因为要异步生成二维码
            // 等生成之后再 补充
            esr.QRPath = "hehe";

            if (_exchangeServiceRecordBLL.Add(esr))
            {
                // 如果添加兑换服务成功  则 扣除当前积分
                ConsumeCreditDel conDel = new ConsumeCreditDel(ConsumeCredit);
                IAsyncResult consumeResult = conDel.BeginInvoke(member.Id, esr.Credit, CreditCallBackMethod, null);

                // 如果兑换服务记录表更新成功 则 异步生成二维码
                GenerateQRDel del = new GenerateQRDel(GenerateQR);
                IAsyncResult result = del.BeginInvoke(esr.Id, member.Id, CallBackMethod, null);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            return Json("True", JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private void GenerateQR(Guid esrId, Guid memberId)
        {
            try
            {
                string dir = Server.MapPath("~/ExchangeQR/");
                ExchangeServiceRecord esr = _exchangeServiceRecordBLL.GetESRById(esrId);
                string sourceString = "http://" + WxConfig.WxDomain + "/Pay/ConsumeExchangeService?esrId=" + esrId.ToString();
                string qrPath = QRCodeCreator.Create(sourceString, dir);
                esr.QRPath = qrPath;

                ExchangeServiceQR qr = new ExchangeServiceQR();
                qr.Id = Guid.NewGuid();
                qr.CreatedTime = DateTime.Now;
                qr.DeletedTime = DateTime.MinValue.AddHours(8);
                qr.ExchangeServiceId = esr.Id;
                qr.IsDeleted = false;
                qr.MemberId = memberId;
                qr.QRPath = qrPath;

                _exchangeServiceQRBLL.Add(qr);

                _exchangeServiceRecordBLL.Update(esr);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        [NonAction]
        private void CallBackMethod(IAsyncResult ar)
        {
            try
            {
                GenerateQRDel del = (GenerateQRDel)ar.AsyncState;
                del.EndInvoke(ar);
            }
            catch (Exception)
            {
            }

        }

        [NonAction]
        private void ConsumeCredit(Guid memberId, double credit)
        {
            Member member = _memberBLL.GetMemberById(memberId);
            member.Credit -= credit;

            _memberBLL.Update(member);
        }

        [NonAction]
        private void CreditCallBackMethod(IAsyncResult ar)
        {
            try
            {
                ConsumeCreditDel del = (ConsumeCreditDel)ar.AsyncState;
                del.EndInvoke(ar);
            }
            catch (Exception)
            {
            }
        }
    }
}