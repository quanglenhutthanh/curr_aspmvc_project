using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class AssignedPermission
    {
        public int IdChucNang { get; set; }
        public string TenChucNang { get; set; }
        public bool Assigned { get; set; }
    }
}