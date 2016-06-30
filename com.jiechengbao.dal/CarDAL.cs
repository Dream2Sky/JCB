using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class CarDAL : DataBaseDAL<Car>, ICarDAL
    {
        public bool IsExist(string numberplate)
        {
            try
            {
                if (db.Set<Car>().Where(n=>n.Numberplate == numberplate).SingleOrDefault() == null)
                {
                    return false;
                }
                else
                {
                    return true;
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
        /// 根据任意条件查询Car列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<CarNotEntity> SelectByAnythingCondition(string condition)
        {
            try
            {
                var result = from car in db.Cars
                             join member in db.Members on car.MemberId equals member.Id
                             where member.NickeName.Contains(condition) || car.CarDetailInfo.Contains(condition)
                             || car.ChassisNumber.Contains(condition) || car.EngineNumber.Contains(condition) ||
                             car.Numberplate.Contains(condition)
                             select new CarNotEntity
                             {
                                 Id = car.Id,
                                 CreatedTime = car.CreatedTime,
                                 MemberId = car.MemberId,
                                 CarDetailInfo = car.CarDetailInfo,
                                 ChassisNumber = car.ChassisNumber,
                                 EngineNumber = car.EngineNumber,
                                 Numberplate = car.Numberplate,
                                 IsDeleted = car.IsDeleted,
                                 DeletedTime = car.DeletedTime
                             };
                return result.ToList();
                             
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public IEnumerable<Car> SelectByMemberId(Guid memberId)
        {
            try
            {
                return db.Set<Car>().Where(n => n.MemberId == memberId); 
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        public Car SelectByNumberPlate(string numberplate)
        {
            try
            {
                return db.Set<Car>().SingleOrDefault(n => n.Numberplate == numberplate);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
    }
}
