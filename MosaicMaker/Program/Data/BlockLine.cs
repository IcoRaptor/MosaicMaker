using System.Collections.Generic;

namespace MosaicMakerNS
{
    public class BlockLine
    {
        #region Properties

        public List<ColorBlock> Blocks { get; private set; }

        #endregion

        #region Contructors

        public BlockLine()
        {
            Blocks = new List<ColorBlock>();
        }

        #endregion
    }
}