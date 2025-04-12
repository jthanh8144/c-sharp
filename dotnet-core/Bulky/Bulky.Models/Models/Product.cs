using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bulky.Models
{
    [Table(name: "products")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Required]
        [Column("isbn")]
        public string ISBN { get; set; }

        [Required]
        [Column("author")]
        public string Author { get; set; }

        [Required]
        [Column("list_price")]
        [Range(1, 10000)]
        [Display(Name = "List price")]
        public double ListPrice { get; set; }

        [Required]
        [Column("price")]
        [Range(1, 10000)]
        [Display(Name = "Price for 1-50")]
        public double Price { get; set; }

        [Required]
        [Column("price50")]
        [Range(1, 10000)]
        [Display(Name = "Price for 51-100")]
        public double Price50 { get; set; }

        [Required]
        [Column("price100")]
        [Range(1, 10000)]
        [Display(Name = "Price for 100+")]
        public double Price100 { get; set; }

        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        [Display(Name = "Category")]
        public Category Category { get; set; }

        [Required]
        [Column("cover_type_id")]
        public int CoverTypeId { get; set; }

        [ForeignKey("CoverTypeId")]
        [ValidateNever]
        [Display(Name = "Cover type")]
        public CoverType CoverType { get; set; }

        [ValidateNever]
        [Display(Name = "Product images")]
        public List<ProductImage> ProductImages { get; set; }
    }
}
