using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.Target;
using Square.Picasso;

namespace GenesisAuto.Droid.CustomBindings
{
    public class ImageUrlToLoadBinding : MvxAndroidTargetBinding
    {
        int _defaultImage;
        int _errorImage;
        public ImageUrlToLoadBinding(View target, int defaultImage, int errorImage) : base(target)
        {
            _defaultImage = defaultImage;
            _errorImage = errorImage;
        }

        public int PlaceHolderToUse { get; set; }

        protected override void SetValueImpl(object target, object value)
        {
            var imageView = (ImageView)target;
            Picasso.With(imageView.Context)
                    .Load(value as string)
                    .Placeholder(_defaultImage)
                    .Error(_errorImage)
                    .Into(imageView);
        }

        public override Type TargetType => typeof(string);
    }
}