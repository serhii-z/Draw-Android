using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;

namespace DrawAndroid
{
    public enum Figure
    {
        Circle = 1,
        Rectangle,
        Bar
    }
    public class MyCustumView : View
    {
        private Paint _paint;
        private Paint _paint1;
        private Paint _paint2;
        private Paint _paint3;
        private int _newHeight;

        private int _progress;
        public int Progress 
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                Invalidate();
            } 
        }

        private Figure _variant;
        public Figure Variant
        {
            get
            {
                return _variant;
            }
            set
            {
                _variant = value;
                Invalidate();
            }
        }

        #region constructors
        public MyCustumView(Context context) : base(context)
        {
                  
        }

        public MyCustumView(Context context, IAttributeSet attrs) : 
            base(context, attrs)
        {
        }

        public MyCustumView(Context context, IAttributeSet attrs, int defStyleAttr) : 
            base(context, attrs, defStyleAttr)
        {
        }

        public MyCustumView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : 
            base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected MyCustumView(IntPtr javaReference, JniHandleOwnership transfer) : 
            base(javaReference, transfer)
        {
        }
        #endregion

        protected override void DispatchDraw(Canvas canvas)
        {
            base.DispatchDraw(canvas);
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            canvas.DrawColor(Android.Graphics.Color.WhiteSmoke);
            _paint = new Paint();
            _paint1 = new Paint();
            _paint2 = new Paint();
            _paint3 = new Paint();

            _paint.Color = Android.Graphics.Color.Blue;
            _paint1.Color = Android.Graphics.Color.CadetBlue;
            _paint2.Color = Android.Graphics.Color.WhiteSmoke;
            _paint3.Color = Android.Graphics.Color.DarkBlue;
            
            _paint3.StrokeWidth = 20;

            _paint.SetStyle(Paint.Style.Fill);
            _paint1.SetStyle(Paint.Style.Fill);
            _paint2.SetStyle(Paint.Style.Fill);
            _paint3.SetStyle(Paint.Style.Stroke);
            ChooseVariant(canvas);          
        }

        private void ChooseVariant(Canvas canvas)
        {
            switch (Variant)
            {
                case Figure.Circle:
                    DrawCircle(canvas);
                    break;
                case Figure.Rectangle:
                    DrawRectangle(canvas);
                    break;
                case Figure.Bar:
                    DrawBar(canvas);
                    break;
            }
        }

        private void DrawRectangle(Canvas canvas)
        {
            RectF myRect = new RectF();
            _newHeight = canvas.Height - Progress * 10;         
            myRect.Set(canvas.Width/3f, _newHeight , canvas.Width/1.5f, canvas.Height);
            if(Progress > 0)
            {
                canvas.DrawRect(myRect, _paint1);
                canvas.DrawRect(myRect, _paint3);
            }
            ShowText(canvas);          
        }

        private void DrawCircle(Canvas canvas)
        {
            float radius = 400f;
            Path path = new Path();
            path.AddCircle(canvas.Width, canvas.Height, radius, Path.Direction.Cw);
            
            float center_x, center_y;
            center_x = canvas.Width / 2;
            center_y = canvas.Height / 2;

            RectF oval = new RectF();

            // большой круг
           
            oval.Set(center_x - 500f, center_y - 500f, center_x + 500f, center_y + 500f);
            canvas.DrawArc(oval, 270, Progress * 3.6f, true, _paint1);

            // маленький круг
            oval.Set(center_x - radius, center_y - radius, center_x + radius, center_y + radius);
            canvas.DrawArc(oval, 270, Progress * 3.6f, true, _paint2);

            ShowText(canvas);
        }

        private void DrawBar(Canvas canvas)
        {
            int countRect = Progress/10;
            
            int heightRect = 0;          
            int i = 0;         
            while (i < countRect)
            {
                RectF rect = new RectF();
                rect.Set(canvas.Width / 3f, (canvas.Height * 0.945f) - heightRect, canvas.Width / 1.5f, 
                    canvas.Height - heightRect);
                canvas.DrawRoundRect(rect, 25, 25, _paint1);
                rect.Set(canvas.Width / 3f, (canvas.Height * 0.945f) - heightRect, canvas.Width / 1.5f, 
                    canvas.Height - heightRect);
                canvas.DrawRoundRect(rect, 25, 25, _paint3);
                heightRect += 115;
                i++;
            }
            _newHeight = canvas.Height - heightRect;
            ShowText(canvas);          
        }

        private void ShowText(Canvas canvas)
        {
            _paint.TextSize = 100;
            _paint.TextAlign = Paint.Align.Center;
            string percent = Progress.ToString() + "%";
            if (Variant == Figure.Circle)
            {
                canvas.DrawText(percent, canvas.Width / 2, canvas.Height / 2, _paint);
            }
            else
            {
                canvas.DrawText(percent, canvas.Width / 2, _newHeight - 30, _paint);
            }
        }
    }
}