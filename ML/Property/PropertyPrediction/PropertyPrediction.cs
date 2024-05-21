using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using CapitalClue.Common.Models;
using System.Security.Cryptography.X509Certificates;
using CapitalClue.Web.Server.ML.Entities;
using System.Reflection;

namespace CapitalClue.Web.Server.Ml.Property.PropertyPrediction;

public class PropertyPrediction
{
    private string ModelFileName = "PropertyModel.zip";
    private string Directory = "TrainedModels";
    private ITransformer trainedModel;
    private MLContext context;

    public PropertyPrediction(string City, string PropertyType)
    {
        context = new MLContext();
        DataViewSchema modelSchema;

        ModelFileName = string.Format("{0}-{1}-{2}", City, PropertyType, ModelFileName);
        var binDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        trainedModel = context.Model.Load($"{binDirectory}/{Directory}/{ModelFileName}", out modelSchema);
    }

    public PropertyPredictionEntity GetPrediction()
    {
        var ForeCastEngein = trainedModel.CreateTimeSeriesEngine<PropertyValueIndex, PropertyPredictionEntity>(context);
        var result = ForeCastEngein.Predict();

        return result;
    }

    public PropertyPredictionDto GetPredictionYearByYear()
    {
        var ForeCastEngein = trainedModel.CreateTimeSeriesEngine<PropertyValueIndex, PropertyPredictionEntity>(context);
        var result = ForeCastEngein.Predict();

        int CurrentYear = DateTime.Now.Year;
        PropertyPredictionDto propertyPredictionDto = new PropertyPredictionDto();

        propertyPredictionDto.ConfidenceLowerBound.Add(CurrentYear, 0);
        propertyPredictionDto.ConfidenceUpperBound.Add(CurrentYear, 0);
        propertyPredictionDto.ForeCastIndex.Add(CurrentYear, 0);
        for (int i = 1; i <= 5; i++)
        {
            var lowerboundPercent = (result.ConfidenceLowerBound[28 * i - 1] - result.ConfidenceLowerBound[28 * (i - 1)]) / result.ConfidenceLowerBound[28 * i - 1];
            var upperBoundPercent = (result.ConfidenceUpperBound[28 * i - 1] - result.ConfidenceUpperBound[28 * (i - 1)]) / result.ConfidenceUpperBound[28 * i - 1];
            var indexPercent = (result.ForeCastIndex[28 * i - 1] - result.ForeCastIndex[28 * (i - 1)]) / result.ForeCastIndex[28 * i - 1];

            propertyPredictionDto.ConfidenceLowerBound.Add(CurrentYear + i, lowerboundPercent);
            propertyPredictionDto.ConfidenceUpperBound.Add(CurrentYear + i, upperBoundPercent);
            propertyPredictionDto.ForeCastIndex.Add(CurrentYear + i, indexPercent);
        }

        return propertyPredictionDto;
    }
}