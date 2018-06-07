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
using SQLite;

namespace teste_androidv1.ViewModel
{
    public class PoliciesViewModel
    {
        
        //"min": 2,
        //"discount": 10.0
        
        public int Min { get; set; }

        public double Discount { get; set; }

    }
}