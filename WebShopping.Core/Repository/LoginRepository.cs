using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopping.Core.IRepository;
using WebShopping.Core.Objects;

namespace WebShopping.Core.Repository
{
    public class LoginRepository : ILoginRepository
    {
        ShopDbContext db = new ShopDbContext();

        public Member AddMember(Member member)
        {
            db.Members.Add(member);
            db.SaveChanges();
            return member;
        }

        public Member CheckMember(string email)
        {
            return db.Members.SingleOrDefault(p => p.Email == email);
        }

        public Member GetMember(string email, string password)
        {
            return db.Members.SingleOrDefault(p => p.Email == email && p.Password == password);
        }

        public List<Member> GetMembers()
        {
            return db.Members.ToList();
        }
    }
}
