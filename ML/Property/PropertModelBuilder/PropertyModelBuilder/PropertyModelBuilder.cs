using CapitalClue.Common.Models;
using CapitalClue.Web.Server.ML.Entities;
using Microsoft.ML;

namespace CapitalClue.Web.Server.Ml.Property.ModelBuilder;

public class PropertyModelBuilder
{
    private string ModelFileName = "PropertyModel.zip";
    private string directory = "C:/TrainedModels";
    private MLContext context;
    private IDataView data;

    public PropertyModelBuilder(PropertyModelDto propertyModelDto)
    {
        context = new MLContext();
        data = context.Data.LoadFromEnumerable<PropertyValueIndex>(propertyModelDto.PropertyValueIndices);

        if (propertyModelDto.PropertyValueIndices.Last().DateTime.Year == 2019)
            directory += "-2019";

       ModelFileName = string.Format("{0}-{1}-{2}", propertyModelDto.City, propertyModelDto.PropertyType, ModelFileName);
    }

    public void Build()
    {
        try
        {
            var pipline = context.Forecasting.ForecastBySsa(
              outputColumnName: nameof(PropertyPredictionEntity.ForeCastIndex),
              inputColumnName: nameof(PropertyValueIndex.Value),
              confidenceLevel: 0.95F,
              confidenceLowerBoundColumn: nameof(PropertyPredictionEntity.ConfidenceLowerBound),
              confidenceUpperBoundColumn: nameof(PropertyPredictionEntity.ConfidenceUpperBound),
              windowSize: 28,
      seriesLength: 28 * 24,
              trainSize: 28 * 24,
              horizon: 28 * 5);

            var model = pipline.Fit(data);

            context.Model.Save(model, data.Schema, directory + "/" + ModelFileName);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}