using com.jiechengbao.entity;
using com.jiechengbao.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.dal
{
    public class AdvertisementDAL : DataBaseDAL<Advertisement>, IAdvertisementDAL
    {
        /// <summary>
        /// 获取指定数量的没有被删除的广告
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Advertisement> GetAllNotDeletedAdvertisement(int count)
        {
            try
            {
                return db.Set<Advertisement>().Where(n => n.IsDeleted == false).Take(count);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// 获取指定分类的没有被删除的广告
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public IEnumerable<Advertisement> GetNotDeletedAdvertisementByCategory(Guid categoryId)
        {
            try
            {
                return db.Set<Advertisement>().Where(n => n.CategoryId == categoryId).OrderByDescending(n=>n.CreatedTime);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// 获取所有的未删除的广告
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Advertisement> SelectAllNotDeletedAdverstisements()
        {
            try
            {
                return db.Set<Advertisement>().Where(n => n.IsDeleted == false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// 获取指定code的广告
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Advertisement SelectByAdCode(string code)
        {
            try
            {
                return db.Set<Advertisement>().SingleOrDefault(n => n.AdCode == code);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// 获取指定数量的未删除的推荐广告
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Advertisement> SelectNotDeletedAndIsRecommandAd(int count)
        {
            try
            {
                return db.Set<Advertisement>().Where(n => n.IsDeleted == false && n.IsRecommend == true).OrderByDescending(n => n.CreatedTime).Take(count);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// 根据分类Id找出没删除的推荐广告
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public IEnumerable<Advertisement> SelectNotDeletedAndIsRecommandAdByCategory(Guid categoryId)
        {
            try
            {
                return db.Set<Advertisement>().Where(n => n.IsDeleted == false && n.CategoryId == categoryId && n.IsRecommend == true);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// 根据分类Id找出没删除的非推荐广告
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Advertisement> SelectNotDeletedAndNotIsRecommandAd(int count)
        {
            try
            {
                return db.Set<Advertisement>().Where(n => n.IsDeleted == false && n.IsRecommend == false).OrderByDescending(n => n.CreatedTime).Take(count);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// 根据分类Id找出没删除的不是推荐广告
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public IEnumerable<Advertisement> SelectNotDeletedAndNotIsRecommandAdByCategory(Guid categoryId)
        {
            try
            {
                return db.Set<Advertisement>().Where(n => n.IsDeleted == false && n.IsRecommend == false && n.CategoryId == categoryId);
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
