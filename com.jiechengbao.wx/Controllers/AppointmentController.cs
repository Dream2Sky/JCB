using com.jiechengbao.entity;
using com.jiechengbao.Ibll;
using com.jiechengbao.wx.Global;
using com.jiechengbao.wx.Models;
using POPO.ActionFilter.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace com.jiechengbao.wx.Controllers
{
    [WhitespaceFilter]
    [ETag]
    public class AppointmentController:Controller
    {
        private IAppointmentServiceBLL _appointmentServiceBLL;
        private IAppointmentTimeBLL _appointmentTimeBLL;
        private IMemberBLL _memberBLL;
        private ICarBLL _carBLL;
        private IMyAppointmentBLL _myAppointmentBLL;
        private IMyAppointmentItemBLL _myAppointmentItemBLL;
        public AppointmentController(IAppointmentServiceBLL appointmentServiceBLL,
            IAppointmentTimeBLL appointmentTimeBLL, IMemberBLL memberBLL, 
            ICarBLL carBLL, IMyAppointmentBLL myAppointmentBLL,
            IMyAppointmentItemBLL myAppointmentItemBLL)
        {
            _appointmentServiceBLL = appointmentServiceBLL;
            _appointmentTimeBLL = appointmentTimeBLL;
            _memberBLL = memberBLL;
            _carBLL = carBLL;
            _myAppointmentBLL = myAppointmentBLL;
            _myAppointmentItemBLL = myAppointmentItemBLL;
        }

        [IsRegister]
        public ActionResult MyAppointment()
        {
            // 上来先获取当前用户信息
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            // 获取车辆信息
            List<Car> carList = _carBLL.GetCarListByMemberId(member.Id).ToList();

            // 获取预约服务列表
            List<AppointmentService> appointmentList = _appointmentServiceBLL.GetAllAppointmentServiceButNotDeleted().ToList();

            List<AppointmentTime> appointmentTimeList = _appointmentTimeBLL.GetLastAppointmentTimeList().OrderBy(n=>n.TimePeriod).ToList();

            ViewData["carList"] = carList;
            ViewData["appointmentList"] = appointmentList;
            ViewData["appointmentTimeList"] = appointmentTimeList;

            return View();
        }

        [HttpPost]
        public ActionResult Add(string json)
        {
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            MyAppointmentModel appointmentModel = new MyAppointmentModel();

            try
            {
                // 解析json
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                appointmentModel = serializer.Deserialize<MyAppointmentModel>(json);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }

            // 构造 MyAppointment 
            entity.MyAppointment myAppointment = new entity.MyAppointment();

            myAppointment.Id = Guid.NewGuid();
            myAppointment.IsDeleted = false;
            myAppointment.IsPay = false;
            myAppointment.MemberId = member.Id;
            myAppointment.Notes = "";
            myAppointment.Supplement = appointmentModel.jcbremark;
            myAppointment.CarInfo = appointmentModel.carInfo;
            myAppointment.CarNumber = appointmentModel.carNo;
            myAppointment.Price = 0;
            myAppointment.CreatedTime = DateTime.Now;
            myAppointment.DeletedTime = DateTime.MinValue.AddHours(8);
            myAppointment.AppointmentTime = appointmentModel.appointmentTime;

            if (_myAppointmentBLL.Add(myAppointment))
            {
                foreach (var item in appointmentModel.appointmentItems)
                {
                    MyAppointmentItem appointmentItem = new MyAppointmentItem();
                    appointmentItem.Id = Guid.NewGuid();
                    appointmentItem.CreatedTime = DateTime.Now;
                    appointmentItem.DeletedTime = DateTime.MinValue.AddHours(8);
                    appointmentItem.IsDeleted = false;
                    appointmentItem.MyAppointmentId = myAppointment.Id;
                    appointmentItem.AppointmentServiceId = Guid.Parse(item);

                    _myAppointmentItemBLL.Add(appointmentItem);
                }
                // 返回 json

                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                // 返回 json
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 我的预约单列表
        /// </summary>
        /// <returns></returns>
        [IsRegister]
        public ActionResult List()
        {
            // 先获取当前会员对象
            Member member = _memberBLL.GetMemberByOpenId(System.Web.HttpContext.Current.Session["member"].ToString());

            if (member == null)
            {
                LogHelper.Log.Write("member null");
            }

            // 获取该会员对象 所有的 预约单
            List<MyAppointment> myAppointmentList = new List<entity.MyAppointment>();
            myAppointmentList = _myAppointmentBLL.GetHasPayAppointmentByMemberId(member.Id).ToList();
            myAppointmentList.AddRange(_myAppointmentBLL.GetNoPayAppointmentByMemberId(member.Id).ToList());

            List<MyAppointmentListModel> appointmentModelList = GetMyAppointmentModelList(myAppointmentList);

            ViewData["MyAppointmentList"] = appointmentModelList;

            return View();
        }

        /// <summary>
        /// 构造 MyAppointmentListModel;
        /// </summary>
        /// <param name="myAppointmentList"></param>
        /// <returns></returns>
        [NonAction]
        private List<MyAppointmentListModel> GetMyAppointmentModelList(List<MyAppointment> myAppointmentList)
        {
            // 这个是要传递到view的 modelList 
            List<MyAppointmentListModel> modelList = new List<MyAppointmentListModel>();

            // 遍历我的预约服务列表
            foreach (var item in myAppointmentList)
            {
                // 根据每个预约单的Id 找到 对应的服务项
                List<MyAppointmentItem> appointmentItemList = new List<MyAppointmentItem>();
                appointmentItemList = _myAppointmentItemBLL.GetByMyAppointmentId(item.Id).ToList();

                MyAppointmentListModel apmodel = new MyAppointmentListModel();
                foreach (var aItem in appointmentItemList)
                {
                    // 找到每个服务项 并添加到 itemList中
                    AppointmentService appointmentService = _appointmentServiceBLL.GetById(aItem.AppointmentServiceId);
                    apmodel.AppointmentServiceItems = appointmentService.Name + " ";
                }

                // 构造 myAppointmentModel 
                
                apmodel.MyAppointmentId = item.Id;

                Member member = new Member();
                member = _memberBLL.GetMemberById(item.MemberId);

                
                apmodel.AppointmentTime = item.AppointmentTime;
                apmodel.IsPay = item.IsPay;

                apmodel.Price = item.Price;
                apmodel.CarNumber = item.CarNumber;

                // 添加到前面定义的 modelList中
                modelList.Add(apmodel);
            }

            return modelList;
        }
    }
}