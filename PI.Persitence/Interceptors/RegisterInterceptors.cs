using Autofac;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace PI.Persitence.Interceptors
{
    public static class RegisterInterceptors
    {
        public static DbContextOptionsBuilder UseInterceptors(this DbContextOptionsBuilder builder, ISaveChangesInterceptor saveChangesInterceptor)
        {
            builder.AddInterceptors(saveChangesInterceptor);

            return builder;
        }
    }
}
