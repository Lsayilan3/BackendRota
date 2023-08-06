using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Bolgeler : IEntity
    {
        public int BolgelerId { get; set; }
        public int UlkeId { get; set; }  //ülke ıd
        public string Foto { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Yayin { get; set; }
        public int Sira { get; set; }
    }
}
