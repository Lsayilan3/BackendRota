using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Gunler :IEntity
    {
        public int GunlerId { get; set; }
        public int RotaId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }

    }
}
