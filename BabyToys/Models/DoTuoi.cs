using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class DoTuoi
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDoTuoi { get; set; }
        public int TuTuoi { get; set; }
        public int DenTuoi { get; set; }
        public string GhiChu { get; set; }
    }
}