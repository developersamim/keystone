using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientaggregator.application.Contracts.Models.Cattle
{
    public class CattleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public string Tag { get; set; }
        public string IdentificationHash { get; set; }
        public string FarmId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LivestockType { get; set; }
    }
}
