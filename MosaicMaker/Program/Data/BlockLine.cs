using System.Collections.Generic;

namespace MosaicMakerNS
{
    public sealed class BlockLine
    {
        #region Variables

        private readonly List<ColorBlock> _blocks;

        #endregion

        #region Properties

        public int Count { get { return _blocks.Count; } }

        #endregion

        #region Constructors

        public BlockLine(int capacity, LineFillMode mode)
        {
            _blocks = new List<ColorBlock>(capacity);

            if (mode == LineFillMode.FillNull)
                for (int i = 0; i < capacity; i++)
                    _blocks.Add(null);
        }

        #endregion

        public void Add(ColorBlock block)
        {
            _blocks.Add(block);
        }

        public void SetBlockAt(ColorBlock block, int index)
        {
            _blocks[index] = block;
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