using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface ICarBLL
    {
        IEnumerable<Car> GetCarListByMemberId(Guid memberId);
        bool IsExist(string numberplate);
        bool Add(Car car);
        Car GetCarById(Guid Id);
        Car GetCarByCarNumber(string numberplate);
        bool Update(Car car);
    }
}
