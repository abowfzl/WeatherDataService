using DigipayTask.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DigipayTask.Data;

public class WeatherDbContext(DbContextOptions<WeatherDbContext> options) : DbContext(options)
{
	public DbSet<WeatherInfo> WeatherInfos { get; set; }
}
