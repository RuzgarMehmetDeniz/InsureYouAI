using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace InsureYouAI.Services
{
    public class PolicySalesData
    {
        public DateTime Date { get; set; }
        public float SaleCount { get; set; }
    }
    public class PolicySalesForecast
    {
        public float[] ForecastedValues { get; set; }
        public float[] LowerBoundValues { get; set; }
        public float[] UpperBoundValues { get; set; }
    }
    public class ForecastService
    {
        private readonly MLContext _mlContext;
        public ForecastService()
        {
            _mlContext = new MLContext();
        }

        public PolicySalesForecast GetForecast(List<PolicySalesData> salesData, int horizon = 3)
        {
            int count = salesData.Count;

            if (count < 6)
                throw new Exception("Yeterli veri yok. En az 6 veri satırı beklenir.");

            var dataView = _mlContext.Data.LoadFromEnumerable(salesData);

            int windowSize = Math.Max(2, count / 4);
            int seriesLength = Math.Max(windowSize * 2 + 1, count / 2);
            int trainSize = Math.Max(seriesLength, count - horizon);

            var forecastingPipeline = _mlContext.Forecasting.ForecastBySsa(
                outputColumnName: "ForecastedValues",
                inputColumnName: "SaleCount",
                windowSize: windowSize,
                seriesLength: seriesLength,
                trainSize: trainSize,
                horizon: horizon,
                confidenceLevel: 0.95f,
                confidenceLowerBoundColumn: "LowerBoundValues",
                confidenceUpperBoundColumn: "UpperBoundValues"
            );

            var model = forecastingPipeline.Fit(dataView);
            var forecastingEngine = model.CreateTimeSeriesEngine<PolicySalesData, PolicySalesForecast>(_mlContext);

            return forecastingEngine.Predict();
        }


    }
}