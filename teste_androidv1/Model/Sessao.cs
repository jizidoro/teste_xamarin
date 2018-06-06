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
        /*
        [PrimaryKey]
        public int? id { get; set; }

        public string Total { get; set; }
        
        public override string ToString() => string.Format("[Sessao:id={0}, total={1}]", id, total);
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

        private double _Total;
        [Column("Total")]
        public double Total
        {
            get { return _Total; }
            set
            {
                if (_Total != value)
                {
                    _Total = value;
                }
            }
        }
    }
}