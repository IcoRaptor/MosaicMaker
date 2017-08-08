using System.Drawing;
using System.Windows.Forms;

namespace MosaicMakerNS
{
    public class MenuStripRenderer : ToolStripProfessionalRenderer
    {
        #region Properties

        public static Color Default { get { return Color.FromArgb(45, 45, 45); } }
        public static Color Light { get { return Color.FromArgb(70, 70, 70); } }
        public static Color Dark { get { return Color.FromArgb(15, 15, 15); } }

        #endregion

        #region Contructors

        public MenuStripRenderer() : this(new MenuStripColorTable()) { }

        private MenuStripRenderer(MenuStripColorTable table) : base(table) { }

        #endregion

        #region Render

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected && !e.Item.Pressed)
            {
                using (Brush b = new SolidBrush(Light))
                    e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
            }
            else if (e.Item.Pressed)
            {
                using (Brush b = new SolidBrush(Dark))
                    e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
            }
            else
            {
                using (Brush b = new SolidBrush(Default))
                    e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
            }
        }

        #endregion

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