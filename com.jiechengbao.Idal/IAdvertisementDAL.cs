using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Idal
{
    public interface IAdvertisementDAL:IDataBaseDAL<Advertisement>
    {
        IEnumerable<Advertisement> GetAllNotDeletedAdvertisement(int count);
        IEnumerable<Advertisement> GetNotDeletedAdvertisementByCategory(Guid categoryId);
        /// <summary>
        /// 根据分类Id找出没删除的推荐广告
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        IEnumerable<Advertisement> SelectNotDeletedAndIsRecommandAdByCategory(Guid categoryId);
        IEnumerable<Advertisement> SelectNotDeletedAndNotIsRecommandAdByCategory(Guid categoryId);
        IEnumerable<Advertisement> SelectNotDeletedAndNotIsRecommandAd(int count);
        IEnumerable<Advertisement> SelectNotDeletedAndIsRecommandAd(int count);
        Advertisement SelectByAdCode(string code);
        IEnumerable<Advertisement> SelectAllNotDeletedAdverstisements();

    }
}
