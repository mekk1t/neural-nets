using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Tools;

namespace ImageClusters.ViewModels
{
    public class MainWindowViewModel
    {
        public List<BitmapImage> ImagesStart { get; set; }
        public List<BitmapImage> ImagesBinarized { get; set; }

        public List<ClusterBitmapImage> Clusters { get; set; }

        public MainWindowViewModel()
        {
            ImagesStart = ReadIcons("Images");
            var imagesBinarized = ReadIconsAsBitmap("Images").Select(icon => new ImageBinarizer(icon)).ToList();
            ImagesBinarized = imagesBinarized.Select(icon => icon.ImageBytes.CreateBitmap().ToBitmapImage()).ToList();
            var imagesBinarizedBytes = imagesBinarized.Select(im => im.ImageBytes);
            var neuralNet = new NeuralNet(0.80M, imagesBinarizedBytes.First().Length);
            foreach (var image in imagesBinarizedBytes)
            {
                neuralNet.Process(image);
            }

            Clusters = neuralNet.Clusters
                .Select(cluster =>
                    new ClusterBitmapImage(
                        cluster.Key.TWeights.ToArray().CreateBitmap().ToBitmapImage(),
                        cluster.Select(element => element.TWeights.ToArray().CreateBitmap().ToBitmapImage())))
                .ToList();

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
