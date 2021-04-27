using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ImageClusters.ViewModels
{
    public class MainWindowViewModel
    {
        public BitmapImage ImageSource { get; set; }
        public Array PixelArray { get; set; }
        public List<Color> ImageColors { get; set; } = new();
        // 235 - 255 => не закрашеноx

        public MainWindowViewModel()
        {
            var bitmap = new Bitmap("Images/iconmonstr-generation-10-16.jpg");
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    ImageColors.Add(pixel);
                }
            }
        }
    }
}
