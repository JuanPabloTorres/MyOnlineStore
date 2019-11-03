using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.Design.Widget;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using MyOnlineStore.Global;
using Xamarin.Forms.Xaml;


[assembly: ExportRenderer(typeof(Shell), typeof(MyOnlineStore.Droid.CustomRenderers.CustomShellRenderer))]
namespace MyOnlineStore.Droid.CustomRenderers
{
    public class CustomShellRenderer : ShellRenderer
    {
        //private Color StartColor { get; set; } = Color.FromHex("#43536c");
        //private Color EndColor { get; set; } =  Color.FromHex("#313d4e");

        private Color StartColor { get; set; } = Color.White;
        private Color EndColor { get; set; } = Color.Black;

        public CustomShellRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementSet(Shell element)
        {
            base.OnElementSet(element);
        }

        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            var renderer = base.CreateShellSectionRenderer(shellSection);
            return (IShellSectionRenderer)renderer;
        }

        protected override IShellFlyoutRenderer CreateShellFlyoutRenderer()
        {
            var flyout = base.CreateShellFlyoutRenderer();

            return flyout;
        }

        protected override IShellFlyoutContentRenderer CreateShellFlyoutContentRenderer()
        {
            var theme = Startup.ServiceProvider.GetService<AppTheme>(); /*DependencyService.Get<AppTheme>();*/

            var flyout = base.CreateShellFlyoutContentRenderer();

            GradientDrawable gradient = new GradientDrawable(
                GradientDrawable.Orientation.BottomTop,
                new Int32[] {
                     ((Color)theme.LookupColor("PrimaryMint")).ToAndroid(),
                    ((Color)theme.LookupColor("PrimaryMintLight")).ToAndroid()
                }
            );
            //flyout.AndroidView.SetBackground(gradient);

            var cl = ((CoordinatorLayout)flyout.AndroidView);
            //cl.SetBackgroundColor(Color.PeachPuff.ToAndroid());
            cl.SetBackground(gradient);

            var g = (AppBarLayout)cl.GetChildAt(0);
            g.SetBackgroundColor(Color.Transparent.ToAndroid());
            g.OutlineProvider = null;

            var header = g.GetChildAt(0);
            header.SetBackgroundColor(Color.Transparent.ToAndroid());



            //var appbar = cl.FindViewById<AppBarLayout>(Resource.Id.flyoutcontent_appbar)
            //appBar.GetChildAt(0)
            //header.SetBackgroundColor(Android.Graphics.Color.Transparent);

            //header.SetBackgroundColor(Android.Graphics.Color.Transparent);
            //flyout.AndroidView.FindViewById

            return flyout;
        }

      
    }
}