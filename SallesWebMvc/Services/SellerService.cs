using SallesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SallesWebMvc.Services.Exceptions;

namespace SallesWebMvc.Services
{
    public class SellerService
    {
        private readonly SallesWebMvcContext _context;
        public SellerService(SallesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIDAsync(int id)
        {
            return await _context.Seller.Include(obj=>obj.Departament).FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var seller = await FindByIDAsync(id);
                _context.Remove(seller);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(s => s.ID == seller.ID);
            if (!hasAny)
            {
                throw new NotFoundException("ID not found!");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task<bool> SellerExists(int id)
        {
            return await _context.Seller.AnyAsync(e => e.ID == id);
        }
    }
}
