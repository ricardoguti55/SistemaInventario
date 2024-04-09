﻿using System.ComponentModel.DataAnnotations;

namespace SistemaInventario.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nombre es Requerido")]
        [MaxLength(60, ErrorMessage ="Nombre debe ser Máximo 60 Caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripción es Requerido")]
        [MaxLength(100, ErrorMessage = "Descripción debe ser Máximo 100 Caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado es Requerido")]
        public bool Estado { get; set; }
    }
}
