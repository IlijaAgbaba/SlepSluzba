using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RsokProject.ViewModel
{
    public class Register
    {
        [Required]
        [Display(Name = "Ime")]
        public string ime { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string lozinka { get; set; }
    }
}