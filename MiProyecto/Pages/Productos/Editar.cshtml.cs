using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;
using MiProyecto.Models;

namespace MiProyecto.Pages.Productos
{
    public class EditarModel : PageModel
    {
        private readonly IHttpClientFactory _factory;
        public Producto Producto { get; set; } = new();

        public EditarModel(IHttpClientFactory factory) => _factory = factory;

        public async Task OnGetAsync(int id)
        {
            var client = _factory.CreateClient("API");
            var resp = await client.GetAsync($"productos/{id}");
            if (resp.IsSuccessStatusCode)
            {
                var json = await resp.Content.ReadAsStringAsync();
                Producto = JsonSerializer.Deserialize<Producto>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
        }

        public async Task<IActionResult> OnPostAsync(Producto producto)
        {
            var client = _factory.CreateClient("API");
            var json = JsonSerializer.Serialize(producto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync($"productos/{producto.Id}", content);
            return RedirectToPage("/Productos/Index");
        }
    }
}