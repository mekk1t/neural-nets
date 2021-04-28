using System.Collections.Generic;
using System.Drawing;

namespace ImageClusters
{
    public class ImageBinarizer
    {
        public Bitmap Image { get; }
        public byte[] ImageBytes { get; }

        public ImageBinarizer(Bitmap image)
        {
            Image = image;
            ImageBytes = GetImagePixelsIntArray(image);
        }

        private static byte[] GetImagePixelsIntArray(Bitmap image)
        {
            var bytes = new List<byte>();
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Width; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    if (pixel.R >= 235)
                        bytes.Add(0);
                    else
                        bytes.Add(1);
                }
            }

            return bytes.ToArray();
        }
    }
}
