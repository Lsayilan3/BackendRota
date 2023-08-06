﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Ulke :IEntity
    {
        public int UlkeId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Foto { get; set; }
        public int Yayin { get; set; }
        public int Sira { get; set; }
    }
}