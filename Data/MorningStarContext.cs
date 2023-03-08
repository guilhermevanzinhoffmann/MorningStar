using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MorningStar.Models;

namespace MorningStar.Data
{
    public class MorningStarContext : DbContext
    {
        public MorningStarContext (DbContextOptions<MorningStarContext> options)
            : base(options)
        {
        }

        public DbSet<MorningStar.Models.Fabricante> Fabricantes { get; set; } = default!;

        public DbSet<MorningStar.Models.Mercadoria> Mercadorias { get; set; }

        public DbSet<MorningStar.Models.Entrada> Entradas { get; set; }

        public DbSet<MorningStar.Models.Saida> Saidas { get; set; }
    }
}
