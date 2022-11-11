using Android.App;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.ConstraintLayout.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context = Android.Content.Context;
using Random = System.Random;

namespace JigshawPuzzle
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private static int[] puzzleplayer = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        public static GridLayout _gridLayout;
        public static int _currentposition;
        public static bool isSwap;
        public static string up = "Up";
        public static string down = "Down";
        public static string left = "Left";
        public static string right = "Right";
        private static List<ImageView> list = new List<ImageView>();
        private static ImageView imageView;
        private static ImageView _imageView;
        private static TextView _textViewWin;
        private static ConstraintLayout _constraintLayout;
        private static MediaPlayer _mediaPlayer;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.activity_main);
            UIReferences();
            DisplayImage(this);
            GetPosition();
        }

        private void GetPosition()
        {
            

            for (int i = 0; i < _gridLayout.ChildCount; i++)
            {
                imageView = (ImageView)_gridLayout.GetChildAt(i);
                list.Add(imageView);
         
            }
            foreach(var image in list)
            {
                  _currentposition = int.Parse(image.Tag.ToString());
                  image.SetOnTouchListener(new OnSwipeTouchListener(this, _currentposition));
            }
           

        }

       

        public static void MoveTiles(Context context,string direction,int position)
        {
            //1 Picture
           if(position == 0)
           {
                if (direction.Equals(right))
                    SwapPositions(context, position, 1);
                else if (direction.Equals(down))
                    SwapPositions(context, position, 3);
                else
                    Toast.MakeText(context, "Invalid Move", ToastLength.Short).Show();
           }
           //2 Picture
           else if (position == 1)
           {
                if(direction.Equals(right)) 
                    SwapPositions(context, position, 1);
                else if(direction.Equals(left))
                    SwapPositions(context, position, -1);
                else if(direction.Equals(down))
                    SwapPositions(context, position, 3);
                else
                    Toast.MakeText(context, "Invalid Move", ToastLength.Short).Show();
           }
           //3 Picture
           else if(position == 2)
           {
                if (direction.Equals(left))
                    SwapPositions(context, position, -1);
                else if (direction.Equals(down))
                    SwapPositions(context,position,3);
                else
                    Toast.MakeText(context, "Invalid Move", ToastLength.Short).Show();
            }
           //4 Picture
            else if(position == 3)
            {
                if (direction.Equals(up))
                    SwapPositions(context, position, -3);
                else if(direction.Equals(right))
                   SwapPositions(context,position, 1);
                else if(direction.Equals(down))
                    SwapPositions(context,position, 3);
                else
                    Toast.MakeText(context, "Invalid Move", ToastLength.Short).Show();
            }
           //5 Picture
            else if(position == 4)
            {
                if (direction.Equals(up))
                    SwapPositions(context, position, -3);
                else if (direction.Equals(right))
                    SwapPositions(context, position, 1);
                else if (direction.Equals(down))
                    SwapPositions(context, position, 3);
                else if (direction.Equals(left))
                    SwapPositions(context, position, -1);
            }
            //6 Picture
            else if (position == 5)
            {
                if (direction.Equals(up))
                    SwapPositions(context, position, -3);
                else if (direction.Equals(down))
                    SwapPositions(context, position, 3);
                else if (direction.Equals(left))
                    SwapPositions(context, position, -1);
                else
                    Toast.MakeText(context, "Invalid Move", ToastLength.Short).Show();
            }
           //7 Picture
           else if(position == 6)
            {
                if (direction.Equals(up))
                    SwapPositions(context, position, -3);
                else if (direction.Equals(right))
                    SwapPositions(context, position, 1);
                else
                    Toast.MakeText(context, "Invalid Move", ToastLength.Short).Show();
            }
            //8 Picture
            else if (position == 7)
            {
                if (direction.Equals(up))
                    SwapPositions(context, position, -3);
                else if (direction.Equals(right))
                    SwapPositions(context, position, 1);
                else if(direction.Equals(left))
                    SwapPositions(context,position, -1);
                else
                    Toast.MakeText(context, "Invalid Move", ToastLength.Short).Show();
            }
           //9 Picture
            else if (position == 8)
            {
                if (direction.Equals(up))
                    SwapPositions(context, position, -3);
                else if (direction.Equals(left))
                    SwapPositions(context, position, -1);
                else
                    Toast.MakeText(context, "Invalid Move", ToastLength.Short).Show();
            }


        }

        

        public static void SwapPositions(Context context,int currentpostion,int swap)
        {
            isSwap = true;
            int temp = puzzleplayer[currentpostion + swap];
            puzzleplayer[currentpostion + swap] = puzzleplayer[currentpostion];
            puzzleplayer[currentpostion] = temp;
            DisplayImage(context);

            if (IsSolved())
            {
                
                _textViewWin.Visibility = ViewStates.Visible;
                _textViewWin.SetTextColor(Color.White);
                foreach(var image in list)
                {
                    image.Enabled = false;
                }
                _constraintLayout.SetBackgroundResource(Resource.Drawable.goku);
                _mediaPlayer = MediaPlayer.Create(context,Resource.Raw.ultrainstincttheme);
                _mediaPlayer.Start();
                RestartGameAsync(context);
                

            }
          

        }

        private static async Task RestartGameAsync(Context context)
        {
            await Task.Delay(8000);
            isSwap = false;
            _textViewWin.Visibility = ViewStates.Gone;
            _constraintLayout.SetBackgroundColor(Color.White);
            foreach (var image in list)
            {
                image.Enabled = true;
            }
            _mediaPlayer.Stop();
            _mediaPlayer.Release();
            DisplayImage(context);
        }

        private static bool IsSolved()
        {
            bool result = false;
            if (puzzleplayer[0] == 0 && puzzleplayer[1] == 1 && puzzleplayer[2] == 2
                && puzzleplayer[3] == 3 && puzzleplayer[4] == 4 && puzzleplayer[5] == 5
                && puzzleplayer[6] == 6 && puzzleplayer[7] == 7 && puzzleplayer[8] == 8)
            {

                result = true;
            }
            
            else
            { 

                    result = false;
            }


            return result;
        }

        private static  void DisplayImage(Context context)
        {
            if (!isSwap)
            {
                var rng = new Random();
                var keys = puzzleplayer.Select(e => rng.Next()).ToArray();

                Array.Sort(keys, puzzleplayer);
            }
            for (int i = 0; i < puzzleplayer.Length; i++)
            {

                if (puzzleplayer[i] == 0)
                {
                    _gridLayout.GetChildAt(i).SetBackgroundResource(Resource.Drawable.goku1);
                }
                else if (puzzleplayer[i] == 1)
                {
                    _gridLayout.GetChildAt(i).SetBackgroundResource(Resource.Drawable.goku2);
                }
                else if (puzzleplayer[i] == 2)
                {
                    _gridLayout.GetChildAt(i).SetBackgroundResource(Resource.Drawable.goku3);
                }
                else if (puzzleplayer[i] == 3)
                {
                    _gridLayout.GetChildAt(i).SetBackgroundResource(Resource.Drawable.goku4);
                }
                else if (puzzleplayer[i] == 4)
                {
                    _gridLayout.GetChildAt(i).SetBackgroundResource(Resource.Drawable.goku5);
                }
                else if (puzzleplayer[i] == 5)
                {
                    _gridLayout.GetChildAt(i).SetBackgroundResource(Resource.Drawable.goku6);
                }
                else if (puzzleplayer[i] == 6)
                {
                    _gridLayout.GetChildAt(i).SetBackgroundResource(Resource.Drawable.goku7);
                }
                else if (puzzleplayer[i] == 7)
                {
                    _gridLayout.GetChildAt(i).SetBackgroundResource(Resource.Drawable.goku8);
                }
                else if (puzzleplayer[i] == 8)
                {
                    _gridLayout.GetChildAt(i).SetBackgroundResource(Resource.Drawable.goku9);
                }


            }

        }

        private void UIReferences()
        {
            _gridLayout = FindViewById<GridLayout>(Resource.Id.gridLayout);
            _textViewWin = FindViewById<TextView>(Resource.Id.textViewWin);
            _constraintLayout = FindViewById<ConstraintLayout>(Resource.Id.constraintLayout);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


    }
}