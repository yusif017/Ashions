using Entities.Concrete;
using Entities.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;


namespace DataAccess.Concrete.EntityFramework
{
    public class AppDbContext : IdentityDbContext<User>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SQL5111.site4now.net;Initial Catalog=db_a9f66c_yusif817;User Id=db_a9f66c_yusif817_admin;Password=Yusif*817*");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CategoryLanguage> CategoryLanguages { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ColorLanguage> ColorLanguages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLanguage> ProductLanguages { get; set; }
        public DbSet<PraductOxsar> PraductOxsars { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ProductSearchDTO>().ToView(null);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}



//CREATE PROCEDURE GetSea
//    @LangCode NVARCHAR(250),
//    @TitleFilter NVARCHAR(250) --Yeni parametre
//AS
//BEGIN
//    SELECT
//        Products.Id,
//        ProductLanguages.ProductTitle,
//        ProductLanguages.SeoUrl,
//        Pictures.PhotoUrl,
//		Products.Price
//    FROM
//        Products
//    INNER JOIN
//        ProductLanguages
//    ON
//        Products.Id = ProductLanguages.ProductId
//    INNER JOIN
//        Pictures
//    ON
//        Products.Id = Pictures.ProductId
//        AND Pictures.Id = (
//            SELECT
//                MIN(Id)
//            FROM
//                Pictures
//            WHERE
//                ProductId = Products.Id
//        )
//    WHERE
//        ProductLanguages.LangCode = @LangCode
//        AND ProductLanguages.ProductTitle LIKE @TitleFilter +'%'   -- Başlığı filtreleme
//    ORDER BY
//        Products.Id DESC
//END
//EXEC GetSea 'az-Az', 's'
