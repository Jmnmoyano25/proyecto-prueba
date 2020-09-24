using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace proyecto_prueba.Controllers
{
    public class PanelUsuarioController : Controller
    {
        public IActionResult MisPedidos()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
