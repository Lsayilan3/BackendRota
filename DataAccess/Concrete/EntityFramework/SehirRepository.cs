﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class SehirRepository : EfEntityRepositoryBase<Sehir, ProjectDbContext>, ISehirRepository
    {
        public SehirRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
