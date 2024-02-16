using CapitalClue.Common.Models;
using Microsoft.ML;

namespace CapitalClue.Web.Server.Ml.Stock.ModelBuilder;

public class StockModelBuilder
{
    string ModelFileName = "StockModel.zip";
    string Directory = "TrainedModels";
    MLContext context;
    IDataView data;
    public StockModelBuilder(StockModelDto stockData)
    {
        context = new MLContext();
        data = context.Data.LoadFromEnumerable<StockValueIndex>(stockData.StockValueIndices);

        ModelFileName = string.Format("{0}-{1}-{2}", stockData.StockName, stockData.Currency, ModelFileName);
    }

    public void Build()
    {
        var pipline = context.Forecasting.ForecastBySsa(
                outputColumnName: nameof(StockPredictionDto.ForeCastIndex),
                inputColumnName: nameof(StockValueIndex.Value),
                confidenceLevel: 0.95F,
                confidenceLowerBoundColumn: nameof(StockPredictionDto.ConfidenceLowerBound),
                confidenceUpperBoundColumn: nameof(StockPredictionDto.ConfidenceUpperBound),
                windowSize: 365,
        seriesLength: 365 * 3,
                trainSize: 365 * 3,
                horizon: 365);

        var model = pipline.Fit(data);
        context.Model.Save(model, data.Schema, Directory +"/"+ ModelFileName);
    }

}
