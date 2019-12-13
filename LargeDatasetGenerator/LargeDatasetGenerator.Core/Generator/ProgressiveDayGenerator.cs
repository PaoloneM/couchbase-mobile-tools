using System;
using System.Threading.Tasks;

using LargeDatasetGenerator.Core.Generator;

using sly.lexer;

namespace LargeDatasetGenerator.Generator
{

    /// <summary>
    /// A generator that randomly generates a date between two given dates
    /// </summary>
    public sealed class ProgressiveDayGenerator : IDataGenerator
    {
        #region Variables

        private readonly DateTimeOffset _max;

        private readonly DateTimeOffset _min;

        private int currentIteration = 0;

        private int maxIterations = 1;

        #endregion

        #region Properties

        /// <inheritdoc />
        public string Description { get; } = "Randomly generates a day (yyyyMMdd) between the given min and max";

        /// <inheritdoc />
        public string Signature { get; } = "{{progressiveday(min: Date = new Date(1970, 1, 1, 0, 0, 0), max: Date = new Date())}}";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parameters">The parameters of the date function call</param>
        public ProgressiveDayGenerator(string parameters)
        {
            var parser = new DateTimeGeneratorParser(parameters);
            (DateTimeOffset min, DateTimeOffset max) result = parser.Parse();
            _min = result.min;
            _max = result.max;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProgressiveDayGenerator() : this("()")
        {
            
        }

        public void setProgress(int i, int Count){
            currentIteration = i;
            maxIterations = Count;
        }

        #endregion

        #region IDataGenerator

        /// <inheritdoc />
        public Task<object> GenerateValueAsync()
        {
            double diff = (currentIteration * (_max.Ticks - _min.Ticks) / maxIterations);
            var nextTicks = Convert.ToInt64(Math.Round(diff)) + _min.Ticks;
            return Task.FromResult<object>(new DateTimeOffset(nextTicks, TimeSpan.Zero).ToString("yyyyMMdd", null));
        }

        #endregion
    }
}