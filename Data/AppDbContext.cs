namespace EFOnlineShop.Data
{
    using EFOnlineShop.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
	{
		//Список таблиц:
		public DbSet<Product> Products => Set<Product>();

		public AppDbContext(
			DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}
	}

}
