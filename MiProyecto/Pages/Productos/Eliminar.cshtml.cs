using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using MiProyecto.Models;

namespace MiProyecto.Pages.Productos
{
    public class EliminarModel : PageModel
    {
        private readonly IHttpClientFactory _factory;
        public Producto Producto { get; set; } = new();

        public EliminarModel(IHttpClientFactory factory) => _factory = factory;

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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var client = _factory.CreateClient("API");
            await client.DeleteAsync($"productos/{id}");
            return RedirectToPage("/Productos/Index");
        }
    }
}