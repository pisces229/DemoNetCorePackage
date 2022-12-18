using System.Drawing.Imaging;
using System.Drawing;

namespace DemoNetCorePackage.DemoDrawingCommon
{
    internal class Runner
    {
        public Runner() 
        {
        }
        public Task Run() 
        {
            using (var memoryStream = new MemoryStream())
            using (var fileStream = File.Create($"{Guid.NewGuid()}.png"))
            {
                using (var bitmap = new Bitmap(1100, 800))
                //using (var bitmap = new Bitmap())
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        //graphics.Clear(Color.LemonChiffon);
                        graphics.Clear(Color.Transparent);
                        //graphics.Clear(Color.White);
                        graphics.DrawString($"aceraeb {DateTime.Now}", new Font("新細明體", 16.0F), Brushes.Blue, new Point(10, 5));
                    }

                    bitmap.Save(fileStream, ImageFormat.Png);

                    var text = $"Aceraeb {DateTime.Now}";
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.Clear(Color.White);
                        graphics.DrawImage(bitmap, 0, 0);
                        var font = new Font("新細明體", 22, FontStyle.Bold);
                        var solidBrush = new SolidBrush(Color.LightGray);
                        graphics.TranslateTransform(bitmap.Width / 3, (bitmap.Height / 4) * (-1));
                        graphics.RotateTransform(45);
                        var point = new List<PointF>()
                            {
                                new PointF(350, -100),
                                new PointF(000, 100),
                                new PointF(700, 100),
                                new PointF(350, 300),
                                new PointF(000, 500),
                                new PointF(700, 500),
                                new PointF(350, 700)
                            };
                        point.ForEach(f =>
                        {
                            graphics.DrawString(text, font, solidBrush, f);
                        });
                    }
                    bitmap.Save(fileStream, ImageFormat.Png);
                }
            }
            return Task.CompletedTask;
        }
    }
}
