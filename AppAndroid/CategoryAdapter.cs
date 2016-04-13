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
using AppAndroid.DTO;

namespace AppAndroid
{
    public class CategoryAdapter : BaseAdapter<String>
    {
        List<CategoryDTO> CategoryList;
        Activity CurrActivity;


        public CategoryAdapter(List<CategoryDTO> itemList, Activity c)
        {
            CategoryList = itemList;
            CurrActivity = c;
        }

        public override string this[int position]
        {
            get
            {
                return CategoryList[position].Id;
            }
        }

        public override int Count
        {
            get
            {
                return CategoryList.Count;
            }
        }        

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = CurrActivity.LayoutInflater.Inflate(Resource.Layout.CategoryCell, null);
            }

            view.FindViewById<TextView>(Resource.Id.textViewCategory).Text = CategoryList[position].Name;

            return view;
        }
    
    }
}