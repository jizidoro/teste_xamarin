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
    class Policies
    {
        
        //"min": 2,
        //"discount": 10.0
        /*
        [PrimaryKey]
        public int id { get; set; }

        public string Min { get; set; }

        public string Discount { get; set; }

        public override string ToString() => string.Format("[Policies:id={0}, min={1}, discount={2}]", id, min, discount);
        */

        private int _Id;
        [Column("Id")]
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                }
            }
        }

        private int _Min;
        [Column("Min")]
        public int Min
        {
            get { return _Min; }
            set
            {
                if (_Min != value)
                {
                    _Min = value;
                }
            }
        }

        private double _Discount;
        [Column("Discount")]
        public double Discount
        {
            get { return _Discount; }
            set
            {
                if (_Discount != value)
                {
                    _Discount = value;
                }
            }
        }
    }
}