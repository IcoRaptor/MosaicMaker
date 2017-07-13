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

        public MosaicBuilder(CheckedItemCollection names, Dictionary<string, string> pathDict)
        {
            foreach (var n in names)
                _paths.Add(pathDict[(string)n]);

            _resizer = new ImageResizer();
            _builder = new ImageBuilder();
        }

        #endregion

        public void Start()
        {
            string msg = string.Empty;

            foreach (var val in _paths)
                msg = string.Concat(msg, val, "\n");

            System.Windows.Forms.MessageBox.Show(msg);
        }
    }
}