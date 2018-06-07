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
                NameTxt = { Text = Products[position].Name }

            };
            holder.Img.SetImageResource(Products[position].Image);

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
            public TextView Promo { get; set; }
            public TextView Price { get; set; }
            public TextView Qtd { get; set; }

            public Button BtnPlus { get; set; }
            public Button BtnLess { get; set; }
            public Button BtnFavorite { get; set; }

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

                holder.Image = view.FindViewById<ImageView>(Resource.Id.imgProduct);

                holder.Name = view.FindViewById<TextView>(Resource.Id.txtName);
                holder.Promo = view.FindViewById<TextView>(Resource.Id.txtPromo);
                holder.Price = view.FindViewById<TextView>(Resource.Id.txtPrice);
                holder.Qtd = view.FindViewById<TextView>(Resource.Id.txtQtd);

                holder.BtnPlus = view.FindViewById<Button>(Resource.Id.btnPlus);
                holder.BtnLess = view.FindViewById<Button>(Resource.Id.btnLess);
                holder.BtnFavorite = view.FindViewById<Button>(Resource.Id.btnFavorite);

                //holder.Description = view.FindViewById<TextView>(Resource.Id.textView2);

                view.Tag = holder;
                //view.SetBackgroundColor(Color.LightGreen);
            }


            holder.Name.Text = products[position].Name;
            holder.Promo.Text = products[position].Price.ToString();
            holder.Price.Text = products[position].Price.ToString();
            //holder.Qtd.Text = products[position].name;
            holder.Qtd.Text = "0";

            //holder.Description.Text = Descriptions[position];
            var imageBitmap = BitmapFactory.DecodeByteArray(products[position].PhotoBite, 0, products[position].PhotoBite.Length);

            holder.Image.SetImageBitmap(imageBitmap);

            return view;
        }


        
    }
}