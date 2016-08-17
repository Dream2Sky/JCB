using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using com.jiechengbao.wx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.jiechengbao.common;
using System.Net;
using System.Collections;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Tags;
using Winista.Text.HtmlParser.Filters;
using com.jiechengbao.wx.Global;
using POPO.ActionFilter.Helper;

namespace com.jiechengbao.wx.Controllers
{
    [WhitespaceFilter]   
    [ETag]
    public class CarController:Controller
    {
        private IMemberBLL _memberBLL;
        private ICarBLL _carBLL;

        public CarController(IMemberBLL memberBLL, ICarBLL carBLL)
        {
            _memberBLL = memberBLL;
            _carBLL = carBLL;
        }

        [IsRegister]
        public ActionResult List()
        {
            // 先获取当前 用户
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            List<Car> carList = _carBLL.GetCarListByMemberId(member.Id).ToList();

            ViewData["CarList"] = carList;
            return View();
        }

        public ActionResult CarList()
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());
            List<Car> carList = _carBLL.GetCarListByMemberId(member.Id).ToList();

            ViewData["CarList"] = carList;

            return View();
        }

        [HttpPost]
        public ActionResult Add(CarModel model)
        {
            // 上来先获取当前用户的对象
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            //  传递的参数 是否为空
            if (model == null)
            {
                return Json("NullParam", JsonRequestBehavior.AllowGet);
            }

            // 再判断 车牌是否相同
            if (_carBLL.IsExist(model.Numberplate.Trim()))
            {
                return Json("ExistNumber", JsonRequestBehavior.AllowGet);
            }

            // 添加新车
            Car car = new Car();
            car.Id = Guid.NewGuid();
            car.IsDeleted = false;
            car.MemberId = member.Id;
            car.Numberplate = model.Numberplate.Trim();
            car.EngineNumber = model.EngineNumber.Trim();
            car.DeletedTime = DateTime.MinValue.AddHours(8);
            car.CreatedTime = DateTime.Now;
            car.ChassisNumber = model.ChassisNumber.Trim();
            car.CarDetailInfo = model.CarDetailInfo.Trim();

            if (_carBLL.Add(car))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Detail(string numberplate)
        {
            // 上来先判断 传递进来的参数是否为空
            if (string.IsNullOrEmpty(numberplate))
            {
                return Content("<div class='title'  style=\"margin: 20px 15px 10px;color: #6d6d72;font-size: 15px;\">传递的参数有误,请重新提交</div>");
            }
            LogHelper.Log.Write("车牌号: "+numberplate);

            // 根据车牌号 获得车辆信息
            Car car = _carBLL.GetCarByCarNumber(numberplate);

            // 如果为空 则返回错误信息
            if (car == null)
            {
                return Content("<div class='title' style=\"margin: 20px 15px 10px;color: #6d6d72;font-size: 15px;\">未能查询到此车牌号的车辆信息</div>");
            }

            // 不为空就返回对象
            return View(car);
        }

        /// <summary>
        /// 更新车辆信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(CarDetailModel cdm)
        {
            if (cdm == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            Car car = _carBLL.GetCarById(cdm.CarId);
            car.Numberplate = cdm.Numberplate;
            car.ChassisNumber = cdm.ChassisNumber;
            car.CarDetailInfo = cdm.CarDetailInfo;
            car.EngineNumber = cdm.EngineNumber;

            if (_carBLL.Update(car))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCarByNumberPlate(string Numberplate)
        {
            if (string.IsNullOrEmpty(Numberplate))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            else
            {
                Car car = _carBLL.GetCarByCarNumber(Numberplate);
                var data = new
                {
                    ChassicNumber = car.ChassisNumber,
                    EngineNumber = car.EngineNumber
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Check(string cookieName, string cookieStr)
        {
            string url = "http://www.cx580.com/Web/Query.aspx";
            Cookie cookie = new Cookie();
            cookie.Name = cookieName;
            cookie.Domain = "www.cx580.com";
            cookie.Path = "/";
            cookie.Value = cookieStr;
            cookie.Expires = DateTime.Now.AddDays(1);

            CookieContainer cc = new CookieContainer();
            cc.Add(new Uri(url),cookie);
            
            string response = HttpManager.AccessURL_GET(url,cc);

            if (response == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            Parser parser = new Parser(new Winista.Text.HtmlParser.Lex.Lexer(response));
            


            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(string carId)
        {
            try
            {
                if (_carBLL.RemoveById(Guid.Parse(carId)))
                {
                    return Json("True", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("False", JsonRequestBehavior.AllowGet);
                }
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