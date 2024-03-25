using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using CapitalClue.Common.Models;
using CapitalClue.Web.Server.ML.Entities;


namespace CapitalClue.Web.Server.Ml.Stock.StockPrediction;

public class StockPredictor
{
    string ModelFileName = "StockModel.zip";
    string Directory = "TrainedModels";
    ITransformer trainedModel;
    MLContext context;
    public StockPredictor(string StockName,string Currency)
    {
        context = new MLContext();
        DataViewSchema modelSchema;

        ModelFileName = string.Format("{0}-{1}-{2}", StockName, Currency, ModelFileName);
        trainedModel = context.Model.Load(Directory + "/" + ModelFileName, out modelSchema);
    }

    public StockPredictionEntiy GetPrediction()
    {
        var ForeCastEngein = trainedModel.CreateTimeSeriesEngine<StockValueIndex, StockPredictionEntiy>(context);
        var result = ForeCastEngein.Predict();

        return result;

    }

    public StockPredictionDto GetPredictionYearByYear()
    {
        var ForeCastEngein = trainedModel.CreateTimeSeriesEngine<StockValueIndex, StockPredictionEntiy>(context);
        var result = ForeCastEngein.Predict();

        int CurrentYear = DateTime.Now.Year;
        StockPredictionDto stockPredictionDto = new StockPredictionDto();

        stockPredictionDto.ConfidenceLowerBound.Add(CurrentYear, 0);
        stockPredictionDto.ConfidenceUpperBound.Add(CurrentYear, 0);
        stockPredictionDto.ForeCastIndex.Add(CurrentYear, 0);
        for (int i = 1; i <= 5; i++)
        {
            var lowerboundPercent = (result.ConfidenceLowerBound[28 * i - 1] - result.ConfidenceLowerBound[28 * (i - 1)]) / result.ConfidenceLowerBound[28 * i - 1] * 100;
            var upperBoundPercent = (result.ConfidenceUpperBound[28 * i - 1] - result.ConfidenceUpperBound[28 * (i - 1)]) / result.ConfidenceUpperBound[28 * i - 1] * 100;
            var indexPercent = (result.ForeCastIndex[28 * i - 1] - result.ForeCastIndex[28 * (i - 1)]) / result.ForeCastIndex[28 * i - 1] * 100;

            stockPredictionDto.ConfidenceLowerBound.Add(CurrentYear + i, lowerboundPercent);
            stockPredictionDto.ConfidenceUpperBound.Add(CurrentYear + i, upperBoundPercent);
            stockPredictionDto.ForeCastIndex.Add(CurrentYear + i, indexPercent);
        }

        return stockPredictionDto;

    }
}
