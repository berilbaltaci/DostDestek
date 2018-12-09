using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DostDestek.Common;
using DostDestek.Entity;

namespace DostDestek.Repository
{
    public class AnimalRepository : DataRepository<Animal, int>
    {
        private static HayvanDestekEntities db = Tools.GetConnection();
        ResultProcess<Animal> result = new ResultProcess<Animal>();
        public override Result<int> Delete(int id)
        {
            Animal animal = db.Animal.SingleOrDefault(t => t.AnimalId == id);
            db.Animal.Remove(animal);
            return result.GetResult(db);
        }

        public override Result<List<Animal>> GetLatestObj(int Quantity)
        {
            return result.GetListResult(db.Animal.OrderByDescending(t => t.AnimalId).Take(Quantity).ToList());
        }

        public override Result<Animal> GetObjById(int id)
        {
            return result.GetT(db.Animal.SingleOrDefault(t => t.AnimalId == id));
        }

        public override Result<int> Insert(Animal item)
        {
            db.Animal.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Animal>> List()
        {
            return result.GetListResult(db.Animal.ToList());
        }

        public override Result<int> Update(Animal item)
        {
            throw new NotImplementedException();
        }
    }
}
