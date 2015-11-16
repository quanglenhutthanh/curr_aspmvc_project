using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Phải nhập tên đăng nhập!")]
        public string LoginName { get; set; }
        [Required(ErrorMessage = "Phải nhập mật khẩu!")]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
        public string returnUrl { get; set; }
    }
}