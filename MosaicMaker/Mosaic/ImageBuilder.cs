using System.Collections.Generic;
using System.Drawing;

namespace MosaicMaker
{
    public class ImageBuilder : IClearable
    {
        #region Properties

        public Bitmap FinalImage { get; set; }

        #endregion

        #region Constructors

        public ImageBuilder(Bitmap image, List<List<ColorBlock>> slicedImage,
            Dictionary<int[], int> indexErrors, List<List<int>> errors)
        {
            FinalImage = image;
        }

        #endregion

        public void BuildImage()
        {
        }

        public void Clear()
        {
            FinalImage = null;
        }
    }
}