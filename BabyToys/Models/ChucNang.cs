using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class ChucNang
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdChucNang { get; set; }
        public String TenChucNang { get; set; }
        public String MaChucNang { get; set; }

        public virtual ICollection<Quyen> Roles { get; set; }
    }
}