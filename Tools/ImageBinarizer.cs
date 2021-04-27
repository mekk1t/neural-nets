using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace ImageClusters
{
    public class ImageBinarizer
    {
        public Bitmap Image { get; }
        public BitArray ImageBits { get; }
        public List<int> ImageInts { get; }

        public ImageBinarizer(Bitmap image)
        {
            Image = image;
            ImageBits = GetImagePixelsBinaryArray(image);
            ImageInts = GetImagePixelsIntArray(image);
        }

        private static List<int> GetImagePixelsIntArray(Bitmap image)
        {
            var ints = new List<int>();
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Width; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    if (pixel.R >= 235)
                        ints.Add(0);
                    else
                        ints.Add(1);
                }
            }

            return ints;
        }

        private static BitArray GetImagePixelsBinaryArray(Bitmap image)
        {
            var bools = new List<bool>();
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Width; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    if (pixel.R >= 235)
                        bools.Add(false);
                    else
                        bools.Add(true);
                }
            }

            return new BitArray(bools.ToArray());
        }
    }
}
