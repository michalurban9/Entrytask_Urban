namespace Entrytask_Urban.Models
{
    public class WeatherForecast
    {
        /// <summary>
        /// Temp Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Temp in C
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Temp in F
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Temp Summary
        /// </summary>
        public string? Summary { get; set; }
    }
}
