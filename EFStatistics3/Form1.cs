using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFStatistics3
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Db3Project20Entities db = new Db3Project20Entities();
        private void Form1_Load(object sender, EventArgs e)
        {
            // Toplam kategori sayısı
            int categoryCount = db.TblCategory.Count();
            lblCategoryCount.Text = categoryCount.ToString();

            // Toplam ürün sayısı
            int productCount = db.TblProduct.Count();
            lblProductCount.Text = productCount.ToString();

            // Toplam müşteri sayısı
            int customerCount = db.TblCustomer.Count();
            lblCustomerCount.Text = customerCount.ToString();

            // Toplam sipariş sayısı
            int orderCount = db.TblOrder.Count();
            lblOrderCount.Text = orderCount.ToString();

            // Toplam stok sayısı
            var totalStockCount = db.TblProduct.Sum(x => x.ProductStock); // ProductStock sütununu komple toplar.
            lblTotalStock.Text = totalStockCount.ToString();

            // Ortalama ürün fiyatı
            var averagePrice = db.TblProduct.Average(x => x.ProductPrice);
            lblAveragePrice.Text = averagePrice.ToString() + " $";

            // Toplam meyve sayısı. Burada kategori id ye filtreleme yaptik. Fruit in cat id si = 1 'dir
            var totalFruit = db.TblProduct.Where(x=>x.CategoryId==1).Sum(y=>y.ProductStock);
            lblTotalFruit.Text = totalFruit.ToString();

            // Kolanın stok ile birim fiyatının çarpımı yani satış Hacmi
            var cokeGetStock = db.TblProduct.Where(x => x.ProductName == "Coke").Select(y => y.ProductStock).FirstOrDefault();
            var cokeGetUnitPrice = db.TblProduct.Where(x => x.ProductName == "Coke").Select(y => y.ProductPrice).FirstOrDefault();
            var cokeVolume = cokeGetStock * cokeGetUnitPrice;
            lblCokeTotal.Text = cokeVolume.ToString() + " $";

            // Stok sayısı 500 den küçük olan ürün sayısı
            var productStockLowerThan500 = db.TblProduct.Where(x=>x.ProductStock<=500).Count();
            lblProductStockLower500.Text = productStockLowerThan500.ToString();

            // Kategorisi Sebze olan ürünleri (aktif) listelemek icin; Categoryname göre sorgulayıp sonucu
            // name den gelen sonucu da categoryid ye atadık
            var activeVegetablesStock = db.TblProduct.Where(x=>x.CategoryId== (db.TblCategory.Where(z=>z.CategoryName=="Vegetables").Select(w=>w.CategoryId).FirstOrDefault()) && x.ProductStatus==true).Sum(y=>y.ProductStock);
            lblVegetablesStock.Text = activeVegetablesStock.ToString();

            // Türkiye'den verilen siparisler SQL Query ile; 4 döndürmesi gerek. Id 2 olan musteri 2 sipariş verdi
            // Id 3 olan musteri 2 sipariş verdi. O yüzden Türkiyeden toplam 4 sipariş verildi.
            var ordersFromTurkey = db.Database.SqlQuery<int>("Select count(*) From TblOrder Where CustomerId In (Select CustomerId From TblCustomer Where CustomerCity = 'Turkey')").FirstOrDefault();
            lblOrdersFromTurkey.Text = ordersFromTurkey.ToString();

            // Türkiye'den verilen siparişler EF ile;
            var turkishCustomerIds = db.TblCustomer.Where(x=>x.CustomerCity == "Turkey")
                                                 .Select(y=>y.CustomerId)
                                                 .ToList();
            var orderCountFromTurkey = db.TblOrder.Count(z => turkishCustomerIds.Contains(z.CustomerId.Value));
            lblOrdersFromTurkeyEF.Text = orderCountFromTurkey.ToString();

            // Siparisler icinde kategorisi technology olan ürünlerin toplam satıs fiyatı;
            var orderTotalPriceTech = db.Database.SqlQuery<decimal>("Select Sum(O.TotalPrice) From TblOrder O Join TblProduct P On O.ProductId = P.ProductId Join TblCategory C On P.CategoryId = C.CategoryId Where C.CategoryName = 'Technology'").FirstOrDefault();
            lblTechTotalPrice.Text = orderTotalPriceTech.ToString() + " $";

            // Yukarıdaki sorgunun EF linq ile yazılması
            var orderTotalPriceTechEF = (from o in db.TblOrder
                                         join p in db.TblProduct on o.ProductId equals p.ProductId
                                         join c in db.TblCategory on p.CategoryId equals c.CategoryId
                                         where c.CategoryName == "Technology"
                                         select o.TotalPrice).Sum();
            lblTechTotalPriceEF.Text = orderTotalPriceTechEF.ToString() + " $";

            // En son eklenen ürünün adını yazdırma
            var lastProductName = db.TblProduct.OrderByDescending(x => x.ProductId).Select(y => y.ProductName).FirstOrDefault();
            lblLastProduct.Text = lastProductName.ToString();

            // En son eklenen ürünün Kategorisini yazdırma
            var lastProductCatId = db.TblProduct.OrderByDescending(x => x.ProductId).Select(y => y.CategoryId).FirstOrDefault();
            var lastProductCatName = db.TblCategory.Where(z => z.CategoryId == lastProductCatId).Select(k => k.CategoryName).FirstOrDefault();
            lblLastAddedProductsCat.Text = lastProductCatName.ToString();

            // Aktif ürün sayısını yazdıralım.
            var activeProductCount = db.TblProduct.Where(x => x.ProductStatus == true).Count();
            lblActiveProductNum.Text = activeProductCount.ToString();

            // Bütün sodaları satarsak ne kadar kazanacağız? 4000 * 20 = 80.000 döndürmeli
            var sodaStock = db.TblProduct.Where(x => x.ProductName == "Soda").Select(y=>y.ProductStock).FirstOrDefault();
            var sodaPrice = db.TblProduct.Where(x => x.ProductName == "Soda").Select(y => y.ProductPrice).FirstOrDefault();
            var totalSodaRevenue = sodaPrice * sodaStock;
            lblTotalSoda.Text = totalSodaRevenue.ToString() + " $";

            // Son siparişi veren müşterinin adını soyadını getirelim (Servet Kaya)
            var lastCustomerId = db.TblOrder.OrderByDescending(x => x.OrderId).Select(y => y.CustomerId).FirstOrDefault();
            var lastCustomerName = db.TblCustomer.Where(x => x.CustomerId == lastCustomerId).Select(y => y.CustomerName).FirstOrDefault();
            lblLastCustomer.Text = lastCustomerName.ToString();

            // Customer tablosu içinden kaç farklı ülke olduğunu getirelim
            var customerCountry = db.TblCustomer.Select(x => x.CustomerCity).Distinct().Count();
            lblOrderCountry.Text = customerCountry.ToString();
        }

        private void lblCustomerCount_Click(object sender, EventArgs e)
        {

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
