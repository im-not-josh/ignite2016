using Xamarin.Forms;
using Xtrade.Droid;
using Android.App;
using Android.Support.Design.Widget;
using Xtrade.Managers;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using View = Android.Views.View;

[assembly: Dependency(typeof(Snacker))]
namespace Xtrade.Droid
{
    public class Snacker : ISnacker
    {
        public void ShowSnack(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Activity context = CrossCurrentActivity.Current.Activity;
                View rootView = context.FindViewById(Android.Resource.Id.Content);
                Snackbar.Make(rootView, message, Snackbar.LengthLong).Show();
            });
        }
    }
}