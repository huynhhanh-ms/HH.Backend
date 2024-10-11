using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HH.Domain.Common;
using HH.Domain.Common.Entity;
using HH.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HH.Persistence.DbContexts;

public partial class HhDatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseLazyLoadingProxies(useLazyLoadingProxies: false)
        .UseNpgsql(AppConfig.ConnectionStrings.DefaultConnection)
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        //ApplyFilterForAllEntities(modelBuilder);
    }

    private void ApplyFilterForAllEntities(ModelBuilder modelBuilder)
    {
        Expression<Func<IDeletable, bool>> filtDeletedEntityExpr = x => !x.IsDeleted;
        foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IDeletable).IsAssignableFrom(mutableEntityType.ClrType))
            {
                var parameter = Expression.Parameter(mutableEntityType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filtDeletedEntityExpr.Parameters.First(), parameter, filtDeletedEntityExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);

                mutableEntityType.SetQueryFilter(lambdaExpression);
            }
        }
    }

}
