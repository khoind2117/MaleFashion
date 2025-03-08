using MaleFashion.Server.Data;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;

namespace MaleFashion.Server.Repositories.Implementations
{
    public class SizeRepository : GenericRepository<ApplicationDbContext, Size>, ISizeRepository
    {
        public SizeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
