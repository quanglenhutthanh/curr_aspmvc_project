using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class Menu
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<int> danhmuc { get; set; }
        public Nullable<int> type { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string link { get; set; }
        public Nullable<int> MenuOrder { get; set; }
        public virtual ICollection<Menu> SubMenu { get; set; }
        public virtual Menu ParentMenu { get; set; }
    }
}