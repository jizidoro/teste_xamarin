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
        
        /*
        [PrimaryKey]
        public int? id { get; set; }

        public string name { get; set; }

        public int CategoryId { get; set; }

        public int PoliciesId { get; set; }
        
        public override string ToString() => string.Format("[Promotion:id={0}, name={1}, policiesId={2}, categoryId={3}]", id, name, policiesId, categoryId);
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

        private int _CategoryId;
        [Column("CategoryId")]
        public int CategoryId
        {
            get { return _CategoryId; }
            set
            {
                if (_CategoryId != value)
                {
                    _CategoryId = value;
                }
            }
        }

        private int _PoliciesId;
        [Column("PoliciesId")]
        public int PoliciesId
        {
            get { return _PoliciesId; }
            set
            {
                if (_PoliciesId != value)
                {
                    _PoliciesId = value;
                }
            }
        }
    }
}