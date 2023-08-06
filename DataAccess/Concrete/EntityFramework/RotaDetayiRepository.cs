
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RotaDetayiRepository : EfEntityRepositoryBase<RotaDetayi, ProjectDbContext>, IRotaDetayiRepository
    {
        public RotaDetayiRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
