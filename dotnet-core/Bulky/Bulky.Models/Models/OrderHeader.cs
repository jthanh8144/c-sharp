using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bulky.Models
{
	[Table(name: "order_headers")]
	public class OrderHeader
	{
		[Key]
        [Column(name: "id")]
        public int Id { get; set; }

        [Column(name: "application_user_id")]
        public string ApplicationUserId { get; set; }

		[ForeignKey("ApplicationUserId")]
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }

		[Required]
        [Column(name: "order_date")]
        public DateTime OrderDate { get; set; }

        [Column(name: "shipping_date")]
        public DateTime? ShippingDate { get; set; }

        [Column(name: "order_total")]
        public double OrderTotal { get; set; }

        [Column(name: "order_status")]
        public string? OrderStatus { get; set; }

        [Column(name: "payment_status")]
        public string? PaymentStatus { get; set; }

        [Column(name: "tracking_number")]
        public string? TrackingNumber { get; set; }

        [Column(name: "carrier")]
        public string? Carrier { get; set; }

        [Column(name: "payment_date")]
        public DateTime? PaymentDate { get; set; }

        [Column(name: "payment_due_date")]
        public DateTime? PaymentDueDate { get; set; }

        [Column(name: "session_id")]
        public string? SessionId { get; set; }

        [Column(name: "payment_intent_id")]
        public string? PaymentIntentId { get; set; }

        [Required]
        [Column(name: "phone_number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Column(name: "street_address")]
        public string StreetAddress { get; set; }

        [Required]
        [Column(name: "city")]
        public string City { get; set; }

        [Required]
        [Column(name: "state")]
        public string State { get; set; }

        [Required]
        [Column(name: "postal_code")]
        public string PostalCode { get; set; }

        [Required]
        [Column(name: "name")]
        public string Name { get; set; }
    }
}

