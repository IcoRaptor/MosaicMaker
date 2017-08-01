using System.Collections.Generic;

namespace MosaicMakerNS
{
    public sealed class BlockColumn
    {
        #region Variables

        private readonly List<ColorBlock> _blocks =
            new List<ColorBlock>();

        #endregion

        #region Properties

        public int Count { get { return _blocks.Count; } }

        #endregion

        public void Add(ColorBlock block)
        {
            _blocks.Add(block);
        }

        public ColorBlock GetBlock(int index)
        {
            return _blocks[index];
        }

        public void Reverse()
        {
            _blocks.Reverse();
        }
    }
}