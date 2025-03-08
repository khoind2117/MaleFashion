using MaleFashion.Server.Data;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;

namespace MaleFashion.Server.Repositories.Implementations
{
    public class ColorRepository : GenericRepository<ApplicationDbContext, Color>, IColorRepository
    {
        public ColorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
