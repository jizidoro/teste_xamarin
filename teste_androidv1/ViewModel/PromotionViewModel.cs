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

namespace teste_androidv1.ViewModel
{
    public class PromotionViewModel
    {
        
        //"name": "Promoção Oi Me Liga",
        //"category_id": 2,
        //"policies": [
        //    {
        //        "min": 1,
        //        "discount": 10.0
        //    }
        //]
        
        public string name { get; set; }

        public int category_id { get; set; }

        public List<PoliciesViewModel> Policies { get; set; }

    }
}