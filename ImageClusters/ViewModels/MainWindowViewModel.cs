using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace ImageClusters.ViewModels
{
    public class MainWindowViewModel
    {
        public List<BitmapImage> ImagesStart { get; set; }
        public List<BitmapImage> ImagesBinarized { get; set; }

        public MainWindowViewModel()
        {
            ImagesStart = ReadIcons("Images");
            ImagesBinarized = ReadIconsAsBitmap("Images").Select(icon => new ImageBinarizer(icon).ImageBytes.CreateBitmap().ToBitmapImage()).ToList();
            //ImageSourceBefore = new Bitmap("Images/iconmonstr-generation-10-16.jpg").ToBitmapImage();
            //CustomPixelImage = RandomBytes().CreateBitmap().ToBitmapImage();

            //var image = new ImageBinarizer(new Bitmap("Images/iconmonstr-generation-10-16.jpg"));
            //ImageSourceAfter = image.ImageBytes.CreateBitmap().ToBitmapImage();
        }

        private static List<Bitmap> ReadIconsAsBitmap(string imagesDirectory)
        {
            var files = Directory.GetFiles(imagesDirectory);
            return files.Select(file => new Bitmap(file)).ToList();
        }

        private static List<BitmapImage> ReadIcons(string imagesDirectory)
        {
            var files = Directory.GetFiles(imagesDirectory);
            return files.Select(file => new Bitmap(file).ToBitmapImage()).ToList();
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
