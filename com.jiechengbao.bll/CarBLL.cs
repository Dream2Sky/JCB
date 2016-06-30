using com.jiechengbao.Ibll;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;

namespace com.jiechengbao.bll
{
    public class CarBLL:ICarBLL
    {
        private ICarDAL _carDAL;
        public CarBLL(ICarDAL carDAL)
        {
            _carDAL = carDAL;
        }

        public bool Add(Car car)
        {
            return _carDAL.Insert(car);
        }

        /// <summary>
        /// 根据车牌号获取汽车信息
        /// </summary>
        /// <param name="numberplate"></param>
        /// <returns></returns>
        public Car GetCarByCarNumber(string numberplate)
        {
            return _carDAL.SelectByNumberPlate(numberplate);
        }

        public Car GetCarById(Guid Id)
        {
            return _carDAL.SelectById(Id);
        }

        public IEnumerable<Car> GetCarListByMemberId(Guid memberId)
        {
            return _carDAL.SelectByMemberId(memberId);
        }

        public bool IsExist(string numberplate)
        {
            return _carDAL.IsExist(numberplate);
        }

        public bool Update(Car car)
        {
            return _carDAL.Update(car);
        }
    }
}
