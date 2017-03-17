using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppNavTest
{
    public partial class App : Application
    {
        public App()
        {
        
            InitializeComponent();

            PageDetail1 pg = PageDetail1.Create();
            MainPage = new NavigationPage(pg);
            pg.GetVM().LoadMeForFirstTime();

           
        }

       
       

    protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
