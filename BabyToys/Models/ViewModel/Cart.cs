using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyToys.Models.ViewModel
{
    public class Cart
    {
        List<CartItem> CartItems;
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        public void ThemVaoGioHang(SanPham sanpham, int soluong,int gia)
        {
            if (SanPhamTonTai(sanpham))
            {
                TangSoLuong(sanpham, soluong);
            }
            else
            {
                CartItem newItem = new CartItem { SoLuong=soluong,SanPham=sanpham,Gia=gia};
                newItem.VuotSoLuong = false;
                if (soluong > sanpham.SoLuong())
                {
                    newItem.VuotSoLuong = true;
                }
                CartItems.Add(newItem);
            }
        }
        public void XoaKhoiGioHang(SanPham sanpham)
        {
            if (SanPhamTonTai(sanpham))
            {
                CartItem item = CartItems.SingleOrDefault(c=>c.SanPham.IdSanPham==sanpham.IdSanPham);
                CartItems.Remove(item);
            }
        }
        public void TangSoLuong(SanPham sanpham, int soluong)
        {
            var item = CartItems.First(c => c.SanPham.IdSanPham == sanpham.IdSanPham);
            item.SoLuong = soluong;
            item.VuotSoLuong = false;
            if (item.SoLuong > sanpham.SoLuong())
            {
                item.VuotSoLuong = true;
            }
            
            if (item.SoLuong > 3)
            {
                item.SoLuong = 3;
            }
        }
        public void XoaGioHang()
        {
            CartItems.Clear();
        }

        public bool SanPhamTonTai(SanPham sanpham)
        {
            return CartItems.Any(c => c.SanPham.IdSanPham == sanpham.IdSanPham);
        }

        public IEnumerable<CartItem> ListItem
        {
            get { return CartItems; }
        }

        public decimal TongCong()
        {
            decimal TongCong = 0;
            foreach (var item in CartItems)
            {
                TongCong += item.ThanhTien;
            }
            return TongCong;
        }
    }
}