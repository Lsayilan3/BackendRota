
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RotaGaleriRepository : EfEntityRepositoryBase<RotaGaleri, ProjectDbContext>, IRotaGaleriRepository
    {
        public RotaGaleriRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
