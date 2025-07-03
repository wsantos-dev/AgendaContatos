﻿using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Contato> Contatos { get; set; }
    }
}
