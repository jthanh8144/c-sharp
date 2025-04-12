using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulky.Models
{
    [Table(name: "categories")]
	public class Category
	{
		[Key]
        [Column("id")]
        public int Id { get; set; }

		[Required]
        [Column("name")]
        [Display(Name = "Category name")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Column("display_order")]
        [Display(Name = "Display order")]
        [Range(1, 1000)]
        public int DisplayOrder { get; set; }
    }
}
