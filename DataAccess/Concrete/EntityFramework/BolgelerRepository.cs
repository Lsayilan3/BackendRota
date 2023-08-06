
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class BolgelerRepository : EfEntityRepositoryBase<Bolgeler, ProjectDbContext>, IBolgelerRepository
    {
        public BolgelerRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
