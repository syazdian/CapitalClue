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
        for (int i = 1; i <= 5; i++)
        {
            stockPredictionDto.ConfidenceLowerBound.Add(CurrentYear + i, result.ConfidenceLowerBound[250 * i - 1]);
            stockPredictionDto.ConfidenceUpperBound.Add(CurrentYear + i, result.ConfidenceUpperBound[250 * i - 1]);
            stockPredictionDto.ForeCastIndex.Add(CurrentYear + i, result.ForeCastIndex[250 * i - 1]);
        }

        return stockPredictionDto;

    }
}
