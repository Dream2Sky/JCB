using com.jiechengbao.Idal;
using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace com.jiechengbao.dal
{
    public class DataBaseDAL<T> : IDataBaseDAL<T> where T : DataEntity
    {
        protected readonly JCB_DBContext db;
        public DataBaseDAL()
        {
            db = new JCB_DBContext();
        }

        public bool Clear()
        {
            try
            {
                var entitys = db.Set<T>();
                entitys.ToList().ForEach(entity => db.Entry(entity).State = System.Data.Entity.EntityState.Deleted); //不加这句也可以，为什么？
                db.Set<T>().RemoveRange(entitys);

                SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return false;
            }
        }

        public bool Delete(T t)
        {
            try
            {
                db.Set<T>().Attach(t);
                db.Entry(t).State = EntityState.Deleted;
                SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return false;
            }
        }

        public bool Insert(T t)
        {
            try
            {
                db.Set<T>().Attach(t);
                db.Set<T>().Add(t);

                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return false;
            }
        }

        public IEnumerable<T> SelectAll()
        {
            try
            {
                return db.Set<T>();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return null;
            }
        }

        public T SelectById(Guid id)
        {
            try
            {
                return db.Set<T>().SingleOrDefault(n => n.Id == id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
        }

        public bool Update(T t)
        {
            try
            {
                db.Set<T>().Attach(t);
                db.Entry(t).State = EntityState.Modified;

                SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return false;
            }
        }

        private int SaveChanges()
        {
            try
            {
                int result = db.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                string message = "error:";
                if (ex.InnerException == null)
                    message += ex.Message + ",";
                else if (ex.InnerException.InnerException == null)
                    message += ex.InnerException.Message + ",";
                else if (ex.InnerException.InnerException.InnerException == null)
                    message += ex.InnerException.InnerException.Message + ",";
                LogHelper.Log.Write(message);
                throw new Exception(message);
            }
        }
    }
}
