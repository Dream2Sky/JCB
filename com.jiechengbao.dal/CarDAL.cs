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
