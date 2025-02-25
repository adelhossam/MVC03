﻿using Company.G03.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required!!")]
        public string Name { get; set; }
        [Range(25, 60, ErrorMessage = "Age Must Be in 25 And 60")]
        public int? Age { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
        , ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Salary Is Required!!")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        //[RegularExpression("^(\\+20|0)?1[0125]\\d{8}$\r\n",
        //    ErrorMessage = "Invalid phone number . Please enter Egyptian phone number, e.g.," +
        //    " '+20123456789' or '0123456789'.")]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public int? WorkForId { get; set; } // FK By Convention NavigationalNamd + Pk Name in Department
        public Department? WorkFor { get; set; } // Navigational Property

        //[Required(ErrorMessage = "Image Is Required!!")]
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }

    }
}
