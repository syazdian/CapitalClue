using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using CapitalClue.Common.Models;


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

    public StockPredictionDto GetPrediction()
    {
        var ForeCastEngein = trainedModel.CreateTimeSeriesEngine<StockValueIndex, StockPredictionDto>(context);
        var result = ForeCastEngein.Predict();

        return result;

    }
}
