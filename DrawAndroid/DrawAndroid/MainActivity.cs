using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using System;
using System.Runtime.Remoting.Contexts;
using Android.Graphics;

namespace DrawAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        
        SeekBar _seek;
        Button _circleBtn;
        Button _rectBtn;
        Button _barBtn;
        MyCustumView _myCustumView;
 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _myCustumView = new MyCustumView(this);
            LinearLayout linear = FindViewById<LinearLayout>(Resource.Id.myView);
            linear.AddView(_myCustumView);
            _myCustumView.Invalidate();

            _seek = FindViewById<SeekBar>(Resource.Id.seekBar);
            _circleBtn = FindViewById<Button>(Resource.Id.circleButton);
            _rectBtn = FindViewById<Button>(Resource.Id.rectButton);
            _barBtn = FindViewById<Button>(Resource.Id.barButton);

            _seek.ProgressChanged += OnProgressChanged;
            _circleBtn.Click += circleButtonClick;
            _rectBtn.Click += rectButtonClick;
            _barBtn.Click += barButtonClick;
        }

        public void circleButtonClick(object sender, EventArgs e)
        {
            _myCustumView.Variant = Figure.Circle;
        }

        public void rectButtonClick(object sender, EventArgs e)
        {
            _myCustumView.Variant = Figure.Rectangle;
        }

        public void barButtonClick(object sender, EventArgs e)
        {
            _myCustumView.Variant = Figure.Bar;
        }

        public void OnProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            _myCustumView.Progress = e.Progress;
        }
    }
}