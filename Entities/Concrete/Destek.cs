using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Destek :IEntity
    {
        public int DestekId { get; set; }
        public string Foto { get; set; }
    }
}
