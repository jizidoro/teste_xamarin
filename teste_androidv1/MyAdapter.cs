using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Drawing;

using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using teste_androidv1.Model;
using teste_androidv1.ViewModel;
using System.IO;

namespace teste_androidv1
{
    class MyAdapter : ArrayAdapter
    {
        private Context c;
        private readonly List<Player> players;
        private readonly List<ProductViewModel> products;
        private LayoutInflater inflater;
        private readonly int resource;

        public MyAdapter(Context context, int resource, List<ProductViewModel> products) : base(context, resource, products)
        {
            this.c = context;
            this.resource = resource;
            this.products = products;
        }
        /*
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (inflater == null)
            {
                inflater = (LayoutInflater)c.GetSystemService(Context.LayoutInflaterService);
            }

            if (convertView == null)
            {
                convertView = inflater.Inflate(resource, parent, false);
            }

            MyHolder holder = new MyHolder(convertView)
            {
                NameTxt = { Text = players[position].Name }

            };
            holder.Img.SetImageResource(players[position].Image);

            convertView.SetBackgroundColor(Color.LightGreen);

            if (position % 2 == 0)
            {
                //convertView.SetBackgroundColor(Color.LightGreen);
            }

            return convertView;
        }
        */
        private class MyViewHolder : Java.Lang.Object
        {
            public TextView Name { get; set; }
            public TextView Description { get; set; }
            public ImageView Image { get; set; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            MyViewHolder holder = null;
            var view = convertView;

            if (view != null)
                holder = view.Tag as MyViewHolder;


            if (holder == null)
            {
                holder = new MyViewHolder();

                if (inflater == null)
                {
                    inflater = (LayoutInflater)c.GetSystemService(Context.LayoutInflaterService);
                }

                if (convertView == null)
                {
                    convertView = inflater.Inflate(resource, parent, false);
                }

                view = convertView;
                holder.Name = view.FindViewById<TextView>(Resource.Id.nameTxt);
                //holder.Description = view.FindViewById<TextView>(Resource.Id.textView2);
                holder.Image = view.FindViewById<ImageView>(Resource.Id.playerImg);
                view.Tag = holder;
                //view.SetBackgroundColor(Color.LightGreen);
            }


            holder.Name.Text = products[position].name;
            //holder.Description.Text = Descriptions[position];
            var imageBitmap = BitmapFactory.DecodeByteArray(products[position].photoBite, 0, products[position].photoBite.Length);

            holder.Image.SetImageBitmap(imageBitmap);

            return view;
        }


        
    }
}