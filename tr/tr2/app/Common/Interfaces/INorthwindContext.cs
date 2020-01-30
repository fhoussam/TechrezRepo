using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace domain.Entities
{
    public interface INorthwindContext
    {
        Task<int> SaveChangeAsyc();
        DbSet<AlphabeticalListOfProducts> AlphabeticalListOfProducts { get; set; }
        DbSet<Categories> Categories { get; set; }
        DbSet<CategorySalesFor1997> CategorySalesFor1997 { get; set; }
        DbSet<CurrentProductList> CurrentProductList { get; set; }
        DbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCity { get; set; }
        DbSet<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
        DbSet<CustomerDemographics> CustomerDemographics { get; set; }
        DbSet<Customers> Customers { get; set; }
        DbSet<Employees> Employees { get; set; }
        DbSet<EmployeeTerritories> EmployeeTerritories { get; set; }
        DbSet<Invoices> Invoices { get; set; }
        DbSet<OrderDetails> OrderDetails { get; set; }
        DbSet<OrderDetailsExtended> OrderDetailsExtended { get; set; }
        DbSet<Orders> Orders { get; set; }
        DbSet<OrdersQry> OrdersQry { get; set; }
        DbSet<OrderSubtotals> OrderSubtotals { get; set; }
        DbSet<Products> Products { get; set; }
        DbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrice { get; set; }
        DbSet<ProductSalesFor1997> ProductSalesFor1997 { get; set; }
        DbSet<ProductsByCategory> ProductsByCategory { get; set; }
        DbSet<QuarterlyOrders> QuarterlyOrders { get; set; }
        DbSet<Region> Region { get; set; }
        DbSet<SalesByCategory> SalesByCategory { get; set; }
        DbSet<SalesTotalsByAmount> SalesTotalsByAmount { get; set; }
        DbSet<Shippers> Shippers { get; set; }
        DbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarter { get; set; }
        DbSet<SummaryOfSalesByYear> SummaryOfSalesByYear { get; set; }
        DbSet<Suppliers> Suppliers { get; set; }
        DbSet<Territories> Territories { get; set; }
    }
}