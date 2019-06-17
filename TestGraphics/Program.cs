using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace TestGraphics
{
    class Program
    {
        private static string _output = @"C:\Users\tunii\source\repos\TestApplications\TestGraphics\TestGraphics\Output\";

        static void Main(string[] args)
        {
            // Create string to draw.
            var drawString = "Sample Text";
            var filePath = @"C:\Users\tunii\source\repos\TestApplications\TestGraphics\TestGraphics\Images\TestImage.jpg";

            using var bitmap = new Bitmap(filePath);
            using var graphics = Graphics.FromImage(bitmap);

            Brush brush = new SolidBrush(Color.Red);

            using var font = new Font("Arial", 90, FontStyle.Italic, GraphicsUnit.Pixel);

            var textSize = graphics.MeasureString(drawString, font);
            var position = new Point(bitmap.Width - ((int)textSize.Width + 10), bitmap.Height - ((int)textSize.Height + 10));

            graphics.DrawString(drawString, font, brush, position);

            var v = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            var w = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            var x = Directory.GetParent(@"./")?.Parent?.FullName ?? "Unknown";
            var y = Path.GetFullPath(@"./");
            var z = AppDomain.CurrentDomain.BaseDirectory;

            var outputFilenameWithPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.FullName, "Output", $"{Guid.NewGuid().ToString()}.jpg");

            // using var mStream = new MemoryStream();
            bitmap.Save(outputFilenameWithPath, ImageFormat.Jpeg);

            // bitmap.Save(mStream, ImageFormat.Png);

            // mStream.Position = 0;
            // return File(mStream.ToArray(), "image/png", file);

            Console.WriteLine($"\nPress any key to exit.");
        }

        static void ResizeImage(string filePath, int width, int height)
        {
            using var pngStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var image = new Bitmap(pngStream);
            var resized = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(resized);
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.DrawImage(image, 0, 0, width, height);

            /* resized.Save($"resized-{file}", ImageFormat.Png);
            Console.WriteLine($"Saving resized-{file} thumbnail"); */
        }
    }
}
