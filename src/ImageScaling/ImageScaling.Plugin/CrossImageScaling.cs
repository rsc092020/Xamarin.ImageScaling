using ImageScaling.Plugin.Abstractions;
using System;
using System.Threading;

namespace ImageScaling.Plugin
{
  /// <summary>
  /// Cross platform ImageScaling implementations
  /// </summary>
    public class CrossImageScaling
    {
        static Lazy<IImageScaling> Implementation = new Lazy<IImageScaling>(() => CreateImageScaling(), LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Current settings to use
        /// </summary>
        public static IImageScaling Current
        {
            get
            {
                var ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        static IImageScaling CreateImageScaling()
        {
#if PORTABLE
            return null;
#else
            return new ImageScalingImplementation();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
