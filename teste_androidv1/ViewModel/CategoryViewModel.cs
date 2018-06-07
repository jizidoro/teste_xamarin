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
    public class CategoriaViewModel
    {
         
        //"id": 1,
        //"name": "Televisores"
        
        public int Id { get; set; }

        public string Name { get; set; }

    }
}