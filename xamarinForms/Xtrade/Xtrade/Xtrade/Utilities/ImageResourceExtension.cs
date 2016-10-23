﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Xtrade.Utilities
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            var imageSource = ImageSource.FromResource(Source);
            return imageSource;
        }
    }
}
