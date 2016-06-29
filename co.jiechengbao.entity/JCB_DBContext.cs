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
    public class JCB_DBContext:DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Goods> GoodsSet { get; set; }
        public DbSet<GoodsCategory> GoodsCategorySet { get; set; }
        public DbSet<GoodsImage> GoodsImages{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Recharge> Recharges { get; set; }
        public DbSet<Transaction> TransactionSet { get; set; }
        public DbSet<Rules> Ruleses { get; set; }
    }
}
