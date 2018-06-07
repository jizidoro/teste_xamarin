﻿using System;
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

namespace teste_androidv1.Model
{
    class Promotion
    {

        //"name": "Promoção Oi Me Liga",
        //"category_id": 2,
        //"policies": [
        //    {
        //        "min": 1,
        //        "discount": 10.0
        //    }
        //]

        [Column("Id"), PrimaryKey, NotNull, AutoIncrement]
        public int? Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Column("PoliciesId")]
        public int PoliciesId { get; set; }
        
        //[OneToMany]
        //public List<Policies> PoliciesId { get; set; }

    }
}