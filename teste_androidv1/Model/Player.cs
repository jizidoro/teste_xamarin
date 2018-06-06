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
using SQLite.Net.Attributes;

namespace teste_androidv1.Model
{
    class Player
    {
        public Player(string name, int image)
        {
            this.Name = name;
            this.Image = image;
        }

        public string Name { get; }

        public int Image { get; }

    }
}