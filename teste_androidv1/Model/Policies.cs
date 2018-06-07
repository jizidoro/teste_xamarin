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
using SQLiteNetExtensions.Attributes;
using teste_androidv1.Model;

namespace teste_androidv1.ViewModel
{
    class Policies
    {

        //"min": 2,
        //"discount": 10.0


        [Column("Id"), PrimaryKey, NotNull, AutoIncrement]
        public int? Id { get; set; }

        [Column("Min")]
        public int Min { get; set; }

        [Column("Discount")]
        public double Discount { get; set; }

        [ForeignKey(typeof(Promotion))]
        public int PoliciesId { get; set; }
    }
}