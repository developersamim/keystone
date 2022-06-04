using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientaggregator.application.Contracts.Models.Muzzle
{
    public class MuzzleDto
    {
        public string Id { get; set; }
        public string Confidence { get; set; }
        public string Filename { get; set; }        
        public string Status { get; set; }
    }
}
