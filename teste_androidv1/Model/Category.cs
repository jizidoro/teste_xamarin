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
    class Categoria
    {
         
        //"id": 1,
        //"name": "Televisores"
        
        /*
        [PrimaryKey]
        public int? id { get; set; }
        public string name { get; set; }
        public override string ToString() => string.Format("[Categoria:id={0}, name={1}]", id, name);
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

        private string _Name;
        [Column("Name")]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                }
            }
        }

    }
}