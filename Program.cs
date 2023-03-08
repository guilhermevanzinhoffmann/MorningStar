using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MorningStar.Data;
using MorningStar.Repositories.Interfaces;
using MorningStar.Repositories;
using NuGet.Protocol.Core.Types;
using MorningStar.Services.Interfaces;
using MorningStar.Services;
using Rotativa.AspNetCore;
using Microsoft.Build.Framework;

namespace MorningStar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MorningStarContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MorningStarContext") ?? throw new InvalidOperationException("Connection string 'MorningStarContext' not found.")));


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            RotativaConfiguration.Setup(builder.Environment.WebRootPath);
            builder.Services.AddScoped<IFabricanteRepository, FabricanteRepository>();
            builder.Services.AddScoped<IMercadoriaRepository, MercadoriaRepository>();
            builder.Services.AddScoped<IEntradaRepository, EntradaRepository>();
            builder.Services.AddScoped<ISaidaRepository, SaidaRepository>();

            builder.Services.AddScoped<IFabricanteService, FabricanteService>();
            builder.Services.AddScoped<IMercadoriaService, MercadoriaService>();
            builder.Services.AddScoped<IEntradaService, EntradaService>();
            builder.Services.AddScoped<ISaidaService, SaidaService>();
            builder.Services.AddScoped<IGraficoService, GraficoService>();

            var app = builder.Build();
            
            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            
            app.UseStatusCodePagesWithReExecute("/Erro/{0}");
            
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            app.Run();
        }
    }
}