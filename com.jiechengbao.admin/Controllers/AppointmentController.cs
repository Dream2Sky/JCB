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
    public class AppointmentController : Controller
    {
        private IAppointmentServiceBLL _appointmentServiceBLL;
        private IAppointmentTimeBLL _appointmentTimeBLL;
        private IMyAppointmentBLL _myAppointmentBLL;
        private IMyAppointmentItemBLL _myAppointmentItemBLL;
        private IMemberBLL _memberBLL;
        public AppointmentController(IAppointmentServiceBLL appointmentServiceBLL,
            IAppointmentTimeBLL appointmentTimeBLL, 
            IMyAppointmentBLL myAppointmentBLL,IMyAppointmentItemBLL myAppointmentItemBLL,
            IMemberBLL memberBLL)
        {
            _appointmentServiceBLL = appointmentServiceBLL;
            _appointmentTimeBLL = appointmentTimeBLL;
            _myAppointmentBLL = myAppointmentBLL;
            _myAppointmentItemBLL = myAppointmentItemBLL;
            _memberBLL = memberBLL;
        }

        public ActionResult Setting()
        {
            ViewData["AppointmentServiceList"] = _appointmentServiceBLL.GetAllAppointmentServiceButNotDeleted();
            ViewData["AppointmentTimeList"] = _appointmentTimeBLL.GetLastAppointmentTimeList();
            return View();
        }

        #region 预约服务项列表

        /// <summary>
        /// 预约服务列表
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AppointmentServiceList()
        {
            List<AppointmentService> asList = _appointmentServiceBLL.GetAllAppointmentService().ToList();
            ViewData["AppointmentServiceList"] = asList;
            return PartialView();
        }


        /// <summary>
        /// 添加服务项
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddService(string serviceName)
        {
            // 判断空值
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            // 判断是否存在该服务项
            if (_appointmentServiceBLL.IsExistByName(serviceName))
            {
                return Json("ExistName", JsonRequestBehavior.AllowGet);
            }

            try
            {
                AppointmentService appointmentService = new AppointmentService();

                appointmentService.Id = Guid.NewGuid();
                appointmentService.Code = "apm_" + TimeManager.GetCurrentTimestamp();
                appointmentService.CreatedTime = DateTime.Now;
                appointmentService.DeletedTime = DateTime.MinValue.AddHours(8);
                appointmentService.IsDeleted = false;
                appointmentService.Name = serviceName;

                // 添加预约服务
                if (_appointmentServiceBLL.Add(appointmentService))
                {
                    // 构造 返回的 json 对象
                    var obj = new
                    {
                        Code = appointmentService.Code,
                        Name = appointmentService.Name
                    };

                    // 添加成功则返回json对象  
                    return Json(obj, JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// 删除服务项
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteService(string Code)
        {
            if (string.IsNullOrWhiteSpace(Code))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            AppointmentService appointmentService = new AppointmentService();
            appointmentService = _appointmentServiceBLL.GetByCode(Code);

            if (appointmentService == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            appointmentService.IsDeleted = true;
            appointmentService.DeletedTime = DateTime.Now;

            if (_appointmentServiceBLL.Update(appointmentService))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region 预约时间项列表

        /// <summary>
        /// 预约时间列表
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AppointmentTimeList()
        {
            List<AppointmentTime> atList = _appointmentTimeBLL.GetLastAppointmentTimeList().ToList();
            ViewData["AppointmentTimeList"] = atList;
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddTime(string TimePeriod)
        {
            if (string.IsNullOrWhiteSpace(TimePeriod))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            List<AppointmentTime> atList = _appointmentTimeBLL.GetLastAppointmentTimeList().ToList();

            string[] tp = TimePeriod.Split('~');
            DateTime stime = DateTime.Parse(tp[0]);
            DateTime etime = DateTime.Parse(tp[1]);

            if (stime >= etime)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            foreach (var item in atList)
            {
                string[] time = item.TimePeriod.Split('~');
                DateTime startTime = DateTime.Parse(time[0]);
                DateTime endTime = DateTime.Parse(time[1]);

                if ((stime > startTime && stime < endTime) || (endTime > startTime && etime < endTime))
                {
                    return Json("Exception", JsonRequestBehavior.AllowGet);
                }
            }

            AppointmentTime at = new AppointmentTime();
            at.Id = Guid.NewGuid();
            at.IsDeleted = false;
            at.TimePeriod = stime.ToShortTimeString() + "~" + etime.ToShortTimeString();
            at.CreatedTime = DateTime.Now;
            at.DeletedTime = DateTime.MinValue.AddHours(8);

            if (_appointmentTimeBLL.Add(at))
            {
                var obj = new
                {
                    Id = at.Id,
                    TimePeriod = at.TimePeriod
                };

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteTime(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            AppointmentTime at = new AppointmentTime();
            at = _appointmentTimeBLL.GetById(Guid.Parse(Id));

            if (at == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            at.DeletedTime = DateTime.Now;
            at.IsDeleted = true;

            if (_appointmentTimeBLL.Update(at))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        /// <summary>
        /// 获取所有已付款的预约单
        /// </summary>
        /// <returns></returns>
        public ActionResult HasPayAppointment()
        {
            // 先获取我的预约服务列表
            List<MyAppointment> appointmentList = new List<MyAppointment>();
            appointmentList = _myAppointmentBLL.GetAllHasPayAppointment().ToList();

            // 构造 MyAppointmentModel List 
            List<MyAppointmentModel> modelList = new List<MyAppointmentModel>();

            modelList = GetMyAppointmentModelList(appointmentList);

            ViewData["HasPayAppointmentList"] = modelList;

            return View();
        }

        [HttpPost]
        public ActionResult HasPayAppointment(string name)
        {
            List<MyAppointment> appointmentList = new List<MyAppointment>();

            List<Member> memberList = _memberBLL.GetMembersByNickNameAndPhone(name).ToList();

            foreach (var item in memberList)
            {
                appointmentList.AddRange(_myAppointmentBLL.GetHasPayAppointmentByMemberId(item.Id));
            }

            List<MyAppointmentModel> modelList = new List<MyAppointmentModel>();

            modelList = GetMyAppointmentModelList(appointmentList);

            ViewData["HasPayAppointmentList"] = modelList;
            return View();
        }

        /// <summary>
        /// 获取所有待付款的预约单
        /// </summary>
        /// <returns></returns>
        public ActionResult NotPayAppointment()
        {
            //先获取我的未付款的预约服务列表
            List<MyAppointment> appointmentList = new List<MyAppointment>();
            appointmentList = _myAppointmentBLL.GetAllNoPayAppointment().ToList();

            // 构造 MyAppointmentModel List
            List<MyAppointmentModel> modelList = new List<MyAppointmentModel>();
            modelList = GetMyAppointmentModelList(appointmentList);

            ViewData["NOPayAppointmentList"] = modelList;

            return View();
        }

        [HttpPost]
        public ActionResult NotPayAppointment(string name)
        {
            List<MyAppointment> appointmentList = new List<MyAppointment>();

            List<Member> memberList = _memberBLL.GetMembersByNickNameAndPhone(name).ToList();

            foreach (var item in memberList)
            {
                appointmentList.AddRange(_myAppointmentBLL.GetNoPayAppointmentByMemberId(item.Id));
            }

            List<MyAppointmentModel> modelList = new List<MyAppointmentModel>();

            modelList = GetMyAppointmentModelList(appointmentList);

            ViewData["HasPayAppointmentList"] = modelList;
            return View();
        }

        [NonAction]
        private List<MyAppointmentModel> GetMyAppointmentModelList(List<MyAppointment> myAppointmentList)
        {
            // 这个是要传递到view的 modelList 
            List<MyAppointmentModel> modelList = new List<MyAppointmentModel>();

            // 遍历我的预约服务列表
            foreach (var item in myAppointmentList)
            {
                // 根据每个预约单的Id 找到 对应的服务项
                List<MyAppointmentItem> appointmentItemList = new List<MyAppointmentItem>();
                appointmentItemList = _myAppointmentItemBLL.GetByMyAppointmentId(item.Id).ToList();

                List<string> itemList = new List<string>();

                foreach (var aItem in appointmentItemList)
                {
                    // 找到每个服务项 并添加到 itemList中
                    AppointmentService appointmentService = _appointmentServiceBLL.GetById(aItem.AppointmentServiceId);
                    itemList.Add(appointmentService.Name);
                }

                // 构造 myAppointmentModel 
                MyAppointmentModel apmodel = new MyAppointmentModel();
                apmodel.MyAppointmentId = item.Id;

                Member member = new Member();
                member = _memberBLL.GetMemberById(item.MemberId);

                apmodel.NickName = member.NickeName;
                apmodel.HeadImage = member.HeadImage;

                apmodel.AppointmentTime = item.AppointmentTime;
                apmodel.IsPay = item.IsPay;
                apmodel.AppointmentNameList = itemList;

                apmodel.Price = item.Price;
                apmodel.Notes = item.Notes;

                // 添加到前面定义的 modelList中
                modelList.Add(apmodel);
            }

            return modelList;
        }

        [HttpPost]
        public ActionResult Pay(Guid Id, double Price, string Notes)
        {
            MyAppointment myAppointment = new MyAppointment();
            myAppointment = _myAppointmentBLL.GetById(Id);

            myAppointment.IsPay = true;
            myAppointment.Price = Price;
            myAppointment.Notes = Notes;

            if (_myAppointmentBLL.Update(myAppointment))
            {
                var obj = new
                {
                    Price = Price,
                    Notes = Notes
                };

                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            return Json("False", JsonRequestBehavior.AllowGet);
        }
    }
}