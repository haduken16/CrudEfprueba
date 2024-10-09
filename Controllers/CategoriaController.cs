using ClosedXML.Excel;
using CRUDWebEf.Models;
using CRUDWebEf.Models.ViewsModel;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace CRUDWebEf.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly bd_pruebasdContext _DBContext;

        public CategoriaController(bd_pruebasdContext context)
        {
            _DBContext = context;
        }

        public IActionResult ListaCategorias()
        {
            List<TbCategoria> lista = _DBContext.TbCategorias.Where(d => d.ActivoCa == true).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult CategoriaDetalle(int IdCategoria)
        {
            TbCategoria oCategoria = new TbCategoria();

            if (IdCategoria != 0)
            {
                oCategoria = _DBContext.TbCategorias.Find(IdCategoria);
            }

            return View(oCategoria);
        }

        [HttpPost]
        public IActionResult CategoriaDetalle(TbCategoria oCategoria)
        {
            if (oCategoria.IdCategoria == 0)
            {
                oCategoria.ActivoCa = true;
                _DBContext.TbCategorias.Add(oCategoria);
            }
            else
            {
                oCategoria.ActivoCa = true;
                _DBContext.TbCategorias.Update(oCategoria);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("ListaCategorias", "Categoria");
        }

        [HttpGet]
        public IActionResult EliminarCategoria(int IdCategoria)
        {
            TbCategoria oCategoria = new TbCategoria();

            oCategoria = _DBContext.TbCategorias.Find(IdCategoria);

            return View(oCategoria);
        }

        [HttpPost]
        public IActionResult EliminarCategoria(TbCategoria oCategoria)
        {
            oCategoria = _DBContext.TbCategorias.Find(oCategoria.IdCategoria);
            oCategoria.ActivoCa = false;
            _DBContext.TbCategorias.Update(oCategoria);

            _DBContext.SaveChanges();

            return RedirectToAction("ListaCategorias", "Categoria");
        }
    }
}
