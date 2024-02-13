using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalClue.Web.Server.Ml.Stock.ModelBuilder;

public class ResultModel
{
    public float[] ForeCastIndex { get; set; }
    public float[] ConfidenceLowerBound { get; set; }
    public float[] ConfidenceUpperBound { get; set; }
}
