using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ResimTipi :IEntity
    {
        public int ResimTipiId { get; set; }
        public string Adi { get; set; }
   
    }
}
