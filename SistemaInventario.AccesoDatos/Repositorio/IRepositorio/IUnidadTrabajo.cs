﻿namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable   // Permite deshacerse de cualquier recurso que ya no se esté usando
    {

        IBodegaRepositorio Bodega { get; }
        ICategoriaRepositorio Categoria { get; }

        IMarcaRepositorio Marca { get; }
        IProductoRepositorio Producto { get; }

        IUsuarioAplicacionRepositorio UsuarioAplicacion { get; }

        IBodegaProductoRepositorio BodegaProducto { get; }

        IInventarioRepositorio Inventario { get; }

        IInventarioDetalleRepositorio InventarioDetalle { get; }

        IKardexInventarioRepositorio KardexInventario { get; }

        ICompaniaRepositorio Compania { get; }

        ICarroCompraRepositorio CarroCompra { get; }

        IOrdenRepositorio Orden { get; }

        IOrdenDetalleRepositorio OrdenDetalle { get; }

        Task Guardar();
    }
}
