using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SmartApartmentWebAPP.Services;
using System.Collections.Generic;
using System.Linq;

namespace SmartApartmentWebAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ISearchService searchService;

        public HomeController(ILogger<HomeController> logger, ISearchService searchService)
        {
            this.logger = logger;
            this.searchService = searchService;
        }

        [HttpGet]
    
        public IActionResult Index( string query="",string scope="")
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();

            var marketList = searchService.getMarketList().result;
            if (marketList!=null) {

                ViewData["selection"] = marketList.Select(a =>
                                      new SelectListItem
                                      {
                                          Value = a.ToString(),
                                          Text = a.ToString(),
                                      }).ToList(); 
            }

       
             var response = searchService.getSearchResult(query, scope);

            if (!string.IsNullOrEmpty(query))

                return View(response);
            else return View();
        }

        [HttpPost]
        [Route("Search")]
        public ActionResult Search(string query, string scope)
        {
          
            var response = searchService.getSearchResult(query,scope);

            return new JsonResult(response);
        }
       
        public ActionResult Index2(string query,string scope)
        {
            return RedirectToActionPermanent("Index", new { query=query, scope=scope});
        }


    }
}
