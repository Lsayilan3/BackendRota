using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Yorumlar :IEntity
    {
        public int YorumlarId { get; set; }
        public int RotaId { get; set; }
        public int Puan { get; set; }
        public string Isim { get; set; }
        public string Baslik { get; set; }
        public string Yorum { get; set; }
        public int Yayin { get; set; }
    }
}
