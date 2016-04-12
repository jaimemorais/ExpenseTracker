using System;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppAndroid.DTO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace AppAndroid
{
    [Activity(Label = "AppAndroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : BaseExpenseTrackerActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            

            Button btnListCategory = FindViewById<Button>(Resource.Id.BtnListCategory);
            btnListCategory.Click += BtnListCategoryClick;

        }

        void BtnListCategoryClick(object sender, EventArgs e)
        {
            ListView listViewCategory = FindViewById<ListView>(Resource.Id.ListViewCategory);
            
            List<Category> categories = GetCategoryList("CategoryApi");

            CategoryAdapter ca = new CategoryAdapter(categories, this);
            listViewCategory.Adapter = ca;

        }


        protected List<Category> GetCategoryList(string api)
        {
            try
            {
                string url = this.GetApiServiceURL(api);
                JArray jArray = GetJsonData(url);


                List<Category> categoryList = jArray.Select(c => new Category
                {
                    Id = (string)c["Id"],
                    Name = (string)c["Name"]
                }).ToList();

                return categoryList;
            }
            catch (Exception e)
            {
                string err = "Error : " + e.Message;
                Toast.MakeText(this, "Error getting list.", ToastLength.Short).Show();
                return null;
            }
        }

    }
    
}

