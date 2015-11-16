using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyToys.Models.ViewModel
{
    public class KhachHangViewModel:UserViewModel
    {
        public int DiemTichLuy { get; set; }
        public int IdLoaiKhachHang { get; set; }
    }
}