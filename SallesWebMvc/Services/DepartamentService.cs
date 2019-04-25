using SallesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SallesWebMvc.Services.Exceptions;

namespace SallesWebMvc.Services
{
    public class DepartamentService
    {
        private readonly SallesWebMvcContext _context;
        public DepartamentService(SallesWebMvcContext context)
        {
            _context = context;
        }

        #region Find

        public async Task<List<Departament>> FindAllAsync()
        {
            return await _context.Departament.OrderBy(x=> x.Name).ToListAsync();
        }

        public async Task<Departament> FindByIDAsync(int id)
        {
            return await _context.Departament.FirstOrDefaultAsync(x => x.ID == id);
        }

        #endregion

        #region Insert

        public async Task InsertAsync(Departament departament)
        {
            _context.Add(departament);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Remove

        public async Task RemoveAsync(int id)
        {
            var departament = await FindByIDAsync(id);
            _context.Remove(departament);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Update

        public async Task UpdateAsync(Departament departament)
        {
            bool hasAny = await _context.Departament.AnyAsync(s => s.ID == departament.ID);
            if (!hasAny)
            {
                throw new NotFoundException("ID not found!");
            }
            try
            {
                _context.Update(departament);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        #endregion

        #region Exists

        public async Task<bool> DepartamentExists(int id)
        {
            return await _context.Departament.AnyAsync(e => e.ID == id);
        }

        #endregion
    }
}
