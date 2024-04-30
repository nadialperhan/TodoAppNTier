using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Udemy.TodoAppNTier.DataAccess.Contexts;
using Udemy.TodoAppNTier.DataAccess.UnitofWork;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using Udemy.TodoAppNTierBusiness.Mapping.AutoMapper;
using Udemy.TodoAppNTierBusiness.Services;
using Udemy.TodoAppNTierBusiness.ValidationRules;

namespace Udemy.TodoAppNTierBusiness.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuratio)
        {

            var connectionString = configuratio.GetConnectionString("DefaultConnection");
            //services.AddDbContext<TodoContext>(opt =>
            //{
            //    opt.UseSqlServer("server=(localdb)\\mssqllocaldb;database=TodoDb;integrated security=true;");
            //    opt.LogTo(Console.WriteLine, LogLevel.Information);
            //});
            services.AddDbContext<TodoContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
                opt.LogTo(Console.WriteLine, LogLevel.Information);
            });

            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new WorkProfile());
            });

            var mapper = configuration.CreateMapper();

            services.AddSingleton(mapper);

            services.AddScoped<IUow, Uow>();
            services.AddScoped<IWorkService, WorkService>();

            services.AddTransient<IValidator<WorkCreateDto>,WorkCreateDtoValidator> ();
            services.AddTransient<IValidator<WorkUpdatedDto>,WorkUpdateDtoValidator> ();

        }
    }
}
