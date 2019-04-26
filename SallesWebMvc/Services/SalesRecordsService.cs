using Microsoft.EntityFrameworkCore;
using SallesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SallesWebMvc.Services
{
    public class SalesRecordsService
    {
        private readonly SallesWebMvcContext _context;

        public SalesRecordsService(SallesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? initial, DateTime? final)
        {
            var result = from db in _context.SalesRecords select db;
            if(initial.HasValue)
            {
                result = result.Where(x => x.Date >= initial.Value);
            }
            if (final.HasValue)
            {
                result = result.Where(x => x.Date <= final.Value);
            }
            return await result.Include(x=>x.Seller).Include(x=>x.Seller.Departament).OrderByDescending(x=>x.Date).ToListAsync();
        }

        public async Task<List<IGrouping<Departament,SalesRecord>>> FindByDateGroupingAsync(DateTime? initial, DateTime? final)
        {
            var result = from db in _context.SalesRecords select db;
            if (initial.HasValue)
            {
                result = result.Where(x => x.Date >= initial.Value);
            }
            if (final.HasValue)
            {
                result = result.Where(x => x.Date <= final.Value);
            }
            return await result.Include(x => x.Seller).Include(x => x.Seller.Departament).OrderByDescending(x => x.Date).GroupBy(x=>x.Seller.Departament).ToListAsync();
        }
    }
}
