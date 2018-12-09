using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DostDestek.Common;
using DostDestek.Entity;

namespace DostDestek.Repository
{
    public class VetRepository : DataRepository<Veteriner, int>
    {
        private static HayvanDestekEntities db = Tools.GetConnection();
        ResultProcess<Veteriner> result = new ResultProcess<Veteriner>();
        public override Result<int> Delete(int id)
        {
            Veteriner v = db.Veteriner.SingleOrDefault(t => t.VetId == id);
            db.Veteriner.Remove(v);
            return result.GetResult(db);
        }

        public override Result<List<Veteriner>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<Veteriner> GetObjById(int id)
        {
            return result.GetT(db.Veteriner.SingleOrDefault(t => t.VetId == id));
        }

        public override Result<int> Insert(Veteriner item)
        {
            db.Veteriner.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Veteriner>> List()
        {
            return result.GetListResult(db.Veteriner.ToList());
        }

        public override Result<int> Update(Veteriner vet)
        {
            Veteriner v = db.Veteriner.SingleOrDefault(t => t.VetId == vet.VetId);
            v.VetName = vet.VetName;
            v.VetTelNo = vet.VetTelNo;
            v.VetAddress = vet.VetAddress;
            v.VetCommName = vet.VetCommName;
            return result.GetResult(db);
        }
    }
}
