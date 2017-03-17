using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppNavTest
{
    public   partial class PopupTransparent : BaseContentPage
    {
       
        
        public PopupTransparent(): base()
        {
            InitializeComponent();
           
        }

      
      
        async void btn_close(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        async void  btn_clicked(object sender, EventArgs e)
        {
            try
            {
              

                App app = Application.Current as App;
                

                PageDetail1 md = (PageDetail1)((Xamarin.Forms.NavigationPage)app.MainPage).CurrentPage.Navigation.NavigationStack[0];
                

                string txt = ((Button)sender).Text;
                if (((Button)sender).Text.Contains("Video"))
                    txt = "Video Home";
                if (((Button)sender).Text.Contains("Products"))
                    txt = "Products";
                if (((Button)sender).Text.Contains("Services"))
                    txt = "Services";
                if (((Button)sender).Text.Contains("Corporate"))
                    txt = "Corporate";
                if (((Button)sender).Text.Contains("Events"))
                    txt = "Events";

           
                TestVM vm = null;

                try
                {
                    vm = md.GetVM();
                }
                catch (Exception ex)
                {
                   //error
                }

                vm.ListHeader = new ListHeader();
                vm.ListPage = new ListPage();
                vm.ListSearch = new ListSearch();

                switch (txt)
                {
                    case "Video Home":
                        vm.SelectedCategory = "Video Home";
                        break;
                    case "Products":
                        vm.SelectedCategory = "Products";
                        vm.ListSearch.Allfields.Add(new FieldValuePair("tag", "products"));
                        break;
                    case "Events":
                        vm.SelectedCategory = "Events";
                        vm.ListSearch.Allfields.Add(new FieldValuePair("tag", "events"));
                        break;
                    case "Services":
                        vm.SelectedCategory = "Services";
                        vm.ListSearch.Allfields.Add(new FieldValuePair("tag", "sss"));
                        break;
                    case "Corporate":
                        vm.SelectedCategory = "Corporate";
                        vm.ListSearch.Allfields.Add(new FieldValuePair("tag", "corporate"));
                        break;
                    default:
                        vm.SelectedCategory = "Video Home";
                        vm.ListSearch.Allfields.Clear();
                        break;
                }
                vm.SearchString = "";
                vm.ClearForSearch();
              

                try
                {
                    await Navigation.PopToRootAsync();
                    
                }
                catch (Exception ee)
                {
                    //swallow this
                }

                await vm.LoadMeForFirstTimeAsync();

            }
            catch (Exception ex)
            {
                string s = "";
            }
        }
    }
}
