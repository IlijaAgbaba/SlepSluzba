using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RsokProject.Models
{
    public class Komentar
    {
        public int id { get; set; }
        public string ime { get; set; }
        public string tekstKomentara { get; set; }
        public DateTime datum { get; set; }
    }
}