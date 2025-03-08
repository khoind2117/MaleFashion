using MaleFashion.Server.Data;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;

namespace MaleFashion.Server.Repositories.Implementations
{
    public class MainCategoryRepository : GenericRepository<ApplicationDbContext, MainCategory>, IMainCategoryRepository
    {
        public MainCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
