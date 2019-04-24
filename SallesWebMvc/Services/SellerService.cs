using SallesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SallesWebMvc.Services
{
    public class SellerService
    {
        private readonly SallesWebMvcContext _context;
        public SellerService(SallesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindByID(int id)
        {
            return _context.Seller.FirstOrDefault(x => x.ID == id);
        }

        public void Remove(int id)
        {
            var seller = FindByID(id);
            _context.Remove(seller);
            _context.SaveChanges();
        }
    }
}
