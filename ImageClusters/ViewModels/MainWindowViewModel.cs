using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Tools;

namespace ImageClusters.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<BitmapImage> _imagesStart = new();
        public List<BitmapImage> ImagesStart
        {
            get => _imagesStart;
            set
            {
                _imagesStart = value;
                NotifyPropertyChanged(nameof(ImagesStart));
            }
        }

        private List<BitmapImage> _imagesBinarized = new();
        public List<BitmapImage> ImagesBinarized
        {
            get => _imagesBinarized;
            set
            {
                _imagesBinarized = value;
                NotifyPropertyChanged(nameof(ImagesBinarized));
            }
        }

        private Dictionary<BitmapImage, List<BitmapImage>> _clusters = new();
        public Dictionary<BitmapImage, List<BitmapImage>> Clusters
        {
            get => _clusters;
            set
            {
                _clusters = value;
                NotifyPropertyChanged(nameof(Clusters));
            }
        }

        public ICommand ProcessCommand { get; set; }

        public int GraySoftness { get; set; } = 100;
        public decimal ThresholdLevel { get; set; } = 0.80M;

        public MainWindowViewModel()
        {
            ProcessCommand = new CustomCommand(ProcessImages);
        }

        private void ProcessImages()
        {
            ImagesStart = ReadIcons("Images");
            var imagesBinarized = ReadIconsAsBitmap("Images").Select(icon => new ImageBinarizer(icon, GraySoftness)).ToList();
            ImagesBinarized = imagesBinarized.Select(icon => icon.ImageBytes.CreateBitmap().ToBitmapImage()).ToList();
            var imagesBinarizedBytes = imagesBinarized.Select(im => im.ImageBytes);
            var neuralNet = new NeuralNet(ThresholdLevel, imagesBinarizedBytes.First().Length);
            foreach (var image in imagesBinarizedBytes)
            {
                neuralNet.Process(image);
            }

            var _clusters = new Dictionary<BitmapImage, List<BitmapImage>>();
            neuralNet.Clusters.ForEach(cluster =>
            {
                _clusters.Add(
                    cluster.Key.TWeights.ToArray().CreateBitmap().ToBitmapImage(),
                    cluster.Select(cl => cl.TWeights.ToArray().CreateBitmap().ToBitmapImage()).ToList());
            });

            Clusters = _clusters;
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
