using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DostDestek.Common;
using DostDestek.Entity;

namespace DostDestek.Repository
{
    public class TopicRepository : DataRepository<Topic, int>
    {
        HayvanDestekEntities db = new HayvanDestekEntities();
        ResultProcess<Topic> result = new ResultProcess<Topic>();

        public override Result<int> Delete(int id)
        {
            Topic topic = db.Topic.SingleOrDefault(t => t.TopicId == id);
            db.Topic.Remove(topic);
            return result.GetResult(db);
        }

        public override Result<List<Topic>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<Topic> GetObjById(int id)
        {
            return result.GetT(db.Topic.SingleOrDefault(t => t.TopicId == id));
        }

        public override Result<int> Insert(Topic item)
        {
            db.Topic.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Topic>> List()
        {
            return result.GetListResult(db.Topic.ToList());
        }

        public override Result<int> Update(Topic item)
        {
            throw new NotImplementedException();
        }
    }
}
