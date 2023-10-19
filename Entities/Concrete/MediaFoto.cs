using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class MediaFoto :IEntity
    {
        public int MediaFotoId { get; set; }
        public string Foto { get; set; }
    }
}
