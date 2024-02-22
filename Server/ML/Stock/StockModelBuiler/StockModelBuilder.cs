using CapitalClue.Common.Models;
using CapitalClue.Web.Server.ML.Entities;
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
                outputColumnName: nameof(StockPredictionEntiy.ForeCastIndex),
                inputColumnName: nameof(StockValueIndex.Value),
                confidenceLevel: 0.95F,
                confidenceLowerBoundColumn: nameof(StockPredictionEntiy.ConfidenceLowerBound),
                confidenceUpperBoundColumn: nameof(StockPredictionEntiy.ConfidenceUpperBound),
                windowSize: 250,
        seriesLength: 250 * 10,
                trainSize: 250 * 10,
                horizon: 250*5);

        var model = pipline.Fit(data);
        context.Model.Save(model, data.Schema, Directory +"/"+ ModelFileName);
    }

}
