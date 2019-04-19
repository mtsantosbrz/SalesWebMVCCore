
using System.Collections.Generic;
using System;
using System.Linq;

namespace SallesWebMvc.Models
{
    public class Departament
    {
        public Departament()
        {

        }
        public Departament(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();


        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(sl => sl.TotalSales(initial, final));
        }
    }
}
