using System;
using System.Drawing;
using System.Windows.Forms;

namespace MosaicMakerNS
{
    public static class UIUtil
    {
        /// <summary>
        /// Makes sure that only the item indicated by the index is checked
        /// </summary>
        public static void SingleCheck(int index, params ToolStripMenuItem[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            foreach (var item in items)
                item.Checked = false;

            items[index].Checked = true;
        }

        /// <summary>
        /// Sets the controls Enabled property and
        ///  changes the alpha value of the BackColor
        /// </summary>
        public static void SetEnabled(Control ctrl, bool enabled)
        {
            SetEnabled(ctrl, null, enabled);
        }

        /// <summary>
        /// Sets the controls and the items Enabled property and
        ///  changes the alpha value of the BackColor
        /// </summary>
        public static void SetEnabled(Control ctrl, ToolStripItem item, bool enabled)
        {
            if (ctrl == null)
                throw new ArgumentNullException("ctrl");

            if (item != null)
                item.Enabled = enabled;

            if (ctrl is CheckedListBox)
            {
                ctrl.BackColor = enabled ?
                    Color.LightGray : Color.DimGray;

                return;
            }

            ctrl.Enabled = enabled;
            ctrl.BackColor = enabled ?
                Color.FromArgb(0xFF, ctrl.BackColor) :
                Color.FromArgb(0x80, ctrl.BackColor);
        }
    }
}