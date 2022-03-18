using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using SmartApartmentWebAPP.Models;
using SmartApartmentWebAPP.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartmentWebAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchService searchService;

        public HomeController(ILogger<HomeController> logger, ISearchService searchService)
        {
            _logger = logger;
            this.searchService = searchService;
        }

        [HttpGet]
        //[Route("Index")]
        public IActionResult Index( string query="",string scope=null)
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();

            // new SelectList(); 
            var list = new List<string>()
            {
                "San Francisco","Orange County","Sacramento","Los Angeles","Inland Empire","San Diego","SanDiego","Augusta","Macon","Georgia","Lufkin","DFW","Houston","El Paso","College Station","Port Arthur","Austin","San Antonio","I-35 Corridor","San antonio","Harlingen","Las Vegas","Texas","dfw","Midland Odessa","Longview","Santa Teresa","Mission","Beaumont","Tacoma","Corpus Christi","Southwest Florida","Lubbock","Bay City","Nacogdoches","Orlando","Amarillo","Abilene","Victoria","Brenham","Laredo","Wichita Falls","San Francisco","Orange County","Sacramento","Los Angeles","Inland Empire","SanDiego","Augusta","Savannah","Atlanta","Macon","Georgia","Lufkin","DFW","Houston","El Paso","College Station","Port Arthur","Austin","San Antonio","I-35 Corridor","san antonio","San antonio","Harlingen","Las Vegas","Texas","dfw","Midland Odessa","Longview","Mission","Beaumont","Tacoma","Corpus Christi","Southwest Florida","Lubbock","Bay City","Nacogdoches","Tyler","San Diego"
            }.Distinct();


            selectListItemList.Add(new SelectListItem()
            {
                Text = "select one",
                Value = null
            });
            foreach (var ist in list )
            {
                selectListItemList.Add(new SelectListItem()
                {
                    Text = ist,
                    Value = ist
                });
                
             }


            ViewData["selection"] = selectListItemList;

            
            
             var response = searchService.getSearchResult(query, scope);

            if (!string.IsNullOrEmpty(query))

                return View(response);
            else return View();
        }

        [HttpPost]
        [Route("Search")]
        public ActionResult Search(string query)
        {
            string scope = null;
            var response = searchService.getSearchResult(query,scope);

            return new JsonResult(response);
        }
        //[HttpPost]
        //[Route("Index")]
        public ActionResult Index2(string query,string scope)
        {
            
           

            return RedirectToActionPermanent("Index", new { query=query, scope=scope});
        }


    }
}
