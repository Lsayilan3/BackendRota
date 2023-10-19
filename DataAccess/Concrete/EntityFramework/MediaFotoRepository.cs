
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class MediaFotoRepository : EfEntityRepositoryBase<MediaFoto, ProjectDbContext>, IMediaFotoRepository
    {
        public MediaFotoRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
