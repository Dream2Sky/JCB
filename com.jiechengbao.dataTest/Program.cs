using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.common;
using com.jiechengbao.dal;
using System.Net;
using POPO.Picture.Helper;

namespace com.jiechengbao.dataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");
            #region 添加一个管理员

            //Admin admin = new Admin();
            //admin.Id = Guid.NewGuid();
            //admin.IsDeleted = false;
            //admin.Account = "admin";
            //admin.Password = EncryptManager.SHA1("admin");
            //admin.CreatedTime = DateTime.Now;

            //AdminDAL dal = new AdminDAL();
            //try
            //{
            //    dal.Insert(admin);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine(ex.StackTrace);
            //    throw;
            //}
            #endregion

            #region 添加一个会员
            //Member member = new Member();
            //member.Id = Guid.NewGuid();
            //member.IsDeleted = false;
            //member.NickeName = "hehe";
            //member.OpenId = "okzkZv6LHCo-vIyZHynDoXjeUbKs";
            //member.HeadImage = "/Imaegs/Default.png";
            //member.CreatedTime = DateTime.Now.Date;
            //member.DeletedTime = DateTime.MinValue.AddHours(8);

            //MemberDAL dal = new MemberDAL();
            //dal.Insert(member);
            #endregion

            #region 注释

            //string url = "http://www.baidu.com";
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            //MemberDAL dal = new MemberDAL();
            //Member member = dal.SelectByWxOpenId("okzkZv6LHCo-vIyZHynDoXjeUbKs");
            //Console.WriteLine(member.NickeName);

            ///GetMyAppointments();

            //Console.WriteLine(EncryptManager.MD5("admin123"));

            //GoodsDAL dal = new GoodsDAL();
            //foreach (var item in dal.SelectByAllNoDeletedGoods())
            //{
            //    Console.WriteLine(item.Code);
            //}


            //AdvertisementDAL dal = new AdvertisementDAL();

            //foreach (var item in dal.GetAllNotDeletedAdvertisement(5))
            //{
            //    Console.WriteLine(item.AdName);
            //}

            #endregion

            string sourceFile = @"D:/source.jpg";
            string outputFile = @"D:/output.jpg";

            PictureHelper.getThumImage(sourceFile, 70, 1, outputFile);

            Console.WriteLine("Finished");

            Console.Read();
        }

        static void GetMyAppointments()
        {
            Guid mId = Guid.Parse("5cb8fb1c-f63a-4206-a161-2fe5123104c2");
            MyAppointmentDAL dal = new MyAppointmentDAL();

            foreach (var item in dal.SelectByMemberIdAndPay(mId,false))
            {
                Console.WriteLine(item.CarNumber);
            }
        }

    }

    public class AdModel
    {
        public string AdName { get; set; }
        public string AdCode { get; set; }
        public string AdDescription { get; set; }
        public string AdImagePath { get; set; }
        public bool IsRecommand { get; set; }
    }
}
