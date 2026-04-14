using System.Text.Json;
using MiAPI.Models;

namespace MiAPI.Services
{
    public class InventarioService
    {
        private readonly string _filePath = "inventario.json";

        public List<Producto> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Producto>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Producto>>(json) ?? new List<Producto>();
        }

        public void SaveAll(List<Producto> productos)
        {
            var json = JsonSerializer.Serialize(productos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public Producto? GetById(int id) => GetAll().FirstOrDefault(p => p.Id == id);

        public Producto Create(Producto producto)
        {
            var lista = GetAll();
            producto.Id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1;
            lista.Add(producto);
            SaveAll(lista);
            return producto;
        }

        public bool Update(int id, Producto actualizado)
        {
            var lista = GetAll();
            var idx = lista.FindIndex(p => p.Id == id);
            if (idx == -1) return false;
            actualizado.Id = id;
            lista[idx] = actualizado;
            SaveAll(lista);
            return true;
        }

        public bool Delete(int id)
        {
            var lista = GetAll();
            var producto = lista.FirstOrDefault(p => p.Id == id);
            if (producto == null) return false;
            lista.Remove(producto);
            SaveAll(lista);
            return true;
        }
    }
}