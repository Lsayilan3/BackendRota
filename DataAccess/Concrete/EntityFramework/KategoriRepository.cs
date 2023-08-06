
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class KategoriRepository : EfEntityRepositoryBase<Kategori, ProjectDbContext>, IKategoriRepository
    {
        public KategoriRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
