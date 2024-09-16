using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Common
{
    public class AppContainter
    {
        private static IContainer? _container = null;

        public static IContainer Container
        {
            get
            {
                return _container;
            }
        }

        public static void Load(ContainerBuilder builder)
        {
            _container = builder.Build();
        }
       
    }
}
