using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemasWeb.Areas.Categorias.Models;
using SistemasWeb.Areas.Principal.Controllers;
using SistemasWeb.Data;
using SistemasWeb.Library;
using SistemasWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemasWeb.Areas.Categorias.Controllers
{
    [Area("Categorias")]
    //[Authorize]
    public class CategoriasController : Controller
    {
        //[Route("Categorias/Categoria")]

        private TCategoria _categoria;
        private LCategorias _lcategorias;
        private SignInManager<IdentityUser> _signInManager;
        private static DataPaginador<TCategoria> models;
        public CategoriasController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _lcategorias = new LCategorias(context);
        }

        public IActionResult Categoria()
        {
            if (_signInManager.IsSignedIn(User))
            {
                models = new DataPaginador<TCategoria>
                {
                    Input = new TCategoria()
                };
                return View(models);
            }
            return RedirectToAction(nameof(PrincipalController.Index), "Index");
        }

        [HttpPost]
        public String GetCategorias(DataPaginador<TCategoria> model)
        {
            if (model.Input.Nombre != null && model.Input.Descripcion != null)
            {
                var data = _lcategorias.RegistrarCategoria(model.Input);
                return JsonConvert.SerializeObject(data);
            }
            else
            {
                return "adjakdhakjd";
            }
        }
    }
}
