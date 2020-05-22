using System;
using System.Collections.Generic;
using System.Linq;
using ImageMagick;

namespace WebAnalytics.HeatMaps
{
    public class GradientProvider
    {
        private List<(MagickColor color, double value)> _colors = new List<(MagickColor color, double value)>();

        public GradientProvider()
        {
            _colors.Clear();
            _colors.Add((MagickColor.FromRgb(0, 0, 0), 0.0f)); // Black.
            _colors.Add((MagickColor.FromRgb(0, 0, 255), 0.2f)); // Blue.
            _colors.Add((MagickColor.FromRgb(0, 255, 255), 0.4f)); // Cyan.
            _colors.Add((MagickColor.FromRgb(0, 255, 0), 0.6f)); // Green.
            _colors.Add((MagickColor.FromRgb(255, 255, 0), 0.8f)); // Yellow.
            _colors.Add((MagickColor.FromRgb(255, 0, 0), 1.0f)); // Red.
        }

        public MagickColor GetColorAtValue(double value)
        {
            if (_colors.Count == 0)
                return null;

            for (int i = 0; i < _colors.Count; i++)
            {
                var point = _colors[i];
                if (value < point.value)
                {
                    var nextPoint = _colors[Math.Max(0, i - 1)];
                    var valueDiff = nextPoint.value - point.value;
                    var fractBetween = Math.Abs(valueDiff) < 0.00001 ? 0 : (value - point.value) / valueDiff;
                    var red = (byte) ((nextPoint.color.R - point.color.R) * fractBetween + point.color.R);
                    var green = (byte) ((nextPoint.color.G - point.color.G) * fractBetween + point.color.G);
                    var blue = (byte) ((nextPoint.color.B - point.color.B) * fractBetween + point.color.B);
                    return MagickColor.FromRgb(red, green, blue);
                }
            }

            return _colors.Last().color;
        }
    }
}