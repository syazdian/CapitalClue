using CapitalClue.Common.Models;
using Microsoft.ML;

namespace CapitalClue.Web.Server.Ml.Property.ModelBuilder;

public class PropertyModelBuilder
{
    string ModelFileName = "PropertyModel.zip";
    string Directory = "TrainedModels";
    MLContext context;
    IDataView data;

    public PropertyModelBuilder(PropertyModelDto propertyModelDto)
    {
        context = new MLContext();
        data = context.Data.LoadFromEnumerable<PropertyValueIndex>(propertyModelDto.PropertyValueIndices);

        ModelFileName = string.Format("{0}-{1}-{2}", propertyModelDto.City, propertyModelDto.PropertyType, ModelFileName);
    }

    public void Build()
    {
        var pipline = context.Forecasting.ForecastBySsa(
                outputColumnName: nameof(PropertyPredictionDto.ForeCastIndex),
                inputColumnName: nameof(PropertyValueIndex.Value),
                confidenceLevel: 0.95F,
                confidenceLowerBoundColumn: nameof(PropertyPredictionDto.ConfidenceLowerBound),
                confidenceUpperBoundColumn: nameof(PropertyPredictionDto.ConfidenceUpperBound),
                windowSize: 365,
        seriesLength: 365 * 10,
                trainSize: 365 * 10,
                horizon: 365);

        var model = pipline.Fit(data);
        context.Model.Save(model, data.Schema, Directory + "/" + ModelFileName);
    }
}
