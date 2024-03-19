using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalClue.Common.Models;

public class PropertyPredictionDto
{
    public Dictionary<int,float> ForeCastIndex { get; set; }
    public Dictionary<int, float> ConfidenceLowerBound { get; set; }
    public Dictionary<int, float> ConfidenceUpperBound { get; set; }

    public PropertyPredictionDto()
    {
        ForeCastIndex = new Dictionary<int, float>();
        ConfidenceLowerBound = new Dictionary<int, float>();
        ConfidenceUpperBound = new Dictionary<int, float>();
    }
}
