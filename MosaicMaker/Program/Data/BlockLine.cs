using System.Collections.Generic;

namespace MosaicMakerNS
{
    /// <summary>
    /// Represents a line of ColorBlocks
    /// </summary>
    public sealed class BlockLine
    {
        #region Variables

        private readonly List<ColorBlock> _blocks;

        #endregion

        #region Properties

        /// <summary>
        /// Number of ColorBlocks in the line
        /// </summary>
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

        /// <summary>
        /// Adds a ColorBlock to the line
        /// </summary>
        public void Add(ColorBlock block)
        {
            _blocks.Add(block);
        }

        /// <summary>
        /// Sets a ColorBlock at the given index
        /// </summary>
        public void SetBlock(ColorBlock block, int index)
        {
            _blocks[index] = block;
        }

        /// <summary>
        /// Gets the ColorBlock from the given index
        /// </summary>
        public ColorBlock GetBlock(int index)
        {
            return _blocks[index];
        }

        /// <summary>
        /// Reverses the line
        /// </summary>
        public void Reverse()
        {
            _blocks.Reverse();
        }
    }
}