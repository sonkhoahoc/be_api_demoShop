using Microsoft.EntityFrameworkCore;

namespace VHUX.Api.Entity
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }
        public virtual DbSet<Category_News> Category_News { set; get; }
        public virtual DbSet<Product_File> Product_File { set; get; }
        public virtual DbSet<Cart> Cart { set; get; }
        public virtual DbSet<Cart_Detail> Cart_Detail { set; get; }
        public virtual DbSet<Customer> Customer { set; get; }
        public virtual DbSet<Order> Order { set; get; }
        public virtual DbSet<Order_Detail> Order_Detail { set; get; }
        public virtual DbSet<News> News { set; get; }
        public virtual DbSet<Category_Product> Category_Product { set; get; }
        public virtual DbSet<Category_Size> Category_Size { set; get; }
        public virtual DbSet<Product> Product { set; get; }
        public virtual DbSet<Config> Config { set; get; }
        public virtual DbSet<Admin_User> Admin_User { set; get; }
        public virtual DbSet<Payment_VNPay_Order> Payment_VNPay_Order { set; get; }
        public virtual DbSet<Vnpay_IPN> Vnpay_IPN { set; get; }
        public virtual DbSet<Payment_VNPay_Hitory> Payment_VNPay_Hitory { set; get; }
        public virtual DbSet<Orders_Payment> Orders_Payment { set; get; }
        public virtual DbSet<SMS_OTP> SMS_OTP { set; get; }
        public virtual DbSet<Customer_Cart> Customer_Cart { set; get; }
    }
}

