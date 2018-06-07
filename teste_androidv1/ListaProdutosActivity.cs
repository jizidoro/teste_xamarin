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
        private List<ProductViewModel> _product;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_lista_produtos);

            _lv = FindViewById<ListView>(Resource.Id.lv);
            await GetProductsAsync();
            _adapter = new MyAdapter(this, Resource.Layout.model, _product);

            _lv.Adapter = _adapter;

            _lv.ItemClick += Lv_ItemClick;
        }

        void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Toast.MakeText(this, _Products[e.Position].Name, ToastLength.Short).Show();
        }

        private async System.Threading.Tasks.Task<List<ProductViewModel>> GetProductsAsync()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bancoteste.db3");
            var db = new SQLiteAsyncConnection(dbPath);
            List<Product> listProduct = await db.Table<Product>().ToListAsync();
            List<Promotion> listPromotion = await db.Table<Promotion>().ToListAsync();
            List<Policies> listPolicies = await db.Table<Policies>().ToListAsync();
            var AllProduct = listProduct;
            var AllPromotion = listPromotion;
            var AllPolicies = listPolicies;

            _product = new List<ProductViewModel>();

            foreach (var item in AllProduct)
            {
                ProductViewModel itemProduct = new ProductViewModel
                {
                    Name = item.Name,
                    PhotoBite = item.Photo,
                    Price = item.Price,
                    Category_id = item.CategoryId,
                    Description = item.Description
                };
                _product.Add(itemProduct);
            }

            return _product;
        }
    }
}

