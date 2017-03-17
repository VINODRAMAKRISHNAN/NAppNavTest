
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppNavTest
{
    public partial class PageDetail1 : BaseContentPage
    {
        TestVM vm = null;

       
        public static PageDetail1 Create()
        {
           
            var pg = new PageDetail1();
            //bool ret = await pg.GetVM().LoadMeForFirstTime();
            return pg;
        }

        public PageDetail1() : base()
        {
            InitializeComponent();

            vm = new TestVM();
            this.id1mainpagecontent.BindingContext = vm;
            
        }

        public void RestVMPageDetail1(TestVM tvm) 
        {
            //vm.MVideoList.Clear();
           // vm.MVideoList = new ObservableCollection<MVideo>();
          
            this.id1mainpagecontent.BindingContext = tvm;
        }

        public void ResetVM()
        {
            vm = new TestVM();
        }

        public TestVM GetVM()
        {
            return vm;
        }

        async void btnFeatured_clicked(object sender, EventArgs e)
        {
            var featured = new PageFeatured();
            await Navigation.PushAsync(featured);
            await featured.SetViewModel(vm) ;
            
           
        }
        
        async void imgvideo_selected(object sender, EventArgs e)
        {
            await PlayVideo();


        }
        async void btnNew_clicked(object sender, EventArgs e)
        {
            var newvideopage = new PageNew();
            await Navigation.PushAsync(newvideopage);
            await newvideopage.SetViewModel(vm.SelectedCategory);
        }

        

        
        async void btnTop_clicked(object sender, EventArgs e)
        {
            var topvideopage = new PageTop();
            await Navigation.PushAsync(topvideopage);
            await topvideopage.SetViewModel(vm.SelectedCategory);
           
        }
        async void btnPlaylist_clicked(object sender, EventArgs e)
        {
         
            var plpage = new PagePlaylist();
            await Navigation.PushAsync(plpage);
            await plpage.SetViewModel(vm.ListHeader.SelectedVideoId);


        
        }

        async void btsearch_clicked(object sender, EventArgs e)
        {
            //vm.LoadMeForFirstTime();
            vm.ClearForSearch();
            await vm.LoadMeForFirstTimeAsync();
        }
        async void btnadvsearch_clicked(object sender, EventArgs e)
        {
            var advsearch = new PageAdvancedSearch();
            await Navigation.PushAsync(advsearch);
        }
        
        async void ListView_ItemAppearing(object sender, EventArgs e)
        {
             try
             {
                    ItemVisibilityEventArgs t = (ItemVisibilityEventArgs)e;
                    IList items = vm.MVideoList.ToList();

                    if (items != null &&  items.Count > 0 && t!= null && t.Item == items[items.Count - 1])
                    {
                        await vm.LoadMeForSubsequentTime();
                    }
                }
            catch(Exception ex)
            {
                //swallow this
            }
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if ((e != null) && (e.Item != null))
            {
                ListView lv = sender as ListView;

                if (lv != null)
                    lv.SelectedItem = null;
            }


            MVideo item = e.Item as MVideo;
            if (item != null && item.ListHeader != null)
            {
                this.GetVM().ListHeader = item.ListHeader;
            }
            
            await ResetPage();

          
        }

        public ListView GetCurrentListView()
        {
            return this.videolistviewid;
        }

        public async Task<bool> ResetPage()
        {
            ListView lv = this.videolistviewid;
            layoutbody.Children.RemoveAt(0);
            layoutbody.Children.Insert(0, lv);

            
            return true;

        }

        public async Task<bool> PlayVideo()
        {
            PageVideoView vv = new AppNavTest.PageVideoView();
            ViewVideoVM vvm = await vv.SetVM();
            await Navigation.PushAsync(vv);
            vvm.SelectedVideoName = this.vm.ListHeader.VideoTitle;
            vvm.SelectedVideoId = this.vm.ListHeader.SelectedVideoId.ToString();
            vvm.IsLoading = true;
            //vvm.SelectedVideoURL = "https://vjs.zencdn.net/v/oceans.mp4"; // this.vm.ListHeader.VideoURL;
            await vvm.LoadMediaURL();
            vvm.IsAutoPlay = true;
            vvm.IsLoading = false;
           
            return true;

        }
    }
}
