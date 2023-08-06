
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ResimTipiRepository : EfEntityRepositoryBase<ResimTipi, ProjectDbContext>, IResimTipiRepository
    {
        public ResimTipiRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
