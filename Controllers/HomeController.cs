using CRUDWebEf.Models.ViewsModel;
using CRUDWebEf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Data.Common;
using System.Text;
using ClosedXML;
using ClosedXML.Excel;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CRUDWebEf.Controllers
{
    public class HomeController : Controller
    {
        private readonly bd_pruebasdContext _DBContext;

        public HomeController(bd_pruebasdContext context)
        {
            _DBContext = context;
        }

        public IActionResult Index()
        {
            List<TbProducto> lista = _DBContext.TbProductos.Include(c => c.oMedida).Include(c => c.oCategoria).Where(e => e.ActivoPr == true).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult ProductoDetalle(int IdProducto)
        {
            ProductoVM oProductoVM = new ProductoVM()
            {
                oProducto = new TbProducto(),
                olistaMedidas = _DBContext.TbMedidas.Select(medidas => new SelectListItem()
                {
                    Text = medidas.DescripcionMe,
                    Value = medidas.IdMedida.ToString()
                }).ToList(),
                olistaCategorias = _DBContext.TbCategorias.Select(categoria => new SelectListItem()
                {
                    Text = categoria.DescripcionCa,
                    Value = categoria.IdCategoria.ToString()
                }).ToList()
            };

            if(IdProducto != 0)
            {
                oProductoVM.oProducto = _DBContext.TbProductos.Find(IdProducto);
            }

            return View(oProductoVM);
        }

        [HttpPost]
        public IActionResult ProductoDetalle(ProductoVM oProductoModel)
        {
            if (oProductoModel.oProducto.IdProducto == 0)
            {
                oProductoModel.oProducto.ActivoPr = true;
                oProductoModel.oProducto.FechaCreacion = DateTime.Now;
                _DBContext.TbProductos.Add(oProductoModel.oProducto);
            }
            else
            {
                _DBContext.TbProductos.Update(oProductoModel.oProducto);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Eliminar(int IdProducto)
        {
            ProductoVM oProductoVM = new ProductoVM();

            oProductoVM.oProducto = _DBContext.TbProductos.Find(IdProducto);

            return View(oProductoVM);
        }

        [HttpPost]
        public IActionResult Eliminar(ProductoVM oProductoModel)
        {
            oProductoModel.oProducto = _DBContext.TbProductos.Find(oProductoModel.oProducto.IdProducto);
            oProductoModel.oProducto.ActivoPr = false;
            _DBContext.TbProductos.Update(oProductoModel.oProducto);            

            _DBContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EliminadoFisico(int IdProducto)
        {
            TbProducto oProducto = _DBContext.TbProductos.Include(c => c.oMedida).Include(c => c.oCategoria).Where(e => e.IdProducto == IdProducto).FirstOrDefault();

            return View(oProducto);
        }

        [HttpPost]
        public IActionResult EliminadoFisico(TbProducto oProducto)
        {
            _DBContext.TbProductos.Remove(oProducto);
            _DBContext.SaveChanges();

            return View(oProducto);
        }

        public ActionResult Descargar()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection("Server=(local); DataBase=bd_pruebasd;Integrated Security=true"))
            {
                StringBuilder consulta = new StringBuilder();
                consulta.AppendLine("SELECT a.id_producto, a.descripcion_pr, a.marca, b.descripcion_me as medida, c.descripcion_ca as categoria, a.stock, a.precio, a.id_medida, a.id_categoria from tb_productos a inner join tb_medidas b on b.id_medida = a.id_medida inner join tb_categorias c on c.id_categoria = a.id_categoria where a.activo_pr = 1; ");
                SqlCommand cmd = new SqlCommand(consulta.ToString(), cn);

                cmd.CommandType = CommandType.Text;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            dt.TableName = "Datos";

            using (XLWorkbook libro = new XLWorkbook())
            {
                var hoja = libro.Worksheets.Add(dt);
                hoja.ColumnsUsed().AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    libro.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte " + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }
    }
}
