using System.Drawing;
using System.Windows.Forms;

namespace MosaicMakerNS
{
    public class MenuStripRenderer : ToolStripProfessionalRenderer
    {
        #region Contructors

        public MenuStripRenderer() : this(new MenuStripColorTable()) { }

        private MenuStripRenderer(MenuStripColorTable table) : base(table) { }

        #endregion

        #region Render

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected && !e.Item.Pressed)
            {
                using (Brush b = new SolidBrush(Color.FromArgb(60, 60, 60)))
                    e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
            }
            else if (e.Item.Pressed)
            {
                using (Brush b = new SolidBrush(Color.FromArgb(30, 30, 30)))
                    e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
            }
            else
            {
                using (Brush b = new SolidBrush(Color.FromArgb(45, 45, 45)))
                    e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
            }
        }

        #endregion

        #region ColorTable

        private class MenuStripColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground
            {
                get { return Color.FromArgb(30, 30, 30); }
            }

            public override Color MenuBorder
            {
                get { return Color.FromArgb(0, 0, 0, 0); }
            }

            public override Color ImageMarginGradientBegin
            {
                get { return Color.FromArgb(30, 30, 30); }
            }

            public override Color ImageMarginGradientMiddle
            {
                get { return Color.FromArgb(30, 30, 30); }
            }

            public override Color ImageMarginGradientEnd
            {
                get { return Color.FromArgb(30, 30, 30); }
            }
        }

        #endregion
    }
}