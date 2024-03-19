using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalClue.Frontend.Shared.Models;

public class PropertyPredictionResult
{
    public double Gain { get; set; }
    public double ConfidenceLowerGain { get; set; }
    public double ConfidenceUpperGain { get; set; }

    public Dictionary<int, double> ForeCastIndex { get; set; }
    public Dictionary<int, double> ConfidenceLowerBound { get; set; }
    public Dictionary<int, double> ConfidenceUpperBound { get; set; }

    public PropertyPredictionResult()
    {
        ForeCastIndex = new Dictionary<int, double>();
        ConfidenceLowerBound = new Dictionary<int, double>();
        ConfidenceUpperBound = new Dictionary<int, double>();
    }
}

public class PropertyPurchaseInfo
{
    public double InterestRate { get; set; }
    public double PropertyPrice { get; set; }
    public double PropertyMonthlyPayment { get; set; }
    public double DownPayment { get; set; }
    public int MortgageTerm { get; set; }
}