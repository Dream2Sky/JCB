using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.jiechengbao.admin.Models
{
    public class CarModel:Car
    {
        public CarModel() { }
        public CarModel(Car car) {
            this.CarDetailInfo = car.CarDetailInfo;
            this.ChassisNumber = car.ChassisNumber;
            this.CreatedTime = car.CreatedTime;
            this.DeletedTime = car.DeletedTime;
            this.EngineNumber = car.EngineNumber;
            this.Id = car.Id;
            this.IsDeleted = car.IsDeleted;
            this.MemberId = car.MemberId;
            this.Numberplate = car.Numberplate;
            
        }
        public string MemberName { get; set; }
        public string MemberImagePath { get; set; }
        public string Phone { get; set; }
    }
}