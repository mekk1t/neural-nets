using System.Collections.Generic;
using System.Drawing;

namespace ImageClusters
{
    public class ImageBinarizer
    {
        public Bitmap Image { get; }
        public byte[] ImageBytes { get; }
        private readonly int _graySoftness;

        public ImageBinarizer(Bitmap image, int graySoftness)
        {
            _graySoftness = graySoftness;
            Image = image;
            ImageBytes = GetImagePixelsIntArray(image);
        }

        private byte[] GetImagePixelsIntArray(Bitmap image)
        {
            var bytes = new List<byte>();
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Width; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    if (pixel.R >= (255 - _graySoftness))
                        bytes.Add(0);
                    else
                        bytes.Add(1);
                }
            }

            return bytes.ToArray();
        }
    }
}
