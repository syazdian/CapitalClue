using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalClue.Common.Models
{
    public class RefDealerDto
    {
        public string DealerCode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int StoreId { get; set; }
    }
}