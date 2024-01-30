using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalClue.Common.Models
{
    public class ToDeleteItemsDto
    {
        public List<long> BellIds { get; set; }

        public ToDeleteItemsDto()
        {
            BellIds = new List<long>();
        }
    }
}
