using Cancellations;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<PersonsDbContext>(b =>
{
    b.UseSqlite("Data Source=persons.db");
});
var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<PersonsDbContext>();
await dbContext.Database.EnsureCreatedAsync();
await dbContext.Database.MigrateAsync();
await dbContext.EnsureSeedData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/persons", async(PersonsDbContext dbContext) =>
{
    var persons = await dbContext.Persons.ToListAsync();
    await Task.Delay(TimeSpan.FromSeconds(10));
    persons = await dbContext.Persons.ToListAsync();
    return persons;
})
.WithName("GetPersonsWithoutCancellationToken");

app.MapGet("/personsc", async (PersonsDbContext dbContext, CancellationToken cancellationToken) =>
{
    var persons = await dbContext.Persons.ToListAsync(cancellationToken);
    await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
    persons = await dbContext.Persons.ToListAsync(cancellationToken);
    return persons;
})
.WithName("GetPersonsWithCancellationToken");

await app.RunAsync();
