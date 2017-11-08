using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace MosaicMakerNS
{
    public static class SizeUtil
    {
        /// <summary>
        /// Returns a new size that is evenly divisible by the element size
        /// </summary>
        public static Size GetNewImageSize(Size imgSize, Size elementSize)
        {
            int imgWidth = imgSize.Width;
            int imgHeight = imgSize.Height;
            int elementWidth = elementSize.Width;
            int elementHeight = elementSize.Height;

            int modWidth = imgWidth % elementWidth;
            int modHeight = imgHeight % elementHeight;

            if (modWidth != 0)
            {
                int offset = modWidth < elementWidth / 2 ? 0 : 1;
                imgWidth = (imgWidth / elementWidth + offset) * elementWidth;
            }

            if (modHeight != 0)
            {
                int offset = modHeight < elementHeight / 2 ? 0 : 1;
                imgHeight = (imgHeight / elementHeight + offset) * elementHeight;
            }

            return new Size(imgWidth, imgHeight);
        }

        /// <summary>
        /// Returns the element size of the checked RadioButton
        /// </summary>
        public static Size GetElementSize(Size imgSize, params RadioButton[] buttons)
        {
            if (buttons == null)
                throw new ArgumentNullException("buttons");

            Size size = new Size();

            foreach (var rb in buttons)
            {
                if (rb.Checked)
                {
                    string[] splits = rb.Text.Trim().Split(' ');
                    int w = int.Parse(splits[0], CultureInfo.InvariantCulture);
                    int h = int.Parse(splits[2], CultureInfo.InvariantCulture);

                    size.Width = w;
                    size.Height = h;

                    if (Settings.PixelStrip)
                        size.Height = imgSize.Height;

                    break;
                }
            }

            return size;
        }
    }
}