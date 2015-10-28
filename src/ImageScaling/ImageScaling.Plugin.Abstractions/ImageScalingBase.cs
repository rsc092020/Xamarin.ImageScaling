using System;
using System.IO;
using System.Threading.Tasks;

namespace ImageScaling.Plugin.Abstractions
{
    public interface IImageScaling
    {
        /// <summary>
        /// Scale the specified image stream to the given width and height with specified quality.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <param name="resultType">Type of resulting image.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="quality">Quality (This should be 0-100, if less than 0, it will be 0, if more, it will be 100).</param>
        MemoryStream Scale(Stream stream, ImageType resultType, int width, int height, int quality);

        /// <summary>
        /// Scale the specified image stream proportionally to max dimension with specified quality.
        /// If it already fits in the maxDimension, then only the quality is applied.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <param name="resultType">Type of resulting image.</param>
        /// <param name="maxDimension">Max value that width or height can be.</param>
        /// <param name="quality">Quality (This should be 0-100, if less than 0, it will be 0, if more, it will be 100).</param>
        MemoryStream ScaleIfNeeded(Stream stream, ImageType resultType, int maxDimension, int quality);

        /// <summary>
        /// Scale asynchronously.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <param name="resultType">Type of resulting image.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="quality">Quality (This should be 0-100, if less than 0, it will be 0, if more, it will be 100).</param>
        Task<MemoryStream> ScaleAsync(Stream stream, ImageType resultType, int width, int height, int quality);

        /// <summary>
        /// Scale if needed asynchronously.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <param name="resultType">Type of resulting image.</param>
        /// <param name="maxDimension">Max value that width or height can be.</param>
        /// <param name="quality">Quality (This should be 0-100, if less than 0, it will be 0, if more, it will be 100).</param>
        Task<MemoryStream> ScaleIfNeededAsync(Stream stream, ImageType resultType, int maxDimension, int quality);
    }

    public abstract class ImageScalingBase : IImageScaling
    {
        public abstract MemoryStream Scale(Stream stream, ImageType resultType, int width, int height, int quality);

        public abstract MemoryStream ScaleIfNeeded(Stream stream, ImageType resultType, int maxDimension, int quality);

        public Task<MemoryStream> ScaleAsync(Stream stream, ImageType resultType, int width, int height, int quality)
        {
            return Task.Run(() => Scale(stream, resultType, width, height, quality));
        }

        public Task<MemoryStream> ScaleIfNeededAsync(Stream stream, ImageType resultType, int maxDimension, int quality)
        {
            return Task.Run(() => ScaleIfNeeded(stream, resultType, maxDimension, quality));
        }
    }
}
