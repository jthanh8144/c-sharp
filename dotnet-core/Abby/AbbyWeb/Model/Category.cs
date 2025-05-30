﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AbbyWeb.Model
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

        [DisplayName("Display order")]
		[Range(1, 100, ErrorMessage = "Display order must be in range of 1-100")]
        public int DisplayOrder { get; set; }
	}
}

