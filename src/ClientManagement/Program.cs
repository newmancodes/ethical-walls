using ClientManagement.Application;
using ClientManagement.Components;
using ClientManagement.Infrastructure.Postgres;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddValidatorsFromAssembly(ClientManagement.Application.AssemblyReference.Assembly);
builder.Services.AddMediatR(c =>
{
    c.AddOpenBehavior(typeof(ValidationBehavior<,>));
    c.RegisterServicesFromAssembly(ClientManagement.Application.AssemblyReference.Assembly);
});
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.AddNpgsqlDbContext<ClientDbContext>("ClientsDB");
builder.AddRabbitMQClient("messaging");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// app.MigrateDbContext<ClientDbContext>();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();