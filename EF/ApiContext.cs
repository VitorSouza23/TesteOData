using Microsoft.EntityFrameworkCore;
using TesteOData.Models;

namespace TesteOData.EF 
{
    public class ApiContext : DbContext 
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
            
        }
        
        public DbSet<Student> Students { get; set; }
    }
}