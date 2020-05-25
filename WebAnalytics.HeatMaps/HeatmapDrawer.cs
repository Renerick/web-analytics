using System;
using System.Collections.Generic;
using System.Linq;
using ImageMagick;
using WebAnalytics.Core.Entities.Ontology;

namespace WebAnalytics.HeatMaps
{
    public interface IHeatmapDrawer
    {
        byte[] CreateHeatmap(IEnumerable<RdfSession> sessions);
    }

    public class HeatmapDrawer : IHeatmapDrawer
    {
        private GradientProvider _gradientProvider;

        public HeatmapDrawer()
        {
            _gradientProvider = new GradientProvider();
        }

        public byte[] CreateHeatmap(IEnumerable<RdfSession> sessions)
        {
            const int displayRadius = 40;
            const int groupRadius = 10;

            var variations = sessions.SelectMany(s => s.Contains.Regions)
                                     .SelectMany(r => r.Contains.Variations).ToList();
            var points = variations
                         .SelectMany(v => v.Contains.Events)
                         .Where(e => e is RdfSingleClickMouseEvent).ToList();

            int maxNearestCount = points.Count == 0
                ? 0
                : points.Max(point => points
                    .Count(p => Math.Abs(point.InRegionX - p.InRegionX) <= groupRadius &&
                        Math.Abs(point.InRegionY - p.InRegionY) <= groupRadius));

            int width = variations.Max(v => (int) v.Width);
            int height = points.Max(p => p.InRegionY + 100);
            using var img = new MagickImage(MagickColors.Transparent, width, height);
            var drawables = new Drawables();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var intensity = points
                                    .Select(point =>
                                        Math.Sqrt(Math.Pow(x - point.InRegionX, 2) +
                                            Math.Pow(y - point.InRegionY, 2)))
                                    .Where(distance => distance < displayRadius)
                                    .Sum(distance => 1.0d - distance / displayRadius);

                    intensity = Math.Abs(intensity) < 0.01 ? 0 : intensity / maxNearestCount;

                    drawables.FillColor(_gradientProvider.GetColorAtValue(intensity)).Point(x, y);
                }
            }

            drawables.Draw(img);
            return img.ToByteArray(MagickFormat.Png);
        }
    }
}