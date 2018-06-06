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
    [Table("WordT")]
    public class WordT
    {

        private int _WId;
        [Column("WId")]
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int WId
        {
            get { return _WId; }
            set
            {
                if (_WId != value)
                {
                    _WId = value;
                }
            }
        }

        private string _Word;

        [Column("Word")]
        public string Word
        {
            get { return _Word; }
            set
            {
                if (_Word != value)
                {
                    _Word = value;
                }
            }
        }
    }
}