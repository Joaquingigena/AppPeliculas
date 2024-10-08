﻿using APIPeliculas.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace APIPeliculas.Entidades
{
    public class Genero : IId
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; } = null!;
    }
}
