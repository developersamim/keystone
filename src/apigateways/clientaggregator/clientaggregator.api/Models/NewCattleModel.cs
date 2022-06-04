using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace clientaggregator.api.Models
{
    public class NewCattleModel
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public string Tag { get; set; }
        public string IdentificationHash { get; set; }
        public string FarmId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LivestockType { get; set; }
    }

    public class CattleModel : NewCattleModel
    {
        public string Id { get; set; }
    }

    public class RegisterCattleModel : NewCattleModel
    {
        public IFormFile formFile { get; set; }
    }

}
