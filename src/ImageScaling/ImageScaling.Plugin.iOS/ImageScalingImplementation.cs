using ImageScaling.Plugin.Abstractions;
using System;
using System.IO;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;


namespace ImageScaling.Plugin
{
    public class ImageScalingImplementation : ImageScalingBase
    {
        public override MemoryStream Scale (Stream stream, ImageType resultType, int width, int height, int quality)
        {
            using (var image = UIImage.LoadFromData(NSData.FromStream(stream)))
            {
                var scaledStream = new MemoryStream();
                using (var scaledImage = image.Scale(new SizeF(width, height)))
                {
                    GetData(scaledImage, resultType, quality).AsStream().CopyTo(scaledStream);
                }
                return scaledStream;
            }
        }

        public override MemoryStream ScaleIfNeeded (Stream stream, ImageType resultType, int maxDimension, int quality)
        {
            using (var image = UIImage.LoadFromData(NSData.FromStream(stream)))
            {
                var scaledStream = new MemoryStream();
                if (image.Size.Height >= maxDimension || image.Size.Width >= maxDimension)
                {
                    var ratio = image.Size.Height >= image.Size.Width 
                        ? image.Size.Height / maxDimension 
                        : image.Size.Width / maxDimension;
                    using (var scaledImage = image.Scale(new SizeF(image.Size.Width / ratio, image.Size.Height / ratio)))
                    {
                        GetData(scaledImage, resultType, quality).AsStream().CopyTo(scaledStream);
                    }
                }
                else
                {
                    GetData(image, resultType, quality).AsStream().CopyTo(scaledStream);                    
                }
                return scaledStream;
            }
        }

        private static NSData GetData(UIImage image, ImageType type, int quality)
        {
            var floatQuality = quality/100f;
            switch(type)
            {
                case ImageType.Jpg:
                    return image.AsJPEG(floatQuality);
                case ImageType.Png:
                    return image.AsPNG();
                default:
                    return image.AsJPEG(floatQuality);
            }
        } 
    }
}