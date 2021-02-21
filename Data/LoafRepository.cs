using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoafAndStranger.Models;

namespace LoafAndStranger.Data
{
    public class LoafRepository
    {

        static List<Loaf> _loaves = new List<Loaf>
            {
                new Loaf { id=1, Price = 5.50, Size = LoafSize.Medium, Sliced = true, Type = "Rye" },
                new Loaf { id=2, Price = 2.50, Size = LoafSize.Small, Sliced = false, Type = "French" }
            };

        public List<Loaf> GetAll()
        {
            return _loaves;
        }

        public void Add(Loaf loaf)
        {
            var biggestExistingId = _loaves.Max(l => l.id);
            loaf.id = biggestExistingId + 1;
            _loaves.Add(loaf);
        }

        public Loaf Get(int id)
        {
            var loaf = _loaves.FirstOrDefault(l => l.id == id);
            return loaf;
        }

        public void Remove(int id) {
            var loafToRemove = Get(id);
            _loaves.Remove(loafToRemove);
           
        }

    }
}
