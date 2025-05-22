using System.ComponentModel.DataAnnotations;
using ShopUnivercity.Web.Models.Entities.Products;
namespace ShopUnivercity.Web.Models.Entities.Orders;
public class OrderDetail
{
    
        [Key]
        public int Id { get; set; }

        public int Count { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }

}