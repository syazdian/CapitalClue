using CapitalClue.Common.Models;
using CapitalClue.Web.Server.ML.Entities;
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
                outputColumnName: nameof(PropertyPredictionEntity.ForeCastIndex),
                inputColumnName: nameof(PropertyValueIndex.Value),
                confidenceLevel: 0.95F,
                confidenceLowerBoundColumn: nameof(PropertyPredictionEntity.ConfidenceLowerBound),
                confidenceUpperBoundColumn: nameof(PropertyPredictionEntity.ConfidenceUpperBound),
                windowSize: 28,
        seriesLength: 28 * 24,
                trainSize: 28*24,
                horizon: 28*5);

        var model = pipline.Fit(data);
        context.Model.Save(model, data.Schema, Directory + "/" + ModelFileName);
    }
}
