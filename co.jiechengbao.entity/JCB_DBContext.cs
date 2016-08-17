using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.entity
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class JCB_DBContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Goods> GoodsSet { get; set; }
        public DbSet<GoodsCategory> GoodsCategorySet { get; set; }
        public DbSet<GoodsImage> GoodsImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Recharge> Recharges { get; set; }
        public DbSet<Transaction> TransactionSet { get; set; }
        public DbSet<Rules> Ruleses { get; set; }

        /// <summary>
        /// 推荐商品表
        /// </summary>
        public DbSet<ReCommend> ReCommends { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CreditRecord> CreditRecords { get; set; }
        public DbSet<MyService> Services { get; set; }

        /// <summary>
        /// 服务消费记录表
        /// </summary>
        public DbSet<ServiceConsumeRecord> SCRecord { get; set; }

        public DbSet<ServiceQR> ServiceQRs { get; set; }
        public DbSet<ServiceConsumePassword> SCPassword { get; set; }

        /// <summary>
        /// 兑换服务
        /// </summary>
        public DbSet<ExchangeService> ExchangeServices { get; set; }

        /// <summary>
        ///  服务购买记录表
        /// </summary>
        public DbSet<ExchangeServiceRecord> ExchageServiceRecords { get; set; }

        /// <summary>
        /// 存放兑换服务消费二维码
        /// </summary>
        public DbSet<ExchangeServiceQR> ExchangeServiceQRs { get; set; }

        /// <summary>
        /// 记录订单状态  0 已发货 1 未发货 2 确认收货
        /// </summary>
        public DbSet<OrderStatus> OrderStatusSet { get; set; }

        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        public DbSet<AppointmentService> AppointmentServices { get; set; }
        public DbSet<AppointmentTime> AppointmentTimes { get; set; }
        public DbSet<MyAppointment> MyAppointments { get; set; }
        public DbSet<MyAppointmentItem> MyAppointmentItems { get; set; }


        // 赠送的优惠券模块
        /// <summary>
        /// 优惠券 表
        /// </summary>
        public DbSet<FreeCoupon> FreeCoupons { get; set; }

        /// <summary>
        /// 我的优惠券 表
        /// </summary>
        public DbSet<MyFreeCoupon> MyFreeCoupons { get; set; }

        /// <summary>
        /// 广告 只看不买的广告
        /// </summary>
        public DbSet<Advertisement> Advertisements { get; set; }

    }
}
