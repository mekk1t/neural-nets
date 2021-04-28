using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Windows.Media.Imaging;

namespace ImageClusters.ViewModels
{
    public class MainWindowViewModel
    {
        public BitmapImage ImageSourceBefore { get; set; }
        public BitmapImage ImageSourceAfter { get; set; }
        public BitmapImage CustomPixelImage { get; set; }

        public MainWindowViewModel()
        {
            ImageSourceBefore = new Bitmap("Images/iconmonstr-generation-10-16.jpg").ToBitmapImage();
            CustomPixelImage = RandomBytes().CreateBitmap().ToBitmapImage();

            var image = new ImageBinarizer(new Bitmap("Images/iconmonstr-generation-10-16.jpg"));
            ImageSourceAfter = image.ImageBytes.CreateBitmap().ToBitmapImage();
        }

        private static byte[] RandomBytes()
        {
            var random = new Random();
            var result = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                result[i] = (byte)random.Next(0, 2);
            }

            return result;
        }
    }
}
