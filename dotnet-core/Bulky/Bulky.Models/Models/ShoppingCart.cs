using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bulky.Models
{
    [Table(name: "shopping_carts")]
    public class ShoppingCart
    {
        [Key]
        [Column(name: "id")]
        public int Id { get; set; }

        [Range(1, 1000, ErrorMessage = "Value in range 1 - 1000")]
        [Column(name: "count")]
        public int Count { get; set; }

        [Column(name: "product_id")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }

        [Column(name: "application_user_id")]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
