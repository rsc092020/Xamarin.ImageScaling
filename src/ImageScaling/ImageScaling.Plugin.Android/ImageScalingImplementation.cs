using ImageScaling.Plugin.Abstractions;
using System;
using System.IO;
using Android.Graphics;


namespace ImageScaling.Plugin
{
    public class ImageScalingImplementation : ImageScalingBase
    {
        public override MemoryStream Scale (Stream stream, ImageType resultType, int width, int height, int quality)
        {
            if (quality < 0)
            {
                quality = 0;
            }
            if (quality > 100)
            {
                quality = 100;
            }

            using (var bitmap = BitmapFactory.DecodeStream(stream))
            {
                var scaledStream = new MemoryStream();
                using (var scaledBitmap = Bitmap.CreateScaledBitmap(bitmap, width, height, true))
                {
                    scaledBitmap.Compress(GetFormat(resultType), quality, scaledStream);
                    scaledBitmap.Recycle();
                    bitmap.Recycle();
                }
                return scaledStream;
            }
        }

        public override MemoryStream ScaleIfNeeded (Stream stream, ImageType resultType, int maxDimension, int quality)
        {
            if (quality < 0)
            {
                quality = 0;
            }
            if (quality > 100)
            {
                quality = 100;
            }

            using (var bitmap = BitmapFactory.DecodeStream(stream))
            {
                var scaledStream = new MemoryStream();
                if (bitmap.Height >= maxDimension || bitmap.Width >= maxDimension)
                {
                    var ratio = bitmap.Height >= bitmap.Width 
                        ? bitmap.Height / (double)maxDimension 
                        : bitmap.Width / (double)maxDimension;

                    using (var scaledBitmap = Bitmap.CreateScaledBitmap(bitmap, Convert.ToInt32(bitmap.Width / ratio), Convert.ToInt32(bitmap.Height / ratio), true))
                    {
                        scaledBitmap.Compress(GetFormat(resultType), quality, scaledStream);
                        scaledBitmap.Recycle();
                    }
                }
                else
                {
                    bitmap.Compress(GetFormat(resultType), quality, scaledStream);
                }
                bitmap.Recycle();

                return scaledStream;
            }
        }

        private static Bitmap.CompressFormat GetFormat(ImageType type)
        {
            switch(type)
            {
                case ImageType.Jpg:
                    return Bitmap.CompressFormat.Jpeg;
                case ImageType.Png:
                    return Bitmap.CompressFormat.Png;
                default:
                    return Bitmap.CompressFormat.Jpeg;
            }
        } 
    }
}