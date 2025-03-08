using MaleFashion.Server.Data;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;

namespace MaleFashion.Server.Repositories.Implementations
{
    public class SubCategoryRepository : GenericRepository<ApplicationDbContext, SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
