using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalClue.Common.Models;

public class StockPredictionDto
{
    public float[] ForeCastIndex { get; set; }
    public float[] ConfidenceLowerBound { get; set; }
    public float[] ConfidenceUpperBound { get; set; }
}
