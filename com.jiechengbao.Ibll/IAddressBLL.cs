using com.jiechengbao.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.jiechengbao.Ibll
{
    public interface IAddressBLL
    {
        IEnumerable<Address> GetAddressByMemberId(Guid memberId);
        Address GetAddressById(Guid addressId);
        bool IsBindAddress(Guid memberId);
        Address GetDefaultOrFirstAddress(Guid memberId);
        bool Update(Address address);
        bool Add(Address address);
    }
}
