using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyToys.Models.ViewModel
{
    public class CartSession
    {
        private static string CartSessionKey = "CartSession";
        
        public static bool CartTonTai(HttpContextBase context)
        {
            return context.Session[CartSessionKey] as Cart != null;
        }
        
        public static Cart GetCart(HttpContextBase context)
        {
            Cart cart = null;
            if (CartTonTai(context))
            {
                cart = context.Session[CartSessionKey] as Cart;
            }
            else
            {
                cart = new Cart();
            }
            return cart;
        }
        public static void ThemVaoCartSession(HttpContextBase context,SanPham sanpham, int soluong,int gia)
        {
            Cart cart = GetCart(context);
            cart.ThemVaoGioHang(sanpham, soluong,gia);
            context.Session[CartSessionKey] = cart;
        }
        public static void XoaKhoiCartSession(HttpContextBase context, SanPham sanpham)
        {
            if (CartTonTai(context))
            {
                Cart cart = GetCart(context);
                cart.XoaKhoiGioHang(sanpham);
                context.Session[CartSessionKey] = cart;
            }
        }
        public static void XoaCartSession(HttpContextBase context)
        {
            if (CartTonTai(context))
            {
                Cart cart = GetCart(context);
                cart.XoaGioHang();
                context.Session[CartSessionKey] = cart;
            }
        }
        public static void CapNhatCartSession(HttpContextBase context, SanPham sanpham,int soluong)
        {
            if (CartTonTai(context))
            {
                Cart cart = GetCart(context);
                cart.TangSoLuong(sanpham,soluong);
                context.Session[CartSessionKey] = cart;
            }
        }
        public static decimal TongTien(HttpContextBase context)
        {
            if (CartTonTai(context))
            {
                Cart cart = GetCart(context);
                return cart.TongCong();
            }
            return 0;
        }
    }
}