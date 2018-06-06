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
    class Product
    {
        //"id": 3,
        //"name": "65\" UHD 4K Flat Smart TV JU6000 Series 6",
        //"description": "Sua Smart TV vai possibilitar você baixar e acessar aplicativos e assistir conteúdo de vídeos da internet em uma tela muito melhor. Desfrute do Netflix e You Tube com toda a qualidade de imagem da sua TV.",
        //"photo": "https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/fd08d6e6-48f7-11e6-886b-0a37f4bea89f.jpeg",
        //"price": 10999.00,
        //"category_id": 1


        /*
        [PrimaryKey]
        public int? id { get; set; }

        public string name { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public double Price { get; set; }

        public int? categoryId { get; set; }

        public override string ToString() => string.Format("[Product:id={0}, name={1}, description={2}, photo={3}, price={4}, categoryId={5}]", id, name, description, photo, price , categoryId);
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

        private string _Description;
        [Column("Description")]
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                }
            }
        }


        private byte[] _Photo;
        [Column("Photo")]
        public byte[] Photo
        {
            get { return _Photo; }
            set
            {
                if (_Photo != value)
                {
                    _Photo = value;
                }
            }
        }

        private double _Price;
        [Column("Price")]
        public double Price
        {
            get { return _Price; }
            set
            {
                if (_Price != value)
                {
                    _Price = value;
                }
            }
        }

        private int? _CategoryId;
        [Column("CategoryId")]
        public int? CategoryId
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

    }
}