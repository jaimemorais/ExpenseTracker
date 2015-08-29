using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace App.Droid
{
    [Activity(Label = "Expense Tracker", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            var txtUser = FindViewById<EditText>(Resource.Id.txtUser);
            var btnSearch = FindViewById<EditText>(Resource.Id.btnSearchRepos);
            var lvwRepos = FindViewById<ListView>(Resource.Id.lvwRepos);

            btnSearch.Click += async (object sender, EventArgs e) =>
            {
                var eta = new ExpenseTrackerApi();
                var repos = await eta.GetGitHubReposAsync(txtUser.Text);
                lvwRepos.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemSingleChoice, repos);
            };

        }
    }
}

