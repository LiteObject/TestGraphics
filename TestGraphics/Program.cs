namespace TestGraphics
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    /// The program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            // Create string to draw.
            var drawString = "Sample Text";
            var filePath = Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")), "Images", "TestImage.jpg");

            AddText(filePath, drawString, Color.Teal, 120, 50, 50);

            /*  bitmap.Save(mStream, ImageFormat.Png);
                mStream.Position = 0;
                return File(mStream.ToArray(), "image/png", file); 
                */

            Console.WriteLine($"\nPress any key to exit.");
        }

        /// <summary>
        /// The add text.
        /// </summary>
        /// <param name="imagePath">
        /// The image path.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <param name="fontSize">
        /// The font size.
        /// </param>
        /// <param name="positionX">
        /// The position x.
        /// </param>
        /// <param name="positionY">
        /// The position y.
        /// </param>
        private static void AddText(string imagePath, string text, Color color, int fontSize = 100, int positionX = 0, int positionY = 0)
        {
            using (var bitmap = new Bitmap(imagePath))
            using (var graphics = Graphics.FromImage(bitmap))
            using (var font = new Font("Arial", fontSize, FontStyle.Regular, GraphicsUnit.Pixel))
            {
                Brush brush = new SolidBrush(color);

                /*  var textSize = graphics.MeasureString(text, font);
                var position = new Point(bitmap.Width - ((int)textSize.Width + relativePositionX), bitmap.Height - ((int)textSize.Height + relativePositionY)); */

                var position = new Point(positionX, positionY);

                graphics.DrawString(text, font, brush, position);

                var outputFilenameWithPath = Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")), "Output", $"{Guid.NewGuid().ToString()}.jpg");

                // using var mStream = new MemoryStream();
                bitmap.Save(outputFilenameWithPath, ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// The resize image.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        private static void ResizeImage(string filePath, int width, int height)
        {
            using (var pngStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var image = new Bitmap(pngStream))
            {
                var resized = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resized))
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, width, height);

                    /* resized.Save($"resized-{file}", ImageFormat.Png);
                    Console.WriteLine($"Saving resized-{file} thumbnail"); */
                }
            }
        }
    }
}
