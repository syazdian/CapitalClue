using CapitalClue.Common.Models;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapitalClue.Web.Server.Ml.Stock.ModelBuilder;

public class ModelBuilder
{
    MLContext context;
    IDataView data;
    TimeSeriesPredictionEngine<StockValueIndex, ResultModel> ForeCastEngein;
    public ModelBuilder(StockModelDto stockData)
    {
        context = new MLContext();
        data = context.Data.LoadFromEnumerable<StockValueIndex>(stockData.StockValueIndices);
    }

    public TimeSeriesPredictionEngine<StockValueIndex, ResultModel> Build()
    {
        var pipline = context.Forecasting.ForecastBySsa(
                outputColumnName: nameof(ResultModel.ForeCastIndex),
                inputColumnName: nameof(StockValueIndex.Value),
                confidenceLevel: 0.95F,
                confidenceLowerBoundColumn: nameof(ResultModel.ConfidenceLowerBound),
                confidenceUpperBoundColumn: nameof(ResultModel.ConfidenceUpperBound),
                windowSize: 365,
        seriesLength: 365 * 3,
                trainSize: 365 * 3,
                horizon: 365);

        var model = pipline.Fit(data);

        ForeCastEngein = model.CreateTimeSeriesEngine<StockValueIndex, ResultModel>(context);
        return ForeCastEngein;
       
    }

    public void SaveModel()
    {
        byte[] modelBytes;
        using (var stream = new MemoryStream())
        {
            ForeCastEngein.CheckPoint(context, stream);
            modelBytes = stream.ToArray();

            string filePath = Directory.GetCurrentDirectory(); ;
            string fileName = "StockModel";
            DirectoryInfo info = new DirectoryInfo(filePath);
            if (!info.Exists)
            {
                info.Create();
            }

            string path = Path.Combine(filePath, fileName);
            using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
            {
                stream.WriteTo(outputFileStream);
                outputFileStream.Close();
                stream.Close();
            }
        }
    }

}
