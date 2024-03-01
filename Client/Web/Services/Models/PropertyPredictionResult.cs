using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalClue.Frontend.Web.Models;

public class PropertyPredictionResult
{
    public float Gain { get; set; }
    public float ConfidenceLowerGain { get; set; }
    public float ConfidenceUpperGain { get; set; }

    public Dictionary<int, float> ForeCastIndex { get; set; }
    public Dictionary<int, float> ConfidenceLowerBound { get; set; }
    public Dictionary<int, float> ConfidenceUpperBound { get; set; }

    public PropertyPredictionResult()
    {
        ForeCastIndex = new Dictionary<int, float>();
        ConfidenceLowerBound = new Dictionary<int, float>();
        ConfidenceUpperBound = new Dictionary<int, float>();
    }
}

public class PropertyPurchaseInfo
{
    public float InterestRate { get; set; }
    public float PropertyPrice { get; set; }
    public float MortgageMonthlyPayment { get; set; }
    public float DownPayment { get; set; }
    public int MortgageTerm { get; set; }
}