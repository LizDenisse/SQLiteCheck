using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLitePCL;
using SQLite;
using System.IO;
using System.Xml.Linq;
using System.Data;

namespace SQLiteCheck
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public SQLiteConnection database;
        public MainPage()
        {
            InitializeComponent();
        }

      async  void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "User1.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<PinCodeModel>();
            var item = new PinCodeModel()
            {
                MyData = Myentry.Text
            };

            db.Insert(item);
        }

       async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "User1.db");
            var db = new SQLiteConnection(dbpath);
            if(isTableExist("PinCodeModel")== true)
            {
                var pincodequery = db.Table<PinCodeModel>().Where(a => a.MyData == Myentry.Text).FirstOrDefault();
                if (pincodequery != null)
                {
                    Mylabel.Text = Myentry.Text;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "No Data", "OK");
                }
            }

        }

        private bool isTableExist(string v)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "User1.db");
            var db = new SQLiteConnection(dbpath);

            try
            {
                var tableinfo = db.GetTableInfo(v);

                if (tableinfo.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
            
        }
    }
}
