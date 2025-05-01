using System.ComponentModel.DataAnnotations;
namespace ShopUnivercity.Web.Models.Entities.Site;
public class SiteSettings
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }

    public string? LogoImageName { get; set; }

    public int? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? FooterText { get; set; }

}