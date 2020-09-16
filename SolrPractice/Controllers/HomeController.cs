using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolrNet;
using SolrNet.Commands.Parameters;
using SolrPractice.core;
using SolrPractice.core.Domian;
using SolrPractice.Models;

namespace SolrPractice.Controllers
{
    public class HomeController : Controller
    {

        private ISolrIndexService<PhotoSearch> solr;

        ISolrIndexService<SolrPostModel> _solrIndexService;
        public HomeController(ISolrIndexService<PhotoSearch> solr, ISolrIndexService<SolrPostModel> solrIndexService)
        {
            this.solr = solr;
            _solrIndexService = solrIndexService;

        }

        //https://github.com/SolrNet/SolrNet/blob/master/Documentation/Querying.md
        public IActionResult Index()
        {

            SolrQueryResults<PhotoSearch> results = solr.GetAll();

            return View(results);
        }
        // https://github.com/SolrNet/SolrNet/blob/master/Documentation/CRUD.md
        public IActionResult Create()
        {
            PhotoSearch post = new PhotoSearch();
            post.PhotoId = "3410687";
            post.Name = new string[] { "New Added " + DateTime.Now };
            solr.AddUpdate(post);

            SolrQueryResults<PhotoSearch> results = solr.GetAll();

            return View("index", results);
        }
       public IActionResult Edit(string Id)
        {
            var post = new PhotoSearch();
            post.PhotoId = "3410688";
            post.Name = new string[] { "Updated " + DateTime.Now };
            post.id = Id;
            solr.AddUpdate(post);

            SolrQueryResults<PhotoSearch> results = solr.GetAll();

            return View("index", results);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region TetingWorks
        public void Create11(Post model)
        {

            var mypost_Data = new Post();
            mypost_Data.Id = "000100";
            mypost_Data.IsActive = false;

            mypost_Data.Price = 101;

            mypost_Data.Title = "TEsting DATA";
            mypost_Data.IsSold = DateTime.Today;
            _solrIndexService.AddUpdate(new SolrPostModel(model));
        }
        #endregion

    }
}
