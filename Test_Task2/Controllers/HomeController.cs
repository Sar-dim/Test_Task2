using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceApplication;
using ServiceB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Test_Task2.Models;

namespace Test_Task2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISubdivisionService _subdivisionService;

        public HomeController(ISubdivisionService subdivisionService)
        {
            _subdivisionService = subdivisionService;
        }

        public async Task<IActionResult> Index()
        {
            var subdivisionModels = new List<SubdivisionModel>();
            var subdivisions = await _subdivisionService.GetAll();
            subdivisions.ForEach(x => subdivisionModels.Add(
                new SubdivisionModel(x.id, x.name, x.status == 0 ? false : true, x.ownerid)));
            _subdivisionService.ChangeDivisionsStatusInfinity();
            return View("Index", subdivisionModels);
        }

        public async Task<IActionResult> Synchronize()
        {
            List<subdivision> subdivision = JsonConvert.DeserializeObject<List<subdivision>>
                (System.IO.File.ReadAllText("subdivisions.json"));
            await _subdivisionService.InsertSubdivisions(subdivision);
            return await Index();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
