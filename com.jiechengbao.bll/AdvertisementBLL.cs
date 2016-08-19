using com.jiechengbao.Ibll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.entity;
using com.jiechengbao.Idal;

namespace com.jiechengbao.bll
{
    public class AdvertisementBLL : IAdvertisementBLL
    {
        private IAdvertisementDAL _advertisementDAL;
        public AdvertisementBLL(IAdvertisementDAL advertisementDAL)
        {
            _advertisementDAL = advertisementDAL;
        }

        public Advertisement GetAdByAdCode(string code)
        {
            return _advertisementDAL.SelectByAdCode(code);
        }

        public IEnumerable<Advertisement> GetAllNotDeletedAdvertisements()
        {
            return _advertisementDAL.SelectAllNotDeletedAdverstisements();
        }

        public IEnumerable<Advertisement> GetAllNotDeletedAdvertisements(int count)
        {
            return _advertisementDAL.GetAllNotDeletedAdvertisement(count);
        }

        public IEnumerable<Advertisement> GetNotDeletedAdvertisementsByCategory(Guid categoryId)
        {
            return _advertisementDAL.GetNotDeletedAdvertisementByCategory(categoryId);
        }

        public IEnumerable<Advertisement> GetNotDeletedAndIsRecommandAd(int count)
        {
            return _advertisementDAL.SelectNotDeletedAndIsRecommandAd(count);
        }

        public IEnumerable<Advertisement> GetNotDeletedAndIsRecommandAdByCategory(Guid categoryId)
        {
            return _advertisementDAL.SelectNotDeletedAndIsRecommandAdByCategory(categoryId);
        }

        public IEnumerable<Advertisement> GetNotDeletedAndNotIsRecommandAd(int count)
        {
            return _advertisementDAL.SelectNotDeletedAndNotIsRecommandAd(count);
        }

        public IEnumerable<Advertisement> GetNotDeletedAndNotIsRecommandAdByCategory(Guid categoryId)
        {
            return _advertisementDAL.SelectNotDeletedAndNotIsRecommandAdByCategory(categoryId);
        }

    }
}
