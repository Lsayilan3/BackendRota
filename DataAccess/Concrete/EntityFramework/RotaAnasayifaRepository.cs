
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RotaAnasayifaRepository : EfEntityRepositoryBase<RotaAnasayifa, ProjectDbContext>, IRotaAnasayifaRepository
    {
        public RotaAnasayifaRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
