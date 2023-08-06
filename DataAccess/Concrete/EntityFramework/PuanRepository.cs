
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class PuanRepository : EfEntityRepositoryBase<Puan, ProjectDbContext>, IPuanRepository
    {
        public PuanRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
