using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppNavTest
{
    
    public partial class PageFeatured : BaseContentPage
    {
      

        FeaturedVM vm = null;
        public PageFeatured() : base()
        {
            InitializeComponent();
        }

        public FeaturedVM GetVM()
        {
            return vm;
        }



      

        public async Task<bool> SetViewModel(TestVM pvm)
        {
            
            vm = new FeaturedVM();
           
            vm.SelectedCategory = pvm.SelectedCategory;
            this.id1mainpagecontent.BindingContext = vm;
            vm.ListSearch.Allfields.Clear();
            if (vm.SelectedCategory != "Video Home")
            {
                vm.ListSearch.Allfields.Add(new AppNavTest.FieldValuePair("tag", vm.SelectedCategory));
            }
          
            
            await vm.LoadMeForFirstTime();

            return true;
        }

        async void btnbacktest_clicked(object sender, EventArgs e)
        {
            PageDetail1 prepage =this.Navigation.NavigationStack[0] as PageDetail1;

            prepage.GetVM().ClearForSearch();
            await prepage.GetVM().LoadMeForFirstTimeAsync();

            await Navigation.PopAsync();
        }

        async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if ((e != null) && (e.Item != null))
            {
                ListView lv = sender as ListView;

                if (lv != null)
                    lv.SelectedItem = null;
            }

            App app = Application.Current as App;
            PageDetail1 md = (PageDetail1)((Xamarin.Forms.NavigationPage)app.MainPage).CurrentPage.Navigation.NavigationStack[0];

            MVideo item = e.Item as MVideo;

            if (item != null && item.ListHeader != null)
            {
                md.GetVM().ListHeader = item.ListHeader;
            }


            await md.ResetPage();
            await Navigation.PopAsync();
           
            
        }

    }


}
