using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Team :IEntity
    {
        public int TeamId { get; set; }
        public string Foto { get; set; }
        public string Adi { get; set; }
        public string Baslik { get; set; }
        public string Linkbir { get; set; }
        public string Linkiki { get; set; }
        public string Linkbuc { get; set; }
    }
}
