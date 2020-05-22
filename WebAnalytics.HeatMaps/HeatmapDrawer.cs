using System;
using System.Collections.Generic;
using System.Linq;
using ImageMagick;
using WebAnalytics.Core.Entities.Ontology;

namespace WebAnalytics.HeatMaps
{
    public interface IHeatmapDrawer
    {
        void CreateHeatmap(IEnumerable<RdfSession> sessions);
    }

    public class HeatmapDrawer : IHeatmapDrawer
    {
        private GradientProvider _gradientProvider;

        public HeatmapDrawer()
        {
            _gradientProvider = new GradientProvider();
        }

        public void CreateHeatmap(IEnumerable<RdfSession> sessions)
        {
            const int displayRadius = 40;
            const int groupRadius = 10;

            var points = sessions.SelectMany(s => s.Contains.Regions)
                                 .SelectMany(r => r.Contains.Variations)
                                 .SelectMany(v => v.Contains.Events)
                                 .Where(e => e is RdfSingleClickMouseEvent).ToList();

            int maxNearestCount = points.Count == 0
                ? 0
                : points.Max(point => points
                    .Count(p => Math.Abs(point.InRegionX - p.InRegionX) <= groupRadius &&
                        Math.Abs(point.InRegionY - p.InRegionY) <= groupRadius));

            int width = (int) 100;
            int height = (int) 100;
            using var img = new MagickImage(MagickColors.Transparent, width, height);
            uint[] pixels = new uint[width * height];
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

                    byte color =
                        intensity == 0 ? (byte) 240 :
                        intensity >= 1 ? (byte) 0 :
                        Convert.ToByte((1 - intensity) * 240);

                    // var pixelColorHsl = new ColorHSL(intensity * 240 / 255, .7, .5);
                    drawables.FillColor(_gradientProvider.GetColorAtValue(intensity)).Point(x, y);
                }
            }

            drawables.Draw(img);
            img.Write("/home/renerick/test/images/test1.png");
        }
    }
}