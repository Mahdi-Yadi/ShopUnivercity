using ShopUnivercity.Web.Models.Entities.Orders;
using System.ComponentModel.DataAnnotations;
namespace ShopUnivercity.Web.Models.Entities.Account;
public class User
{
    [Key]
    public int Id { get; set; }
    [DataType(DataType.Text)]
    [MaxLength(50)]
    [MinLength(3)]
    public string FullName { get; set; }

    [DataType(DataType.EmailAddress)]
    [MaxLength(100)]
    [MinLength(3)]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [MaxLength(200)]
    [MinLength(3)]
    public string Password { get; set; }

    [DataType(DataType.Text)]
    [MaxLength(400)]
    [MinLength(3)]
    public string Address { get; set; }

    [DataType(DataType.Text)]
    [MaxLength(100)]
    [MinLength(3)]
    public string CodePost { get; set; }

    [DataType(DataType.PhoneNumber)]
    [MaxLength(13)]
    [MinLength(9)]
    public string PhoneNumber { get; set; }

    public int ActiveCode { get; set; }

    public bool IsAdmin { get; set; }

    public DateTime CreateDate { get; set; }

    public ICollection<Order> Orders { get; set; }

}