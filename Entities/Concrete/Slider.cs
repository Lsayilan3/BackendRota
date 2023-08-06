using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Slider :IEntity
    {
        public int SliderId { get; set; }
        public string Altyazi { get; set; }
        public string Baslik { get; set; }
         public string Foto { get; set; }
    }
}
