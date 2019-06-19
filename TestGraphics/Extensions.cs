namespace TestGraphics
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Text;

    /// <summary>
    /// The extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The draw rounded rectangle.
        /// </summary>
        /// <param name="graphics">
        /// The graphics.
        /// </param>
        /// <param name="pen">
        /// The pen.
        /// </param>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        /// <param name="cornerRadius">
        /// The corner radius.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The null argument exception.
        /// </exception>
        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            if (pen == null)
            {
                throw new ArgumentNullException(nameof(pen));
            }

            using (var path = RoundedRect(bounds, cornerRadius))
            {
                graphics.DrawPath(pen, path);
            }
        }

        /// <summary>
        /// The fill rounded rectangle.
        /// </summary>
        /// <param name="graphics">
        /// The graphics.
        /// </param>
        /// <param name="brush">
        /// The brush.
        /// </param>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        /// <param name="cornerRadius">
        /// The corner radius.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The null argument exception.
        /// </exception>
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            if (brush == null)
            {
                throw new ArgumentNullException(nameof(brush));
            }

            using (var path = RoundedRect(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }

        /// <summary>
        /// The rounded rectangle.
        /// </summary>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        /// <param name="radius">
        /// The radius.
        /// </param>
        /// <returns>
        /// The <see cref="GraphicsPath"/>.
        /// </returns>
        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            var diameter = radius * 2;
            var size = new Size(diameter, diameter);
            var arc = new Rectangle(bounds.Location, size);
            var path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
