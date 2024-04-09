namespace SistemaInventario.Modelos.Especificaciones
{
    public class PagesList<T> : List<T>
    {
        public MetaData MetaData { get; set; }
        public PagesList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize) // Por ejemplo, 1.5 lo transforma a 2, para no trabajar con decimales
            };
            AddRange(items); // Agrega los elementos de la colección al final de la lista
        }

        public static PagesList<T> ToPagesList(IEnumerable<T> entidad, int pageNumber, int pageSize) 
        {
            var count = entidad.Count();
            var items = entidad.Skip((pageNumber -1) * pageSize).Take(pageSize).ToList();
            return new PagesList<T>(items, count, pageNumber, pageSize);
        }

    }
}
