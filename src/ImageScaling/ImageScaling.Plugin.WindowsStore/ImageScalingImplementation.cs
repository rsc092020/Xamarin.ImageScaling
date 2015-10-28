using ImageScaling.Plugin.Abstractions;
using System;


namespace ImageScaling.Plugin
{
    public class ImageScalingImplementation : ImageScalingBase
    {
        protected override MemoryStream ScaleWithQuality (Stream stream, ImageType resultType, int width, int height, int quality)
        {
            throw new NotImplementedException ();
        }

        protected override MemoryStream ScaleIfNeededWithQuality (Stream stream, ImageType resultType, int maxDimension, int quality)
        {
            throw new NotImplementedException ();
        }
    }
}