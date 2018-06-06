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

namespace teste_androidv1.Model
{
    class MyHolder
    {
        public TextView NameTxt;
        public ImageView Img;

        public MyHolder(View v)
        {
            this.NameTxt = v.FindViewById<TextView>(Resource.Id.nameTxt);
            this.Img = v.FindViewById<ImageView>(Resource.Id.playerImg);
        }
    }
}