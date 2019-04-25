using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SallesWebMvc.Models;
using SallesWebMvc.Models.ViewModels;
using SallesWebMvc.Services;
using SallesWebMvc.Services.Exceptions;

namespace SallesWebMvc.Controllers
{
    public class DepartamentsController : Controller
    {
        private readonly DepartamentService _departamentService;

        public DepartamentsController(DepartamentService departamentService)
        {
            _departamentService = departamentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _departamentService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided!" });
            }

            var resp = await _departamentService.FindByIDAsync(id.Value);
            if (resp == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found!" });
            }

            return View(resp);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Departament departament)
        {
            if (!ModelState.IsValid)
            {
                return View(departament);
            }

            await _departamentService.InsertAsync(departament);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided!" });
            }

            var resp = await _departamentService.FindByIDAsync(id.Value);
            if (resp == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found!" });
            }
            return View(resp);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Departament departament)
        {
            if (!ModelState.IsValid)
            {
                return View(departament);
            }

            if (await DepartamentExists(id))
            {
                return RedirectToAction(nameof(Error), new { message = "ID mismacth!" });
            }

            try
            {
                await _departamentService.UpdateAsync(departament);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided!" });
            }

            var resp = await _departamentService.FindByIDAsync(id.Value);
            if (resp == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found!" });
            }

            return View(resp);
        }

        // POST: Departaments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _departamentService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        private async Task<bool> DepartamentExists(int id)
        {
            return await _departamentService.DepartamentExists(id);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
