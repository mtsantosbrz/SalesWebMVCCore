﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SallesWebMvc.Services;

namespace SallesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordsService _salesRecordsService;
        public SalesRecordsController(SalesRecordsService salesRecordsService)
        {
            _salesRecordsService = salesRecordsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(DateTime? minDate, DateTime? maxDate)
        {
            if(!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if(!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy/MM/dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy/MM/dd");
            var list = await _salesRecordsService.FindByDateAsync(minDate, maxDate);
            return  View(list);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy/MM/dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy/MM/dd");
            var list = await _salesRecordsService.FindByDateGroupingAsync(minDate, maxDate);
            return View(list);
        }
    }
}