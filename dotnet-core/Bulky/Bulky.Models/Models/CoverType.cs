using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulky.Models
{
    [Table(name: "cover_types")]
	public class CoverType
    {
        [Key]
        [Column(name: "id")]
        public int Id { get; set; }

        [Required]
        [Column(name: "name")]
        [Display(Name = "Cover Type")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
