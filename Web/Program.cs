using Aplicacion.Cargo.CasosDeUso;
using Aplicacion.Cargo.DTOs;
using Aplicacion.Cargo.Mappers;
using Aplicacion.Concepto.CasosDeUso;
using Aplicacion.Concepto.DTOs;
using Aplicacion.Concepto.Mappers;
using Aplicacion.Estudiante.CasosDeUso;
using Aplicacion.Estudiante.DTOs;
using Aplicacion.Estudiante.Mappers;
using Aplicacion.Interfaces.Mapper;
using Aplicacion.Interfaces.Repository;
using Aplicacion.Interfaces.UseCase;
using Aplicacion.Tutor.CasosDeUso;
using Aplicacion.Tutor.DTOs;
using Aplicacion.Tutor.Mappers;
using Data;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//DbContext
string dbPath = Path.Combine(builder.Environment.ContentRootPath, "ColegioCxC.db");

builder.Services.AddDbContext<ColegioDbContext>(options =>
{
    options.UseSqlite($"Data Source={dbPath}");
});

//Mappers
builder.Services.AddScoped<IMapper<TutorDto, TutorEntity>, TutorDtoATutorEntityMapper>();
builder.Services.AddScoped<IMapper<TutorEntity, TutorDto>, TutorEntityATutorDtoMapper>();

builder.Services.AddScoped<IMapper<EstudianteDto, EstudianteEntity>, EstudianteDtoToEntityMapper>();
builder.Services.AddScoped<IMapper<EstudianteEntity, EstudianteDto>, EstudianteEntityToDtoMapper>();

builder.Services.AddScoped<IMapper<ConceptoDto, ConceptoEntity>, ConceptoDtoToEntityMapper>();
builder.Services.AddScoped<IMapper<ConceptoEntity, ConceptoDto>, ConceptoEntityToDtoMapper>();

builder.Services.AddScoped<IMapper<CargoEntity, CargoDto>, CargoEntityToDtoMapper>();

//Casos de uso
builder.Services.AddScoped<IUpsertUseCase<TutorDto, TutorEntity>, AgregarActualizarTutorUseCase>();
builder.Services.AddScoped<IReadUseCase<TutorDto, TutorEntity>, ObtenerTutorUseCase>();
builder.Services.AddScoped<ISearchUseCase<TutorDto, TutorEntity>, BuscarTutorUseCase>();

builder.Services.AddScoped<IUpsertUseCase<EstudianteDto, EstudianteEntity>, AgregarActualizarEstudianteUseCase>();
builder.Services.AddScoped<IReadUseCase<EstudianteDto, EstudianteEntity>, ObtenerEstudianteUseCase>();

builder.Services.AddScoped<ICreateUseCase<ConceptoDto, ConceptoEntity>, AgregarConceptoUseCase>();
builder.Services.AddScoped<IReadUseCase<ConceptoDto, ConceptoEntity>, ObtenerConceptoUseCase>();

builder.Services.AddScoped<ICreateUseCase<CargoInsertDto, CargoEntity>, AgregarCargosMasivosUseCase>();
builder.Services.AddScoped<IReadUseCase<CargoDto, CargoEntity>, ObtenerCargoUseCase>();

//Repository
builder.Services.AddScoped<ICreateRepository<TutorEntity>, TutorRepository>();
builder.Services.AddScoped<IReadRepository<TutorEntity>, TutorRepository>();
builder.Services.AddScoped<IUpdateRepository<TutorEntity>, TutorRepository>();
builder.Services.AddScoped<ISearchRepository<TutorEntity>, TutorRepository>();

builder.Services.AddScoped<ICreateRepository<EstudianteEntity>, EstudianteReposiory>();
builder.Services.AddScoped<IReadRepository<EstudianteEntity>, EstudianteReposiory>();
builder.Services.AddScoped<IUpdateRepository<EstudianteEntity>, EstudianteReposiory>();
builder.Services.AddScoped<IRangeValidateRepository<EstudianteEntity>, EstudianteReposiory>();

builder.Services.AddScoped<ICreateRepository<ConceptoEntity>, ConceptoRepository>();
builder.Services.AddScoped<IReadRepository<ConceptoEntity>, ConceptoRepository>();

builder.Services.AddScoped<ICreateRangeRepository<CargoEntity>, CargoRepository>();
builder.Services.AddScoped<IReadRepository<CargoEntity>, CargoRepository>();





var culture = new CultureInfo("es-DO");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
