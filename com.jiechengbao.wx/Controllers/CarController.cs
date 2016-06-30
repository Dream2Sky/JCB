using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using com.jiechengbao.wx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.wx.Controllers
{
    public class CarController:Controller
    {
        private IMemberBLL _memberBLL;
        private ICarBLL _carBLL;

        public CarController(IMemberBLL memberBLL, ICarBLL carBLL)
        {
            _memberBLL = memberBLL;
            _carBLL = carBLL;
        }

        public ActionResult List()
        {
            // 先获取当前 用户
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
                return Content("<div class='title'>传递的参数有误,请重新提交</div>");
            }

            // 根据车牌号 获得车辆信息
            Car car = _carBLL.GetCarByCarNumber(numberplate);

            // 如果为空 则返回错误信息
            if (car == null)
            {
                return Content("<div class='title'>未能查询到此车牌号的车辆信息</div>");
            }

            // 不为空就返回对象
            return View(car);
        }

        /// <summary>
        /// 更新车辆信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update()
        {
            Guid CarId = Guid.Parse(Request.QueryString["CarId"].ToString());
            if (CarId == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            Car car = _carBLL.GetCarById(CarId);

            car.CarDetailInfo = Request.QueryString["CarDetailInfo"].ToString();
            car.Numberplate = Request.QueryString["Numberplate"].ToString();
            car.EngineNumber = Request.QueryString["EngineNumber"].ToString();
            car.ChassisNumber = Request.QueryString["ChassisNumber"].ToString();

            if (_carBLL.Update(car))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

    }
}