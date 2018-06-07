using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using teste_androidv1.Model;
using teste_androidv1.ViewModel;

//main

namespace teste_androidv1
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
    {
        List<ProductViewModel> ResultProduct;
        List<PromotionViewModel> ResultPromotion;
        List<CategoriaViewModel> ResultCategory;
        TextView NumeroVersao;
        TextView Titulo;
        TextView NomeJhoni;

        readonly string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bancoteste.db3");

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            Button BotaoTelaMain = FindViewById<Button>(Resource.Id.botao_tela_main);
            BotaoTelaMain.Click += (sender, e) => {
                StartActivity(typeof(ListaProdutosActivity));
            };

            NumeroVersao = FindViewById<TextView>(Resource.Id.numero_versao);
            NumeroVersao.Text = "1.1"; 
            //NumeroVersao.SetWidth(ComplexUnitType.Dip, 100);
            NumeroVersao.SetTextSize(ComplexUnitType.Dip, 30);
            NumeroVersao.SetTextColor(Color.ParseColor("#000fff"));

            Titulo = FindViewById<TextView>(Resource.Id.titulo);
            Titulo.SetTextSize(ComplexUnitType.Dip, 30);
            Titulo.SetTextColor(Color.ParseColor("#000fff"));

            NomeJhoni = FindViewById<TextView>(Resource.Id.nome_do_jhoni);
            NomeJhoni.SetTextSize(ComplexUnitType.Dip, 30);
            NomeJhoni.SetTextColor(Color.ParseColor("#000fff"));

            GetAllData();
        }


        private void GetAllData()
        {
            CreateDatabase();
            GetDataSetCategory();
            Titulo.Text =  AddDataToMyDb();
        }

        private string AddDataToMyDb()
        {
            using (var db = new SQLite.SQLiteConnection(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bancoteste.db3")))
            {
                foreach (var item in ResultCategory)
                {
                    Categoria itemCategory = new Categoria
                    {
                        Id = item.Id,
                        Name = item.Name
                    };
                    db.InsertOrReplace(itemCategory);
                }
                //int i = 0;
                foreach (var item in ResultPromotion)
                {
                    List<Policies> ListPolicies = new List<Policies>();
                    Promotion itemPromotion = new Promotion
                    {
                        //Id = null,
                        Name = item.Name,
                        CategoryId = item.Category_id
                    };
                    foreach (var subItem in item.Policies)
                    {
                        Policies itemPolicies = new Policies
                        {
                            //Id = null,
                            Min = subItem.Min,
                            Discount = subItem.Discount
                        };
                        db.InsertOrReplace(itemPolicies);
                        ListPolicies.Add(itemPolicies);
                    }
                    db.InsertOrReplace(itemPromotion);
                    itemPromotion.PoliciesId = ListPolicies;
                    WriteOperations.UpdateWithChildren(db, itemPromotion);
                    //db.
                }

                foreach (var item in ResultProduct)
                {
                    Product itemProduct = new Product
                    {
                        //Id = null,
                        Name = item.Name,
                        Photo = ConvertPngToJpeg(item.Photo),
                        Price = item.Price,
                        Description = item.Description,
                        CategoryId = item.Category_id
                    };
                    db.InsertOrReplace(itemProduct);
                }
            }
            return "terminou";
        }

        private byte[] ConvertPngToJpeg(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            var options = new BitmapFactory.Options()
            {
                InJustDecodeBounds = false,
                InPurgeable = true,
            };

            int tamanho = (int)(Resources.DisplayMetrics.Density * 100);

            if (imageBitmap != null)
            {
                var sourceSizeHeight = (int)imageBitmap.GetBitmapInfo().Height;
                var sourceSizeWidth = (int)imageBitmap.GetBitmapInfo().Width;
                double scale = (double)tamanho / sourceSizeWidth;

                var height = (int)(scale * sourceSizeHeight);

                using (var bitmapScaled = Bitmap.CreateScaledBitmap(imageBitmap, height, tamanho, true))
                {
                    using (MemoryStream outStream = new MemoryStream())
                    {
                        imageBitmap.Compress(Bitmap.CompressFormat.Jpeg, 50, outStream);
                        byte[] bytes = outStream.ToArray();
                        imageBitmap.Recycle();
                        return bytes;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        private void GetDataSetProduct()
        {
            string url = "https://pastebin.com/raw/eVqp7pfX";
            System.Uri myUri = new System.Uri(url);
            HttpWebRequest myWebRequest = (HttpWebRequest)HttpWebRequest.Create(myUri);

            var myHttpWebRequest = (HttpWebRequest)myWebRequest;
            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Accept = "application/json";

            var myWebResponse = myWebRequest.GetResponse();
            var responseStream = myWebResponse.GetResponseStream();

            var myStreamReader = new StreamReader(responseStream, Encoding.Default);
            var json = myStreamReader.ReadToEnd();

            responseStream.Close();
            myWebResponse.Close();

            ResultProduct = JsonConvert.DeserializeObject<List<ProductViewModel>>(json);

        }


        private void GetDataSetPromotion()
        {
            string url = "https://pastebin.com/raw/R9cJFBtG";
            System.Uri myUri = new System.Uri(url);
            HttpWebRequest myWebRequest = (HttpWebRequest)HttpWebRequest.Create(myUri);

            var myHttpWebRequest = (HttpWebRequest)myWebRequest;
            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Accept = "application/json";

            var myWebResponse = myWebRequest.GetResponse();
            var responseStream = myWebResponse.GetResponseStream();

            var myStreamReader = new StreamReader(responseStream, Encoding.Default);
            var json = myStreamReader.ReadToEnd();

            responseStream.Close();
            myWebResponse.Close();

            ResultPromotion = JsonConvert.DeserializeObject<List<PromotionViewModel>>(json);

            GetDataSetProduct();
        }

        private void GetDataSetCategory()
        {
            string url = "http://pastebin.com/raw/YNR2rsWe";
            System.Uri myUri = new System.Uri(url);
            HttpWebRequest myWebRequest = (HttpWebRequest)HttpWebRequest.Create(myUri);

            var myHttpWebRequest = (HttpWebRequest)myWebRequest;
            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Accept = "application/json";

            var myWebResponse = myWebRequest.GetResponse();
            var responseStream = myWebResponse.GetResponseStream();

            var myStreamReader = new StreamReader(responseStream, Encoding.Default);
            var json = myStreamReader.ReadToEnd();

            responseStream.Close();
            myWebResponse.Close();

            ResultCategory = JsonConvert.DeserializeObject<List<CategoriaViewModel>>(json);

            GetDataSetPromotion();
        }
        
        private string CreateDatabase()
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bancoteste.db3")))
                {
                    connection.CreateTable<Categoria>(SQLite.CreateFlags.ImplicitPK | SQLite.CreateFlags.AutoIncPK);
                    connection.CreateTable<Policies>(SQLite.CreateFlags.ImplicitPK | SQLite.CreateFlags.AutoIncPK);
                    connection.CreateTable<Product>(SQLite.CreateFlags.ImplicitPK | SQLite.CreateFlags.AutoIncPK);
                    connection.CreateTable<Promotion>(SQLite.CreateFlags.ImplicitPK | SQLite.CreateFlags.AutoIncPK);
                    connection.CreateTable<Sessao>(SQLite.CreateFlags.ImplicitPK | SQLite.CreateFlags.AutoIncPK);

                    connection.Execute("DELETE FROM Categoria");
                    connection.Execute("DELETE FROM Policies");
                    connection.Execute("DELETE FROM Product");
                    connection.Execute("DELETE FROM Promotion");
                    connection.Execute("DELETE FROM Sessao");
                }

                return "Database created";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

    }
}

