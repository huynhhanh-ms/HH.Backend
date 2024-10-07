using System;
using System.Collections.Generic;
using HH.Domain.Common;
using HH.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HH.Persistence.DbContexts;

public partial class HhDatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseLazyLoadingProxies(useLazyLoadingProxies: false)
        .UseNpgsql(AppConfig.ConnectionStrings.DefaultConnection)
        .LogTo(Console.WriteLine);

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {

    }
}
