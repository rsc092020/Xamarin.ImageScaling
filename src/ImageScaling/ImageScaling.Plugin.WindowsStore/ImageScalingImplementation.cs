using ImageScaling.Plugin.Abstractions;
using System;


namespace ImageScaling.Plugin
{
    public class ImageScalingImplementation : ImageScalingBase
    {
        public override MemoryStream Scale (Stream stream, ImageType resultType, int width, int height, int quality)
        {
            throw new NotImplementedException ();
        }

        public override MemoryStream ScaleIfNeeded (Stream stream, ImageType resultType, int maxDimension, int quality)
        {
            throw new NotImplementedException ();
        }
    }
}