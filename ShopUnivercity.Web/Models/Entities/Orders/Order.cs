using System.ComponentModel.DataAnnotations;
using ShopUnivercity.Web.Models.Entities.Account;

namespace ShopUnivercity.Web.Models.Entities.Orders;
public class Order
{
    [Key]
    public int Id { get; set; }

    public decimal? SumPrice { get; set; }

    public string? PaymentCode { get; set; }

    public DateTime? PaymentDate { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsPay { get; set; }

    public bool IsCompleted { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }

}