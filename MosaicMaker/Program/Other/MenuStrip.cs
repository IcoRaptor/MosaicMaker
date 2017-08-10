using System.Drawing;
using System.Windows.Forms;

namespace MosaicMakerNS
{
    public sealed class MenuStripRenderer : ToolStripProfessionalRenderer
    {
        #region Properties

        public static Color Default { get { return Color.FromArgb(45, 45, 45); } }
        public static Color Light { get { return Color.FromArgb(70, 70, 70); } }
        public static Color Dark { get { return Color.FromArgb(15, 15, 15); } }
        public static Color Dim { get { return Color.FromArgb(30, 30, 30); } }

        #endregion

        #region Contructors

        public MenuStripRenderer() : this(new MenuStripColorTable()) { }

        private MenuStripRenderer(MenuStripColorTable table) : base(table) { }

        #endregion

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected && !e.Item.Pressed)
            {
                using (Brush b = new SolidBrush(Light))
                    e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
            }
            else if (e.Item.Pressed)
            {
                using (Brush b = new SolidBrush(Dim))
                    e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
            }
            else
            {
                using (Brush b = new SolidBrush(Default))
                    e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            using (Brush b = new SolidBrush(Default))
                e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            using (Brush b = new SolidBrush(Color.Gray))
                e.Graphics.FillRectangle(b, Rectangle.Inflate(e.ImageRectangle, -6, -6));
        }

        #region ColorTable

        private class MenuStripColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground
            {
                get { return Dark; }
            }

            public override Color MenuBorder
            {
                get { return Color.Transparent; }
            }

            public override Color ImageMarginGradientBegin
            {
                get { return Dark; }
            }

            public override Color ImageMarginGradientMiddle
            {
                get { return Dark; }
            }

            public override Color ImageMarginGradientEnd
            {
                get { return Dark; }
            }
        }

        #endregion
    }
}