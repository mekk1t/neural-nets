using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageClusters
{
    public class ClusterBitmapImage : List<BitmapImage>, IGrouping<BitmapImage, BitmapImage>
    {
        public ClusterBitmapImage(BitmapImage key) : base() => Key = key;

        public ClusterBitmapImage(BitmapImage key, IEnumerable<BitmapImage> collection) : base(collection) => Key = key;

        public BitmapImage Key { get; }
    }
}
