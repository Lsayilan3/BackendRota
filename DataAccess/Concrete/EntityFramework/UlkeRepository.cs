
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class UlkeRepository : EfEntityRepositoryBase<Ulke, ProjectDbContext>, IUlkeRepository
    {
        public UlkeRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
