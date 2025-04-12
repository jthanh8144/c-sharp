using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulky.Models
{
	[Table(name: "companies")]
	public class Company
	{
		[Key]
		[Column(name: "id")]
		public int Id { get; set; }

		[Required]
        [Column(name: "name")]
        public string Name { get; set; }

		[DisplayName("Street address")]
        [Column(name: "street_address")]
        public string? StreetAddress { get; set; }

        [Column(name: "city")]
        public string? City { get; set; }

        [Column(name: "state")]
        public string? State { get; set; }

        [DisplayName("Postal code")]
        [Column(name: "postal_code")]
        public string? PostalCode { get; set; }

        [DisplayName("Phone number")]
        [Column(name: "phone_number")]
        public string? PhoneNumber { get; set; }
    }
}
