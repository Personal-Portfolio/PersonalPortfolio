using System;
using System.Globalization;

namespace MatBlazor
{
    public class MatToastTransitionState
    {
        private int Duration { get; init; }
        private double MaxOpacity { get; init; }
        private double Ratio { get; init; }

        public int RemainingMilliseconds { get; private init; }
        public string Opacity => (Ratio * MaxOpacity).ToString("0.##", CultureInfo.InvariantCulture);
        public string ProgressPercentage => (Ratio * 100).ToString("0");

        public static MatToastTransitionState ForRequiredInteraction(int maximumOpacity) => new()
        {
            Duration = 0,
            MaxOpacity = Convert.ToDouble(maximumOpacity) / 100,
            RemainingMilliseconds = 0,
            Ratio = 1
        };

        private MatToastTransitionState()
        {
        }

        public MatToastTransitionState(MatToastTransitionTimer timer, int maximumOpacity)
        {
            Duration = timer.Duration;
            MaxOpacity = Convert.ToDouble(maximumOpacity) / 100;
            RemainingMilliseconds = Convert.ToInt32(timer.RemainingMilliseconds);

            if (Duration == 0 || RemainingMilliseconds <= 0)
            {
                Ratio = 0;
            }
            else if (timer.RemainingMilliseconds > Duration)
            {
                Ratio = 1;
            }
            else
            {
                Ratio = timer.RemainingMilliseconds / Duration;
            }
        }
    }
}