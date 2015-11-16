using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class Log
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLog { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string IP { get; set; }
        public DateTime Time { get; set; }
        public virtual User User { get; set; }
    }
}