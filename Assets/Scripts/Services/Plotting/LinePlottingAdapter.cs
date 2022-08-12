using Roguelike.Core.Services.Plotting;
using UnityEngine;

namespace Roguelike.Services.Plotting
{
    public class LinePlottingAdapter : ILinePlotter
    {
        #region Fields
        public delegate bool CanPlotThrough(Vector3 position);
        const float DefaultStep = 1f;
        
        readonly CanPlotThrough canPlotThrough;
        readonly float step;
        #endregion
        
        #region Methods
        public LinePlottingAdapter(CanPlotThrough canPlotThrough, float step = DefaultStep)
        {
            this.canPlotThrough = canPlotThrough;
            this.step = step;
        }
        
        /// <remarks>
        /// Uses <a href="https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm">Bresenham's line algorithm</a>
        /// and Bresenham's principles of
        /// <a href="https://en.wikipedia.org/wiki/Incremental_error_algorithm">incremental error</a>.
        /// </remarks>
        public bool CanLineBePlotted(Vector3 startPosition, Vector3 targetPosition)
        {
            float xStep = startPosition.x < targetPosition.x ? step : -step;
            float yStep = startPosition.y < targetPosition.y ? step : -step;
            float xDifference = Mathf.Abs(targetPosition.x - startPosition.x);
            float yDifference = -Mathf.Abs(targetPosition.y - startPosition.y);
            float incrementalError = xDifference + yDifference;
            Vector3 linePoint = startPosition;
            
            bool isLinePlotted = linePoint == targetPosition;
            while (!isLinePlotted && canPlotThrough(linePoint))
            {
                float doubleIncrementalError = 2f * incrementalError;
                if (doubleIncrementalError >= yDifference)
                {
                    incrementalError += yDifference;
                    linePoint.x += xStep;
                }
                
                if (doubleIncrementalError <= xDifference)
                {
                    incrementalError += xDifference;
                    linePoint.y += yStep;
                }
                
                isLinePlotted = linePoint == targetPosition;
            }
            return isLinePlotted;
        }
        #endregion
    }
}