using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using teste_androidv1.Model;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using SQLite;
using teste_androidv1.ViewModel;

namespace teste_androidv1
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
	public class ListaProdutosActivity : AppCompatActivity
    {

        private MyAdapter _adapter;
        private ListView _lv;
        private List<Player> _players;
        private List<ProductViewModel> _product;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_lista_produtos);

            _lv = FindViewById<ListView>(Resource.Id.lv);
            await GetPlayersAsync();
            _adapter = new MyAdapter(this, Resource.Layout.model, _product);

            _lv.Adapter = _adapter;

            _lv.ItemClick += Lv_ItemClick;
        }

        void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, _players[e.Position].Name, ToastLength.Short).Show();
        }

        private async System.Threading.Tasks.Task<List<Player>> GetPlayersAsync()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bancoteste.db3");
            var db = new SQLiteAsyncConnection(dbPath);
            List<Product> list = await db.Table<Product>().ToListAsync();
            var AllProduct = list;

            _product = new List<ProductViewModel>();

            foreach (var item in AllProduct)
            {
                ProductViewModel itemProduct = new ProductViewModel();
                //itemProduct. = item.Id;
                itemProduct.name = item.Name;
                itemProduct.photoBite = item.Photo;
                itemProduct.price = item.Price;
                itemProduct.category_id = item.CategoryId;
                itemProduct.description = item.Description;
                _product.Add(itemProduct);
            }


            _players = new List<Player>()
            {
                new Player("David De Gea",Resource.Mipmap.ic_launcher),
                new Player("Juan Mata",Resource.Mipmap.ic_launcher),
                new Player("Ander Herera",Resource.Mipmap.ic_launcher),
                new Player("Eden Hazard",Resource.Mipmap.ic_launcher),
                new Player("John Terry",Resource.Mipmap.ic_launcher),
                new Player("Michael Carrick",Resource.Mipmap.ic_launcher)
            };

            return _players;
        }
    }
}

