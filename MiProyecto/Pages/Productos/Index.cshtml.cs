using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using MiProyecto.Models;

namespace MiProyecto.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _factory;
        public List<Producto> Productos { get; set; } = new();

        public IndexModel(IHttpClientFactory factory) => _factory = factory;

        public async Task OnGetAsync()
        {
            var client = _factory.CreateClient("API");
            var resp = await client.GetAsync("productos");
            if (resp.IsSuccessStatusCode)
            {
                var json = await resp.Content.ReadAsStringAsync();
                Productos = JsonSerializer.Deserialize<List<Producto>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
        }
    }
}