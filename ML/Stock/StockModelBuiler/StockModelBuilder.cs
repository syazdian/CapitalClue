using CapitalClue.Common.Models;
using CapitalClue.Web.Server.ML.Entities;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace CapitalClue.Web.Server.Ml.Stock.ModelBuilder;

public class StockModelBuilder
{
    private string ModelFileName = "StockModel.mlnet2";
    private string Directory = "TrainedModels";
    private MLContext context;
    private IDataView data;

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
                windowSize: 252,
                seriesLength: 252 * 10,
                trainSize: 252 * 10,
                horizon: 252 * 5
                //     rankSelectionMethod: RankSelectionMethod.

                );

        var model = pipline.Fit(data);
        context.Model.Save(model, data.Schema, Directory + "/" + ModelFileName);
    }
}