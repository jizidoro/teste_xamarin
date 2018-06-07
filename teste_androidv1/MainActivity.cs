using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using Newtonsoft.Json;
using SQLite;
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

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            

            Button BotaoTelaMain = FindViewById<Button>(Resource.Id.botao_tela_main);
            BotaoTelaMain.Click += (sender, e) => {
                StartActivity(typeof(ListaProdutosActivity));
            };

            NumeroVersao = FindViewById<TextView>(Resource.Id.numero_versao);
            NumeroVersao.Text = "1.0";
            NumeroVersao.SetTextSize(ComplexUnitType.Dip, 30);
            NumeroVersao.SetTextColor(Color.ParseColor("#000fff"));

            Titulo = FindViewById<TextView>(Resource.Id.titulo);
            Titulo.SetTextSize(ComplexUnitType.Dip, 30);
            Titulo.SetTextColor(Color.ParseColor("#000fff"));

            NomeJhoni = FindViewById<TextView>(Resource.Id.nome_do_jhoni);
            NomeJhoni.SetTextSize(ComplexUnitType.Dip, 30);
            NomeJhoni.SetTextColor(Color.ParseColor("#000fff"));


            Titulo.Text = await GetAllData();
        }


        private async System.Threading.Tasks.Task<string> GetAllData()
        {
            CreateDatabase();
            GetDataSetCategory();
            NomeJhoni.Text = await AddDataToMyDbAsync();

            return "terminou";
        }

        private async System.Threading.Tasks.Task<string> AddDataToMyDbAsync()
        {
            var db = new SQLiteAsyncConnection(dbPath);

            foreach (var item in ResultCategory)
            {
                Categoria itemCategory = new Categoria
                {
                    Id = item.id,
                    Name = item.name
                };
                await db.InsertOrReplaceAsync(itemCategory);
            }
            //int i = 0;
            foreach (var item in ResultPromotion)
            {
                Promotion itemPromotion = new Promotion
                {
                    //Id = null,
                    Name = item.name,
                    CategoryId = item.category_id
                };
                foreach (var subItem in item.Policies)
                {
                    Policies itemPolicies = new Policies
                    {
                        //Id = null,
                        Min = subItem.min,
                        Discount = subItem.discount
                    };
                    await db.InsertOrReplaceAsync(itemPolicies);
                    //itemPromotion.PoliciesId = (int)itemPolicies.Id;
                }
                await db.InsertOrReplaceAsync(itemPromotion);
            }

            foreach (var item in ResultProduct)
            {
                Product itemProduct = new Product
                {
                    //Id = null,
                    Name = item.name,
                    Photo = ConvertPngToJpeg(item.photo),
                    Price = item.price,
                    Description = item.description,
                    CategoryId = item.category_id
                };
                await db.InsertOrReplaceAsync(itemProduct);
            }

            return "terminou";

            /*
            var dadosToken = db.Table<Product>();
            var TokenAtual = await dadosToken.FirstOrDefaultAsync();
            NomeJhoni.Text = TokenAtual.Name;
            */
        }

        private byte[] ConvertPngToJpeg(string url)
        {
            Bitmap imageBitmap = null;
            //Bitmap scaledimageBitmap = null;


            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    //scaledimageBitmap = Bitmap.CreateScaledBitmap(imageBitmap, 100, 100, true);
                }
            }

            var options = new BitmapFactory.Options()
            {
                InJustDecodeBounds = false,
                InPurgeable = true,
            };


            if (imageBitmap != null)
            {
                var sourceSizeHeight = (int)imageBitmap.GetBitmapInfo().Height;
                var sourceSizeWidth = (int)imageBitmap.GetBitmapInfo().Width;
                double scale = (double)NumeroVersao.Width / sourceSizeWidth;

                //var width = (int)(scale * sourceSizeWidth);
                var height = (int)(scale * sourceSizeHeight);

                using (var bitmapScaled = Bitmap.CreateScaledBitmap(imageBitmap, height, NumeroVersao.Width , true))
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
                //var connection = new SQLiteConnection(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bancoteste.db3"));

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





                //connection.Close();
                //Toast.MakeText(this, "Database created", ToastLength.Short).Show();
                return "Database created";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

    }
}

