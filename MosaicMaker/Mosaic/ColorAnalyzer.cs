using System.Collections.Generic;
using System.Drawing;

namespace MosaicMaker
{
    public class ColorAnalyzer : IClearable
    {
        #region Variables

        private List<ColorBlock> _colorBlocks =
            new List<ColorBlock>();

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public ColorAnalyzer(List<Color[,]> pixels)
        {
            foreach (var c in pixels)
                _colorBlocks.Add(new ColorBlock(c));
        }

        #endregion

        public void AnalyzeColors()
        {
            System.Threading.Thread.Sleep(1000);
        }

        public void Clear()
        {
            _colorBlocks.Clear();
        }
    }
}