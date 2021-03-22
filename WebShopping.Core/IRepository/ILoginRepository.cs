using System.Collections.Generic;
using WebShopping.Core.Objects;

namespace WebShopping.Core.IRepository
{
    public interface ILoginRepository
    {
        List<Member> GetMembers();
        Member GetMember(string email, string password);
        Member CheckMember(string email);
        Member AddMember(Member member);
    }
}
