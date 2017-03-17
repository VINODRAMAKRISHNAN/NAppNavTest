using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppNavTest
{
    public partial class PagePlaylistVideos : BaseContentPage
    {
      
        PlaylistVideoVM vm = null;
        public PagePlaylistVideos() : base()
        {
            InitializeComponent();
        }

        public async Task<bool> SetViewModel()
        {
            vm = new PlaylistVideoVM();
            this.id1mainpagecontent.BindingContext = vm;
          
            return true;
        }
        public async Task<bool> LoadData(ObservableCollection<MVideo> vdos)
        {
          
            if (vdos != null && vdos.Count > 0)
            {
                vm.MVideoList = new ObservableCollection<MVideo>(vdos);
                vm.ListPage.TotalRecords = vm.MVideoList.Count;
                vm.ListPage.CurPulledRecords = vm.MVideoList.Count;
            }
           
            return true;
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
            await Navigation.PopToRootAsync();
        }

        public PlaylistVideoVM GetVM()
        {
            return vm;
        }
    }
}

