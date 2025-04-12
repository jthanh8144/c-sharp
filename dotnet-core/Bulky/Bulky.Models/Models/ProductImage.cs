using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulky.Models
{
	public class ProductImage
	{
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column(name: "image_url")]
        public string ImageUrl { get; set; }

        [Column(name: "product_id")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
	}
}

