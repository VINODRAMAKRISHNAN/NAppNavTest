using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppNavTest
{
    public partial class PagePlaylist :  BaseContentPage
    {
       
        PlayListVM vm = null;
        public PagePlaylist() : base()
        {
            InitializeComponent();
        }

        public async Task<bool> SetViewModel( long vid)
        {

            vm = new PlayListVM();
            vm.SelectedVideoId = vid;

            this.id1mainpagecontent.BindingContext = vm;

            await vm.LoadMeForFirstTimeNew();
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

            MPlaylist item = e.Item as MPlaylist;

          

            var plistvideo = new PagePlaylistVideos();
           
           
            await plistvideo.SetViewModel();
            plistvideo.GetVM().IsLoading = true;
           await plistvideo.LoadData(item.Videos);
            plistvideo.GetVM().IsLoading = false;
            plistvideo.GetVM().SelectedPlaylist = item.DisplayName;

            await Navigation.PushAsync(plistvideo);
        }
    }
}
