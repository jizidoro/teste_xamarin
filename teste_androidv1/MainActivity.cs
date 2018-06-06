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

        string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bancoteste.db3");

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            GetAllData();

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

        }


        private void GetAllData()
        {
            CreateDatabase();
            GetDataSetCategory();
            AddDataToMyDbAsync();
        }

        private async System.Threading.Tasks.Task AddDataToMyDbAsync()
        {
            var db = new SQLiteAsyncConnection(dbPath);

            foreach (var item in ResultCategory)
            {
                Categoria itemCategory = new Categoria();
                itemCategory.Id = item.id;
                itemCategory.Name = item.name;
                await db.InsertOrReplaceAsync(itemCategory);
            }
            //int i = 0;
            foreach (var item in ResultPromotion)
            {
                Promotion itemPromotion = new Promotion();
                //itemPromotion.id = item.Id;
                itemPromotion.Name = item.name;
                itemPromotion.CategoryId = item.category_id;
                foreach (var subItem in item.Policies)
                {
                    Policies itemPolicies = new Policies();
                    itemPolicies.Min = subItem.min;
                    itemPolicies.Discount = subItem.discount;
                    await db.InsertOrReplaceAsync(itemPolicies);
                    itemPromotion.PoliciesId = (int)itemPolicies.Id;
                }
                await db.InsertOrReplaceAsync(itemPromotion);
            }

            foreach (var item in ResultProduct)
            {
                Product itemProduct = new Product();
                //itemProduct.id = item.Id;
                itemProduct.Name = item.name;
                itemProduct.Photo = ConvertPngToJpeg(item.photo);
                itemProduct.Price = item.price;
                itemProduct.Description = item.description;
                itemProduct.CategoryId = item.category_id;
                await db.InsertOrReplaceAsync(itemProduct);
            }

            /*
            var dadosToken = db.Table<Product>();
            var TokenAtual = await dadosToken.FirstOrDefaultAsync();
            NomeJhoni.Text = TokenAtual.Name;
            */
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

            //Create memory stream
            using (MemoryStream outStream = new MemoryStream())
            {
                //Save the image as Jpeg
                imageBitmap.Compress(Bitmap.CompressFormat.Jpeg, 50, outStream);
                byte[] bytes = outStream.ToArray();
                return bytes;
                //string base64String = Convert.ToBase64String(bytes);
                //return base64String;
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
                var connection = new SQLiteConnection(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bancoteste.db3"));
                connection.CreateTable<Categoria>();
                connection.CreateTable<Policies>();
                connection.CreateTable<Product>();
                connection.CreateTable<Promotion>();
                connection.CreateTable<Sessao>();
                connection.CreateTable<WordT>();

                connection.Execute("DELETE FROM Categoria");
                connection.Execute("DELETE FROM Policies");
                connection.Execute("DELETE FROM Product");
                connection.Execute("DELETE FROM Promotion");
                connection.Execute("DELETE FROM Sessao");
                connection.Execute("DELETE FROM WordT");

                connection.Close();
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

