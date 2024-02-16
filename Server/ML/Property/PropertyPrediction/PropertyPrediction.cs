using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using CapitalClue.Common.Models;
using System.Security.Cryptography.X509Certificates;

namespace CapitalClue.Web.Server.Ml.Property.PropertyPrediction;

public class PropertyPrediction
{
    string ModelFileName = "PropertyModel.zip";
    string Directory = "TrainedModels";
    ITransformer trainedModel;
    MLContext context;

    public PropertyPrediction(string City, string PropertyType)
    {
        context = new MLContext();
        DataViewSchema modelSchema;

        ModelFileName = string.Format("{0}-{1}-{2}", City, PropertyType, ModelFileName);
        trainedModel = context.Model.Load(Directory + "/" + ModelFileName, out modelSchema);
    }

    public PropertyPredictionDto GetPrediction()
    {
        var ForeCastEngein = trainedModel.CreateTimeSeriesEngine<PropertyValueIndex, PropertyPredictionDto>(context);
        var result = ForeCastEngein.Predict();

        return result;

    }
}
