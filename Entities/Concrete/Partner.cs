using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Partner :IEntity
    {
        public int PartnerId { get; set; }
        public string Foto { get; set; }
    }
}
