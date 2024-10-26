using Lonk.Managers;
using Lonk.Models;
using Lonk.Services;

namespace Lonk
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1102:Make class static", Justification = "This is the entry point")]
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            LinkCtx.Init();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContextFactory<LinkCtx>();
            builder.Services.AddTransient<ILinkRepository, DbLinkRepo>();
            builder.Services.AddTransient<IBlocklistRepo, DbBlocklistRepo>();
            builder.Services.AddTransient<LinkManager>();

            builder.Services.AddSingleton<ILinkCleaner, DbLinkCleaner>();
            builder.Services.AddSingleton<BlocklistManager>();

            builder.Services.AddHostedService<CleanupService>();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseStatusCodePagesWithRedirects("/");

            app.MapControllers();

            app.MapRazorPages();

            app.Run();
        }
    }
}
