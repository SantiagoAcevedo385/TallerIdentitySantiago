using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TallerIndentity.Models;
using TallerIndentity.Models.ViewModels;

namespace GraficaSantiago.Controllers
{
    public class HomeController : Controller
    {
        private readonly TallerIdentityContext _dbcontext;

        public HomeController(TallerIdentityContext context)
        {
            _dbcontext = context;
        }


        public IActionResult resumenVenta()
        {
            DateTime fechaInicio = DateTime.Now;
            fechaInicio = fechaInicio.AddDays(-30);

            List<ViewVenta> Lista = (from data in _dbcontext.Ventas
                                    where data.FechaVenta.Date >= fechaInicio.Date
                                    group data by data.FechaVenta.Date into grupo
                                    select new ViewVenta
                                    {
                                        fecha = grupo.Key.ToString("dd/MM/yy"),
                                        cantidad = grupo.Count(),
                                    }
                                    ).ToList();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        public IActionResult resumenProducto()
        {
            List<ViewProducto> Lista = (from tbdetalleventa in _dbcontext.Productos.ToList()
                                             group tbdetalleventa by tbdetalleventa.Nombre into grupo
                                             orderby grupo.Count() descending
                                             select new ViewProducto
                                             {
                                                 producto = grupo.Key,
                                                 cantidad = grupo.Count(),

                                             }
                                            ).Take(4).ToList();
            //return View(Lista);
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        public IActionResult Index()
        {
            return View();
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
    }
}