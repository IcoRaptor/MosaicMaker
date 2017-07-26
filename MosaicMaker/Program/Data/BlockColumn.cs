using System.Collections.Generic;

namespace MosaicMakerNS
{
    public sealed class BlockColumn
    {
        #region Variables

        private List<ColorBlock> _blocks;

        #endregion

        #region Properties

        public int Count { get { return _blocks.Count; } }

        #endregion

        #region Contructors

        public BlockColumn()
        {
            _blocks = new List<ColorBlock>();
        }

        #endregion

        public void Add(ColorBlock block)
        {
            _blocks.Add(block);
        }

        public ColorBlock GetBlock(int index)
        {
            return _blocks[index];
        }
    }
}