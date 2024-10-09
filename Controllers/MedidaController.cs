using CRUDWebEf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebEf.Controllers
{
    public class MedidaController : Controller
    {
        private readonly bd_pruebasdContext _DBContext;

        public MedidaController(bd_pruebasdContext context)
        {
            _DBContext = context;
        }
        public IActionResult ListaMedidas()
        {
            List<TbMedida> lista = _DBContext.TbMedidas.Where(d => d.ActivoMe == true).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult MedidaDetalle(int IdMedida)
        {
            TbMedida oMedida = new TbMedida();

            if (IdMedida != 0)
            {
                oMedida = _DBContext.TbMedidas.Find(IdMedida);
            }

            return View(oMedida);
        }

        [HttpPost]
        public IActionResult MedidaDetalle(TbMedida oMedida)
        {
            if (oMedida.IdMedida == 0)
            {
                oMedida.ActivoMe = true;
                _DBContext.TbMedidas.Add(oMedida);
            }
            else
            {
                oMedida.ActivoMe = true;
                _DBContext.TbMedidas.Update(oMedida);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("ListaMedidas", "Medida");
        }

        [HttpGet]
        public IActionResult EliminarMedida(int IdMedida)
        {
            TbMedida oMedida = new TbMedida();

            oMedida = _DBContext.TbMedidas.Find(IdMedida);

            return View(oMedida);
        }

        [HttpPost]
        public IActionResult EliminarMedida(TbMedida oMedida)
        {
            oMedida = _DBContext.TbMedidas.Find(oMedida.IdMedida);
            oMedida.ActivoMe = false;
            _DBContext.TbMedidas.Update(oMedida);

            _DBContext.SaveChanges();

            return RedirectToAction("ListaMedidas", "Medida");
        }
    }
}
