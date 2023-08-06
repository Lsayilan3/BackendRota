using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Rota :IEntity
    {
        public int RotaId { get; set; }
        public string Baslik { get; set; }
        public string Ozet { get; set; }
        public string Aciklama { get; set; }
        public string Yayin { get; set; }
        public int Sira { get; set; }
        public string Foto { get; set; }
        public int KategoriId { get; set; }
        public int SehirId { get; set; }
        public int AnaRotaId { get; set; }
    }
}
 
//  edit eklenecek rota ıd le gidilecek açık form foto başlık detay