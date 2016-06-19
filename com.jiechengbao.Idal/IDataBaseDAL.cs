using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface IDataBaseDAL<T> where T:class
    {
        bool Insert(T t);
        bool Delete(T t);
        bool Update(T t);
        T SelectById(Guid id);
        IEnumerable<T> SelectAll();
        bool Clear();
    }
}
