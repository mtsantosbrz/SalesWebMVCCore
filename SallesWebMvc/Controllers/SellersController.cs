using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SallesWebMvc.Models;
using SallesWebMvc.Models.ViewModels;
using SallesWebMvc.Services;
using SallesWebMvc.Services.Exceptions;

namespace SallesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartamentService _departamentService;
        public SellersController(SellerService sellerService, DepartamentService departamentService)
        {
            _sellerService = sellerService;
            _departamentService = departamentService;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departamentService.FindAll();
            var viewmodel = new SellerFormViewModel { Departaments = departments };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departaments = _departamentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departaments = departaments };
                return View(viewModel);
            }

            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int?id)
        {
            if(id==null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided!" });
            }

            var resp = _sellerService.FindByID(id.Value);
            if(resp == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found!" });
            }

            return View(resp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int?id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided!" });
            }

            var resp = _sellerService.FindByID(id.Value);
            if (resp == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found!" });
            }

            return View(resp);
        }

        public IActionResult Edit(int?id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided!" });
            }

            var resp = _sellerService.FindByID(id.Value);
            if (resp == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found!" });
            }

            List<Departament> departaments = _departamentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = resp, Departaments = departaments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departaments = _departamentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departaments = departaments };
                return View(viewModel);
            }

            if(id!=seller.ID)
            {
                return RedirectToAction(nameof(Error), new { message = "ID mismacth!" });
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
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