using System;
using System.Collections.Generic;
using static System.Windows.Forms.CheckedListBox;

namespace MosaicMaker
{
    public class MosaicBuilder
    {
        #region Variables

        private ImageResizer _resizer;
        private ImageBuilder _builder;

        private List<string> _paths = new List<string>();

        #endregion

        #region Constructors

        public MosaicBuilder(CheckedItemCollection names,
            Dictionary<string, string> namePath)
        {
            foreach (var n in names)
                _paths.Add(namePath[(string)n]);

            _resizer = new ImageResizer();
            _builder = new ImageBuilder();
        }

        #endregion

        /// <summary>
        /// ThreadStart: Entry point for the mosaic
        /// </summary>
        public void Start()
        {
        }
    }
}