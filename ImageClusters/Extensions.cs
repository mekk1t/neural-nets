using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImageClusters
{
    public static class Extensions
    {
        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            using var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);
            ms.Position = 0;
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            return bitmapImage;
        }

        public static byte[,] AsTwoDimensional(this byte[] bytes, int slice)
        {
            if (bytes.Length % slice != 0) throw new ArgumentException("Длина массива не делится на слайс.", nameof(bytes));

            var result = new byte[slice, slice];
            for (int i = 0; i < slice; i++)
            {
                for (int j = 0; j < slice; j++)
                {
                    result[i, j] = bytes[slice * i + j];
                }
            }

            return result;
        }

        public static Bitmap CreateBitmap(this byte[] bytes)
        {
            using var ms = new MemoryStream(bytes);
            return new Bitmap(ms);
        }

        public static void SetBlackAndWhitePixels(this Bitmap bitmap, byte[,] bits)
        {
            var limit = bits.GetUpperBound(0);
            for (int x = 0; x < limit; x++)
            {
                for (int y = 0; y < limit; y++)
                {
                    if (bits[x, y] == 1) bitmap.SetPixel(x, y, Color.Black);
                    if (bits[x, y] == 0) bitmap.SetPixel(x, y, Color.White);
                }
            }
        }
    }
}
