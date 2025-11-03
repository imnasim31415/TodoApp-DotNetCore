var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<TodoApp.Data.TodoService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
