using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DostDestek.Entity;
using DostDestek.Common;

namespace DostDestek.Repository
{
    public class MemberRepository : DataRepository<Member, int>
    {
        private static HayvanDestekEntities db = Tools.GetConnection();
        ResultProcess<Member> result = new ResultProcess<Member>();
        public override Result<int> Delete(int id)
        {
            Member m = db.Member.SingleOrDefault(t => t.UserId == id);
            db.Member.Remove(m);
            return result.GetResult(db);
        }
        public Result<Member> GetMemByRoleId(int roleid)
        {
            return result.GetT(db.Member.SingleOrDefault(t => t.RoleId == roleid));
        }
        public override Result<List<Member>> GetLatestObj(int Quantity)
        {
            return result.GetListResult(db.Member.OrderByDescending(t => t.UserId).Take(Quantity).ToList());
        }

        public override Result<Member> GetObjById(int id)
        {
            return result.GetT(db.Member.SingleOrDefault(t => t.UserId == id));
        }

        public override Result<int> Insert(Member item)
        {
            db.Member.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Member>> List()
        {
            return result.GetListResult(db.Member.ToList());
        }

        public override Result<int> Update(Member item)
        {
            Member m = db.Member.SingleOrDefault(t => t.UserId == item.UserId);
            m.FirstName = item.FirstName;
            m.LastName = item.LastName;
            m.Email = item.Email;
            m.Password = item.Password;
            m.RoleId = item.RoleId;
            m.UserName = item.UserName;
            return result.GetResult(db);
        }
    }
}
