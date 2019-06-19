namespace TestGraphics
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    
    class Program
    {
        static void Main(string[] args)
        {
            // Create string to draw.
            var drawString = "Sample Text";
            var filePath = @"C:\Users\tunii\source\repos\TestApplications\TestGraphics\TestGraphics\Images\TestImage.jpg";

            AddText(filePath, drawString, Color.Teal, 120, 50, 50);

            // bitmap.Save(mStream, ImageFormat.Png);

            // mStream.Position = 0;
            // return File(mStream.ToArray(), "image/png", file);

            Console.WriteLine($"\nPress any key to exit.");
        }

        static void AddText(string imagePath, string text, Color color, int fontSize = 100, int positionX = 0, int positionY = 0)
        {
            using var bitmap = new Bitmap(imagePath);
            using var graphics = Graphics.FromImage(bitmap);

            Brush brush = new SolidBrush(color);

            using var font = new Font("Arial", fontSize, FontStyle.Regular, GraphicsUnit.Pixel);

            /*  var textSize = graphics.MeasureString(text, font);
                var position = new Point(bitmap.Width - ((int)textSize.Width + relativePositionX), bitmap.Height - ((int)textSize.Height + relativePositionY)); */

            var position = new Point(positionX, positionY);

            graphics.DrawString(text, font, brush, position);

            var outputFilenameWithPath = Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")), "Output", $"{Guid.NewGuid().ToString()}.jpg");

            // using var mStream = new MemoryStream();
            bitmap.Save(outputFilenameWithPath, ImageFormat.Jpeg);
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
