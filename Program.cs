// Program.cs
using SimpleBlockchainAPI.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// --- Register the BlockchainService as a Singleton ---
builder.Services.AddSingleton<BlockchainService>();

// --- Add CORS Policy ---
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

// --- Use the CORS policy ---
app.UseCors(); 

app.UseAuthorization();

app.MapControllers();

app.Run();