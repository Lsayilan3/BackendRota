﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class GunlerRepository : EfEntityRepositoryBase<Gunler, ProjectDbContext>, IGunlerRepository
    {
        public GunlerRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
