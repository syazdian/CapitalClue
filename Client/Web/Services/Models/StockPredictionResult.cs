using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalClue.Frontend.Web.Models;

public class StockPredictionResult
{
    public float Gain { get; set; }
    public float ConfidenceLowerGain { get; set; }
    public float ConfidenceUpperGain { get; set; }

    public Dictionary<int, float> ForeCastIndex { get; set; }
    public Dictionary<int, float> ConfidenceLowerBound { get; set; }
    public Dictionary<int, float> ConfidenceUpperBound { get; set; }

    public StockPredictionResult()
    {
        ForeCastIndex = new Dictionary<int, float>();
        ConfidenceLowerBound = new Dictionary<int, float>();
        ConfidenceUpperBound = new Dictionary<int, float>();
    }
}