using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DostDestek.Common;
using DostDestek.Entity;


namespace DostDestek.Repository
{
    public class CommentRepository : DataRepository<Comment, int>
    {
        private HayvanDestekEntities db = Tools.GetConnection();
        ResultProcess<Comment> result = new ResultProcess<Comment>();
        public override Result<int> Delete(int id)
        {
            Comment comment = db.Comment.SingleOrDefault(t => t.CommentId == id);
            db.Comment.Remove(comment);
            return result.GetResult(db); ;
        }

        public override Result<List<Comment>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<Comment> GetObjById(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(Comment item)
        {
            db.Comment.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Comment>> List()
        {
            return result.GetListResult(db.Comment.ToList());
        }

        public override Result<int> Update(Comment item)
        {
            throw new NotImplementedException();
        }
    }
}
