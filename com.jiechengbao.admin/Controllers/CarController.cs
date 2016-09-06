using com.jiechengbao.admin.Global;
using com.jiechengbao.admin.Models;
using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using POPO.ActionFilter.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.admin.Controllers
{
    [WhitespaceFilter]
    [ETag]
    [IsLogin]
    public class CarController:Controller
    {
        private IMemberBLL _memberBLL;
        private ICarBLL _carBLL;

        public CarController(IMemberBLL memberBLL, ICarBLL carBLL)
        {
            _memberBLL = memberBLL;
            _carBLL = carBLL;
        }

        /// <summary>
        /// 列出用户的爱车啊
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            // 上来先获取最新用户的前10名
            List<Member> memberList = _memberBLL.GetAllNoDeletedMembers().ToList();

            List<CarModel> carList = new List<CarModel>();

            // 遍历用户列表 构造CarModel列表
            foreach (var item in memberList)
            {
                foreach (var car in _carBLL.GetCarListByMemberId(item.Id))
                {
                    CarModel cm = new CarModel(car);
                    cm.MemberName = item.NickeName;
                    cm.Phone = item.Phone;
                    cm.MemberImagePath = item.HeadImage;

                    carList.Add(cm);
                }
            }
            // 返回CarModel列表
            ViewData["CarList"] = carList;
            return View();
        }

        [HttpPost]
        public ActionResult List(string condition)
        {
            // 上来先判断传递过来的条件是否为空
       
            // 如果为空 则 Get 到 /Car/List 显示出前 10 名用户的爱车列表
            if (string.IsNullOrEmpty(condition))
            {
                return RedirectToAction("List");
            }

            List<Car> carList = _carBLL.GetCarListByAnythingCondition(condition).ToList();
            List<CarModel> carModelList = new List<CarModel>();

            foreach (var item in carList)
            {
                Member member = _memberBLL.GetMemberById(item.MemberId);

                CarModel cm = new CarModel(item);
                cm.MemberName = member.NickeName;
                cm.MemberImagePath = member.HeadImage;
                cm.Phone = member.Phone;

                carModelList.Add(cm);
            }

            ViewData["CarList"] = carModelList;
            return View();
        }
    }
}