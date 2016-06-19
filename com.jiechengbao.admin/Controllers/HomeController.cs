using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.jiechengbao.admin.Controllers
{
    public class HomeController:Controller
    {
        /// <summary>
        /// 管理员后台首页 
        /// 登陆成功后进入此页
        /// 显示 各种数据信息 包括 当前用户数量 当前新提交的订单数量 
        /// 这里用图表表示
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}