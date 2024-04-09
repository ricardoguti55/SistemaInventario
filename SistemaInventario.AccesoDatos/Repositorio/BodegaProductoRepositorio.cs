using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class BodegaProductoRepositorio : Repositorio<BodegaProducto>, IBodegaProductoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public BodegaProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(BodegaProducto bodegaProducto)
        {
            var bodegaProductoDB = _db.BodegasProductos.FirstOrDefault(b => b.Id == bodegaProducto.Id);
            if (bodegaProductoDB != null)
            {

                bodegaProductoDB.Cantidad = bodegaProducto.Cantidad;

                _db.SaveChanges();
            }
        }

    }
}
