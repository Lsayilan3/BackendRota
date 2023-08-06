
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class TeamRepository : EfEntityRepositoryBase<Team, ProjectDbContext>, ITeamRepository
    {
        public TeamRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
