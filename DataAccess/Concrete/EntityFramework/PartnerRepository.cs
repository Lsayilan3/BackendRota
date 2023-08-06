
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class PartnerRepository : EfEntityRepositoryBase<Partner, ProjectDbContext>, IPartnerRepository
    {
        public PartnerRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
