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
using ArcTouch.Core.ViewModels;

namespace ArcTouch.Droid.Views
{
    [Activity(Label = "Movie Details")]
    public class MovieActivity : BaseActivity<MovieViewModel>
    {
        protected override int ResourceLayoutId => Resource.Layout.activity_movie;
    }
}