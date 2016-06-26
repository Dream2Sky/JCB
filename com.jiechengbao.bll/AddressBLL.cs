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
    public class AddressBLL:IAddressBLL
    {
        private IAddressDAL _addressDAL;
        public AddressBLL(IAddressDAL addressDAL)
        {
            _addressDAL = addressDAL;
        }

        public bool Add(Address address)
        {
            return _addressDAL.Insert(address);
        }

        public Address GetAddressById(Guid addressId)
        {
            return _addressDAL.SelectById(addressId);
        }

        public IEnumerable<Address> GetAddressByMemberId(Guid memberId)
        {
            return _addressDAL.SelectByMemberId(memberId);
        }

        /// <summary>
        /// 找到默认地址 没有默认地址的就用第一个地址
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public Address GetDefaultOrFirstAddress(Guid memberId)
        {
            List<Address> addressList = _addressDAL.SelectByMemberId(memberId).ToList();

            return addressList.SingleOrDefault(n => n.IsDefault == true) == null ? addressList.FirstOrDefault() : addressList.SingleOrDefault(n => n.IsDefault == true);
        }

        /// <summary>
        /// 判断是否绑定了配送地址
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public bool IsBindAddress(Guid memberId)
        {
            List<Address> addressList = _addressDAL.SelectByMemberId(memberId).ToList();
            if (addressList == null || addressList.Count <=0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Update(Address address)
        {
            return _addressDAL.Update(address);
        }
    }
}
