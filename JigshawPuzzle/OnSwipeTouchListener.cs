using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Views.GestureDetector;
using static Android.Views.View;

namespace JigshawPuzzle
{
    public class OnSwipeTouchListener : Java.Lang.Object, IOnTouchListener
    {
        public GestureDetector gestureDetector;
        int currentposition;

        public OnSwipeTouchListener(Context context,int currentposition)
        {
            this.currentposition = currentposition;
            gestureDetector = new GestureDetector(context,new GestureListener(currentposition,context));
          
            
        }

        

        public bool OnTouch(View v, MotionEvent e)
        {
           return gestureDetector.OnTouchEvent(e);
        }
    }

    public class GestureListener : SimpleOnGestureListener
    {
        private  const int SWIPE_THRESHOLD = 100;
        private  const int SWIPE_VELOCITY_THRESHOLD = 100;
        private int currentposition;
        private Context context;

        public GestureListener(int currentposition, Context context)
        {
            this.currentposition = currentposition;
            this.context = context;
        }

        public override bool OnDown(MotionEvent e)
        {
            return true;
        }

        public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            bool result= false;
            try
            {
                float diffY = e2.GetY() - e1.GetY();
                float diffX = e2.GetX() - e1.GetX();
                if (Math.Abs(diffX) > Math.Abs(diffY))
                {
                    if (Math.Abs(diffX) > SWIPE_THRESHOLD && Math.Abs(velocityX) > SWIPE_VELOCITY_THRESHOLD)
                    {
                        if (diffX > 0)
                        {
                            OnSwipeRight(context,currentposition,MainActivity.right);
                          
                        }
                        else
                        {
                            OnSwipeLeft(context,currentposition,MainActivity.left);
                        }
                        result=true;
                    }
                }
                else
                {
                    if (Math.Abs(diffY) > SWIPE_THRESHOLD && Math.Abs(velocityY) > SWIPE_VELOCITY_THRESHOLD)
                    {
                        if (diffY > 0)
                        {
                            OnSwipeBottom(context, currentposition, MainActivity.down);
                        }
                        else
                        {
                            OnSwipeTop(context, currentposition, MainActivity.up);
                        }
                        result = true;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return result;
        }

        private void OnSwipeTop(Context context, int currentposition, string up)
        {
            MainActivity.MoveTiles(context, up, currentposition);
        }

        private void OnSwipeBottom(Context context, int currentposition, string down)
        {
            MainActivity.MoveTiles(context,down,currentposition);
        }

        private void OnSwipeLeft(Context context, int currentposition, string left)
        {
           MainActivity.MoveTiles(context,left,currentposition);    
        }

        private void OnSwipeRight(Context context, int currentposition, string right)
        {
            MainActivity.MoveTiles(context, right, currentposition);
        }

        
        
    }
}