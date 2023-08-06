
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RotaRepository : EfEntityRepositoryBase<Rota, ProjectDbContext>, IRotaRepository
    {
        public RotaRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
