using System.Collections.Generic;

namespace MosaicMakerNS
{
    public sealed class BlockLine
    {
        #region Variables

        private readonly List<ImageBlock> _blocks =
            new List<ImageBlock>();

        #endregion

        #region Properties

        public int Count { get { return _blocks.Count; } }

        #endregion

        public void Add(ImageBlock block)
        {
            _blocks.Add(block);
        }

        public ImageBlock GetBlock(int index)
        {
            return _blocks[index];
        }

        public void Reverse()
        {
            _blocks.Reverse();
        }
    }
}