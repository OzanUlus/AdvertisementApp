using AdvertisementApp.DataAccess.Context;
using AdvertisementApp.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Extension
{
    public static class AdvertisementAppBusinessExtension
    {
        public static void AddBusinesExtension(this IServiceCollection services, string SqlCon,Assembly fromAssembly) 
        {
            #region Db
            services.AddDbContext<AdvertisementContext>(opt =>
            {
                opt.UseSqlServer(SqlCon);
            });
            #endregion

            #region AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly(),fromAssembly);
            #endregion

            #region Uow
            services.AddScoped<IUow, Uow>();
            #endregion

            #region Services

            #endregion

            #region FluentValidation

            #endregion


        }
    }
}
