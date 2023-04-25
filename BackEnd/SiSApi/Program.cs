using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SiSApi.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
			options.UseInMemoryDatabase("SisAppDatabase1")
			.ConfigureWarnings(
				cw => cw.Ignore(InMemoryEventId.TransactionIgnoredWarning)
				)
			);

//			builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
//	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);

builder.Services.AddCors(options =>
{
	options.AddPolicy("DefaultCorsPolicy", builder => builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
		.AllowAnyMethod()
		.AllowAnyHeader()
		.AllowCredentials());
});


builder.Services.AddControllers(
									options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
								)
								.AddJsonOptions(
									options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
								);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAppRepository, AppRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("DefaultCorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
