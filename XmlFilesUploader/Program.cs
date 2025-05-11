using DataAccessLayer.DbContexts;
using Microsoft.EntityFrameworkCore;
using XmlFilesUploader.Application.Interfaces;
using XmlFilesUploader.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly("DataAccessLayer")
    ));

builder.Services.AddScoped<IXmlSectionLoader, XmlSectionLoader>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
