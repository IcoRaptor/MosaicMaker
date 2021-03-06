﻿namespace MosaicMakerNS
{
    /// <summary>
    /// Contains constant strings used by the UI
    /// </summary>
    public static class Strings
    {
        // MenuStrip

        public const string About =
            "==============================\n" +
            " MOSAIC MAKER\n" +
            "==============================\n\n" +
            " ~ Transform your images into mosaics ~\n\n" +
            "==============================\n\n" +
            " License: MIT\n" +
            " Copyright 2017 - Tim Redmann\n";

        // MainWindow

        public const string Title = "Mosaic Maker    |    ";
        public const string MosaicMode = "Mosaic Mode";
        public const string PixelMode = "Pixel Mode";

        public const string FormatError = "File format is not supported!";

        public const string Error = "An error occurred!\n\n";
        public const string Error2 = "Please check the properties of the file";
        public const string Error3 = "Please check the properties of the missing files";
        public const string TryAgain = "\nand try again!";

        public const string SaveError = "Image could not be saved!";
        public const string SaveSuccess = "Image saved successfully!";

        public const string Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp|TIFF|*.tif";

        public const string LabelFolder = "No folder loaded...";

        // ProgressDialog

        public const string Slicing = "Slicing loaded image...";
        public const string Analyzing = "Analyzing colors...";
        public const string Building = "Building final image...";
        public const string Finished = "Finished in: ";

        public const string OutOfMemory = "There is not enough memory...\n\n" +
            "Please select a smaller image or increase the element size!";
    }
}