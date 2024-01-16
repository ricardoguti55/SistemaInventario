using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable   // Permite deshacerse de cualquier recurso que ya no se esté usando
    {

        IBodegaRepositorio Bodega { get; }
        ICategoriaRepositorio Categoria { get; }

        IMarcaRepositorio Marca { get; }
        IProductoRepositorio Producto { get; }

        Task Guardar();
    }
}
