using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class RotaGaleri :IEntity
    {
        public int RotaGaleriId { get; set; }
        public int RotaId { get; set; }
        public string Foto { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Yayin { get; set; }
        public int ResimTipiId { get; set; }
    }
}
