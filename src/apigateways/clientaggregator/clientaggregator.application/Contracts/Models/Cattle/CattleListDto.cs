using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientaggregator.application.Contracts.Models.Cattle
{
    public class CattleListDto
    {
        public int CurrentPage { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }

        public List<CattleDto> Items { get; init; }
    }
}
