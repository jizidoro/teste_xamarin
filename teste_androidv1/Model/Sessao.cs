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
    class Sessao
    {

        [Column("Id"), PrimaryKey, NotNull, AutoIncrement]
        public int? Id { get; set; }

        [Column("Total")]
        public double Total { get; set; }
    }
}