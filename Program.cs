using EFOnlineShop.Data;
using EFOnlineShop.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database
var dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
   options => options.UseSqlite($"Data Source={dbPath}"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//C
app.MapPost("/add_product", AddProduct);

//R
app.MapGet("/get_products", async (AppDbContext context)
   => await context.Products.ToListAsync());

//U
app.MapPost("/update_product", UpdateProduct);

//D
app.MapDelete("/delete_product", DeleteProduct);



async Task AddProduct(AppDbContext db, Product product)
{
	ArgumentNullException.ThrowIfNull(db, nameof(db));
	ArgumentNullException.ThrowIfNull(product, nameof(product));

	await db.Products.AddAsync(product);
	await db.SaveChangesAsync();
}

async Task UpdateProduct(AppDbContext db, Guid productId, Product product)
{
	ArgumentNullException.ThrowIfNull(db, nameof(db));
	ArgumentNullException.ThrowIfNull(productId, nameof(productId));
	ArgumentNullException.ThrowIfNull(product, nameof(product));

	var prod = await db.Products.SingleOrDefaultAsync(p => p.Id == productId);
	db.Entry(prod).CurrentValues.SetValues(product);
	await db.SaveChangesAsync();
}

async Task DeleteProduct(AppDbContext db, Guid productId)
{
	ArgumentNullException.ThrowIfNull(db, nameof(db));
	ArgumentNullException.ThrowIfNull(productId, nameof(productId));

	db.Products.Remove(await db.Products.SingleOrDefaultAsync(p => p.Id == productId));
	await db.SaveChangesAsync();
}


app.Run();
