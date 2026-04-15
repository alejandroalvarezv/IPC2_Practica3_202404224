using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;
using MiProyecto.Models;

namespace MiProyecto.Pages.Productos
{
    public class CrearModel : PageModel
    {
        private readonly IHttpClientFactory _factory;
        public CrearModel(IHttpClientFactory factory) => _factory = factory;

        public async Task<IActionResult> OnPostAsync(Producto producto)
        {
            var client = _factory.CreateClient("API");
            var json = JsonSerializer.Serialize(producto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync("productos", content);
            return RedirectToPage("/Productos/Index");
        }
    }
}