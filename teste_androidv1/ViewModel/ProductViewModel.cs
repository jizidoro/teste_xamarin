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

namespace teste_androidv1.ViewModel
{
    public class ProductViewModel
    {
        //"id": 3,
        //"name": "65\" UHD 4K Flat Smart TV JU6000 Series 6",
        //"description": "Sua Smart TV vai possibilitar você baixar e acessar aplicativos e assistir conteúdo de vídeos da internet em uma tela muito melhor. Desfrute do Netflix e You Tube com toda a qualidade de imagem da sua TV.",
        //"photo": "https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/fd08d6e6-48f7-11e6-886b-0a37f4bea89f.jpeg",
        //"price": 10999.00,
        //"category_id": 1
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public byte[] PhotoBite { get; set; }

        public double Price { get; set; }

        public int? Category_id { get; set; }

    }
}