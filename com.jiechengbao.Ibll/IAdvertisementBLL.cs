using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IAdvertisementBLL
    {
        IEnumerable<Advertisement> GetAllNotDeletedAdvertisements(int count);
        IEnumerable<Advertisement> GetNotDeletedAdvertisementsByCategory(Guid categoryId);
        IEnumerable<Advertisement> GetNotDeletedAndIsRecommandAdByCategory(Guid categoryId);
        IEnumerable<Advertisement> GetNotDeletedAndNotIsRecommandAdByCategory(Guid categoryId);
        IEnumerable<Advertisement> GetNotDeletedAndIsRecommandAd(int count);
        IEnumerable<Advertisement> GetNotDeletedAndNotIsRecommandAd(int count);
        Advertisement GetAdByAdCode(string code);
        IEnumerable<Advertisement> GetAllNotDeletedAdvertisements();
    }
}
