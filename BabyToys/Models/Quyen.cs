using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class Quyen
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdQuyen { get; set; }

        [Required, StringLength(30)]
        public string TenQuyen { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ChucNang> ChucNangs { get; set; }

        public bool CoChucNang(String code)
        {
            foreach (var p in this.ChucNangs)
            {
                if (p.MaChucNang.Equals(code))
                    return true;
            }

            return false;
        }
    }
}