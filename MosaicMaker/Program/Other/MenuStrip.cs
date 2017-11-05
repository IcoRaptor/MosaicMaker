using System.Drawing;
using System.Windows.Forms;

namespace MosaicMakerNS
{
    /// <summary>
    /// Custom renderer for the MenuStrip
    /// </summary>
    public sealed class MenuStripRenderer : ToolStripProfessionalRenderer
    {
        #region Variables

        private Color _arrowColor = _Gray150;

        #endregion

        #region Properties

        private static Color _Default { get { return Color.FromArgb(45, 45, 45); } }
        private static Color _Gray150 { get { return Color.FromArgb(150, 150, 150); } }
        private static Color _Gray70 { get { return Color.FromArgb(70, 70, 70); } }
        private static Color _Gray30 { get { return Color.FromArgb(30, 30, 30); } }
        private static Color _Gray15 { get { return Color.FromArgb(15, 15, 15); } }

        #endregion

        #region Contructors

        public MenuStripRenderer() : base(new MenuStripColorTable()) { }

        #endregion

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Item.ContentRectangle;

            if (e.Item.Pressed)
            {
                using (Brush b = new SolidBrush(_Gray30))
                    g.FillRectangle(b, rect);

                _arrowColor = Color.DeepSkyBlue;
            }
            else if (e.Item.Selected)
            {
                using (Brush b = new SolidBrush(_Gray70))
                    g.FillRectangle(b, rect);

                _arrowColor = _Gray150;
            }
            else
            {
                using (Brush b = new SolidBrush(_Default))
                    g.FillRectangle(b, rect);

                _arrowColor = _Gray150;
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Item.ContentRectangle;

            using (Brush b = new SolidBrush(_Default))
                g.FillRectangle(b, rect);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.ImageRectangle;

            using (Brush b = new SolidBrush(Color.DeepSkyBlue))
                g.FillRectangle(b, Rectangle.Inflate(rect, -6, -6));
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.ArrowRectangle;

            using (Brush b = new SolidBrush(_arrowColor))
            {
                Point center = new Point(rect.Left + rect.Width / 2,
                    rect.Top + rect.Height / 2);

                ScaleArrowOffsetsIfNeeded();

                int Offset4Y = Offset2Y * 2;

                Point p1 = new Point(center.X - Offset2X, center.Y - Offset4Y);
                Point p2 = new Point(center.X - Offset2X, center.Y + Offset4Y);
                Point p3 = new Point(center.X + Offset2X, center.Y);

                Point[] arrow = new Point[] { p1, p2, p3 };

                g.FillPolygon(b, arrow);
            }
        }

        #region ColorTable

        /// <summary>
        /// Custom color table for the renderer
        /// </summary>
        private class MenuStripColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground
            {
                get { return _Gray15; }
            }

            public override Color MenuBorder
            {
                get { return Color.Transparent; }
            }

            public override Color ImageMarginGradientBegin
            {
                get { return _Gray15; }
            }

            public override Color ImageMarginGradientMiddle
            {
                get { return _Gray15; }
            }

            public override Color ImageMarginGradientEnd
            {
                get { return _Gray15; }
            }
        }

        #endregion
    }
}