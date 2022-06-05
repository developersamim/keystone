using System;
using System.ComponentModel.DataAnnotations;

namespace clientaggregator.api.Models
{
    public class ProfileUpdateModel
    {
        [Required]
        //[RegularExpression(@"^[a-zA-Z\s-]{2,30}$", ErrorMessage = "Characters are not allowed.")]
        public string GivenName { get; set; }

        [Required]
        //[RegularExpression(@"^[a-zA-Z\s-]{2,30}$", ErrorMessage = "Characters are not allowed.")]
        public string FamilyName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        //[Required]
        //[RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Characters are not allowed.")]
        public string PostalCode { get; set; }

        public string Nickname { get; set; }
        public string PhoneNumber { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public string FAddressLine1 { get; set; }
        public string FAddressLine2 { get; set; }
        public string FSuburb { get; set; }
        public string FCity { get; set; }
        public string FPostalCode { get; set; }
        public string FState { get; set; }
        public string FCountry { get; set; }
    }
}
