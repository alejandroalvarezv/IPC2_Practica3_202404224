using Microsoft.AspNetCore.Mvc;
using MiAPI.Models;
using MiAPI.Services;

namespace MiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly InventarioService _service;
        public ProductosController(InventarioService service) => _service = service;

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var p = _service.GetById(id);
            return p == null ? NotFound() : Ok(p);
        }

        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            if (string.IsNullOrEmpty(producto.Nombre)) return BadRequest("Nombre requerido");
            return Ok(_service.Create(producto));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Producto producto)
        {
            return _service.Update(id, producto) ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _service.Delete(id) ? Ok() : NotFound();
        }
    }
}