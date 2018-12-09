using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DostDestek.Common;
using DostDestek.Entity;
using PagedList;
using PagedList.Mvc;

namespace DostDestek.Repository
{
    public class PostRepository : DataRepository<Post, int>
    {
        private static HayvanDestekEntities db = Tools.GetConnection();
        ResultProcess<Post> result = new ResultProcess<Post>();
        public override Result<int> Delete(int id)
        {
            Post post = db.Post.SingleOrDefault(t => t.PostId == id);
            db.Post.Remove(post);
            return result.GetResult(db);
        }
        public Result<List<Post>> GetMostPopular(int num)
        {
            return result.GetListResult(db.Post.OrderByDescending(t => t.View).Take(5).ToList());
        }
        public override Result<List<Post>> GetLatestObj(int memid)
        {
            return result.GetListResult(db.Post.OrderByDescending(t => t.MemberId).ToList());
        }
        public override Result<Post> GetObjById(int id)
        {
            return result.GetT(db.Post.SingleOrDefault(t => t.PostId == id));
        }

        public override Result<int> Insert(Post item)
        {
            db.Post.Add(item);
            return result.GetResult(db);
        }
        public Result<List<Post>> GetListByPostId(int id)
        {
            return result.GetListResult(db.Post.Where(t => t.PostId == id).ToList());
        }
        public override Result<List<Post>> List()
        {
            return result.GetListResult(db.Post.ToList());
        }
        public override Result<int> Update(Post item)
        {
            throw new NotImplementedException();
        }
    }
}
