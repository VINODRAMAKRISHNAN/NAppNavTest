using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppNavTest
{
   
    public class FeaturedVM : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        private bool isloading = false;
        public bool IsLoading
        {
            set
            {
                if (isloading != value)
                {
                    isloading = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("IsLoading"));

                    }
                }
            }
            get
            {
                return isloading;
            }
        }




        private string selectedcategory = "Video Home";
        public string SelectedCategory
        {
            set
            {
                if (selectedcategory != value)
                {
                    selectedcategory = value;

                    var x = MVideoList.ToList();
                    x.Clear();
                    MVideoList = new ObservableCollection<MVideo>(x);

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("SelectedCategory"));
                        PropertyChanged(this,
                        new PropertyChangedEventArgs("SelectedCategoryDisplay"));
                    }
                }
            }
            get
            {
                return selectedcategory;
            }
        }


       

        private ListPage listpage = new ListPage();
        public ListPage ListPage
        {
            set
            {
                if (listpage != value)
                {
                    listpage = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("ListPage"));
                    }
                }
            }
            get
            {
                return listpage;
            }
        }

        private ListSearch listsearch = new ListSearch();
        public ListSearch ListSearch
        {
            set
            {
                if (listsearch != value)
                {
                    listsearch = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("ListSearch"));
                    }
                }
            }
            get
            {
                return listsearch;
            }
        }

        private ObservableCollection<MVideo> _mvideoList = new ObservableCollection<MVideo>();
        public ObservableCollection<MVideo> MVideoList
        {
            get
            {
                return _mvideoList;
            }
            set
            {
                _mvideoList = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
                        new PropertyChangedEventArgs("MVideoList"));
                }

            }
        }

        public async Task<bool> LoadMeForFirstTime()
        {
            this.IsLoading = true;
         
            try
            {
                var featuredVideos = new CMSItemCollection<CMSApiVideo>();
              
                int newpage = 0;
                var ttvideos =  await CMSApiServiceExtension.SearchVideosFeatured(this.ListSearch.Allfields, this.ListSearch.Anyfields, new List<FieldValuePair>(), 10, newpage, SortBy.PublishDate, SortOrder.Descending);
                var tvideos = ttvideos;
                if (!tvideos.IsError && tvideos != null)
                {
                    
                    if (tvideos != null)
                    {
                        tvideos.RemoveAll(y => y.PublishedDate < DateTime.Now.AddMonths(-6));
                        featuredVideos = tvideos;
                    }

                    
                    this.ListPage.CurPageNumber = newpage;
                    this.ListPage.TotalRecords = featuredVideos.Count;
                    this.ListPage.CurPulledRecords = featuredVideos.Count;

                    foreach (var item in featuredVideos)
                    {

                        string pubdate = "No date found.";

                        try
                        {
                            pubdate = string.Format("{0} {1}, {2}", item.PublishedDate.ToString("MMMM"), item.PublishedDate.Day.ToString(), item.PublishedDate.ToString("yyyy"));

                        }
                        catch (Exception ex)
                        {
                            string s = "test";
                            //swallow exception
                        }

                        int lengtht = 0;
                        if (item.duration != null)
                        {
                            lengtht = int.Parse(item.duration.ToString());
                        }
                        TimeSpan durationt = TimeSpan.FromMilliseconds(double.Parse(lengtht.ToString()));

                        char[] charsToTrim = { '?' };
                        string turl = item.ThumbnailUrl == null ? "icon.png" : item.ThumbnailUrl.TrimEnd(charsToTrim);
                        MVideo temp = new MVideo { DisplayName = item.NameT, Heading = item.AccountId.ToString(), ImgUrl = "icon.png", NoOfViews = item.PlaysTotal.ToString(), PublishedDate = pubdate, Description = item.description };

                        ListHeader vdoListHeader = new ListHeader();
                        vdoListHeader.SelectedVideoId = item.IdT;
                        vdoListHeader.VideoURL = "http://players.brightcove.net/4119874060001/321f0ea8-4a70-4101-8de4-b22992f451f5_default/index.html?videoId=" + item.IdT.ToString();
                        vdoListHeader.VideoTitle = item.NameT == null ? "No Title" : item.NameT;
                        vdoListHeader.VideoDescription = item.description == null ? "" : item.description;
                       
                        if (durationt.Hours == 0)
                        {
                            vdoListHeader.VideoDuration = item.duration == null ? "[0:00]" : "[" + string.Format("{0:D2}:{1:D2}", durationt.Minutes, durationt.Seconds) + "]";
                        }
                        else
                        {
                            vdoListHeader.VideoDuration = item.duration == null ? "[0:00]" : "[" + string.Format("{0:D2}:{1:D2}:{1:D3}", durationt.Hours, durationt.Minutes, durationt.Seconds) + "]";
                        }

                        vdoListHeader.VideoDate = pubdate;
                        vdoListHeader.VideoViews = item.PlaysTotal.ToString() + " Views";
                        vdoListHeader.VideoImgUrl= turl;
                        temp.ListHeader = vdoListHeader;


                        MVideoList.Add(temp);
                        temp.ImgUrl = turl;
                    }

                    if (tvideos == null || (tvideos != null && tvideos.Count == 0))
                    {
                        MVideoList = new ObservableCollection<MVideo>();
                    }

                    if (MVideoList != null && MVideoList.Count > 0)
                    {
                        
                        this.ListPage.IsNoVideoVisible =  false;
                       
                    }
                    else
                    {
                        this.ListPage.IsNoVideoVisible = true;
                    }
                }
            }
            catch (Exception ee)
            {
                string sss = ee.Message.ToString();
               
            }
            finally
            {
                this.IsLoading = false;
            }
            return true;
        }

       

    }

}
