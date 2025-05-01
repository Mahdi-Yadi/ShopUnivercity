using System.ComponentModel.DataAnnotations;
using ShopUnivercity.Web.Models.Entities.Orders;

namespace ShopUnivercity.Web.Models.Entities.Products;
public class Product
{
    [Key]
    public int Id { get; set; }

    [DataType(DataType.Text)]
    [MaxLength(500)]
    [MinLength(3)]
    public string Title { get; set; }

    [DataType(DataType.Text)]
    public string Description { get; set; }

    public decimal Price { get; set; }

    [DataType(DataType.Text)]
    [MaxLength(50)]
    public string ImageName { get; set; }

    public int Count { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public ICollection<OrderDetail> OrderDetail { get; set; }

}