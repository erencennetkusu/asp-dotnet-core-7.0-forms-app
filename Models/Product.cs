using System.ComponentModel.DataAnnotations;

namespace   FormsApp.Models;



public class Product{

    [Display(Name = "Ürün Id")]
    public int ProductId { get; set; }
    [Display(Name = "Ürün İsim")]
    [Required(ErrorMessage = "Ürün İsmi Boş Bırakılamaz")]
    public string  Name { get; set; } = String.Empty;
    [Display(Name = "Ürün Resim")]
    public string? Image { get; set; }
    [Display(Name = "Ürün Fiyat")]
    [Required(ErrorMessage = "Ürün Fiyat Boş Bırakılamaz")]
    public decimal? Price { get; set; }
    [Display(Name = "Ürün Satış Durumu")]
    public bool IsActve { get; set; }
    [Display(Name = "Ürün Kategorisi")]
    public int CategoryId { get; set; }
}