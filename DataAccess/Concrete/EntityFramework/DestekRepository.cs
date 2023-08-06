
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class DestekRepository : EfEntityRepositoryBase<Destek, ProjectDbContext>, IDestekRepository
    {
        public DestekRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
