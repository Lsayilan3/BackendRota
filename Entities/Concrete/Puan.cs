using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Puan :IEntity
    {
        public int PuanId { get; set; }
        public int RotaId { get; set; }
        public string GenelPuan { get; set; }
        public string Hizmetler { get; set; }
        public string HizmetlerPuan { get; set; }
        public string Konum { get; set; }
        public string KonumPuan { get; set; }
        public string Kolayliklar { get; set; }
        public string KolayliklarPuan { get; set; }
        public string Fiyat { get; set; }
        public string FiyatPuan { get; set; }
        public string Yiyecek { get; set; }
        public string YiyecekPuan { get; set; }
        public string Harita { get; set; }


    }
}
