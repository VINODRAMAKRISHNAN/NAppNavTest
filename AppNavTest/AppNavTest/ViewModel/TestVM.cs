using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppNavTest
{
    public class MediaVideo
    {
        public string FLVURL { get; set; }
    }
    public class ListHeader : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isfeaturedavailable = false;
        public bool IsFeaturedAvailable
        {
            set
            {
                if (isfeaturedavailable != value)
                {
                    isfeaturedavailable = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("IsFeaturedAvailable"));

                    }
                }
            }
            get
            {
                return isfeaturedavailable;
            }
        }

        private long selectedvideoid = 0;
        public long SelectedVideoId
        {
            set
            {
                if (selectedvideoid != value)
                {
                    selectedvideoid = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("selectedvideoid"));

                    }
                }
            }
            get
            {
                return selectedvideoid;
            }
        }

        private string videoimgurl = "";
        public string VideoImgUrl
        {
            set
            {
                if (videoimgurl != value)
                {
                    videoimgurl = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("VideoImgUrl"));

                    }
                }
            }
            get
            {
                return videoimgurl;
            }
        }

        private string videotitle = "";
        public string VideoTitle
        {
            set
            {
                if (videotitle != value)
                {
                    videotitle = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("VideoTitle"));

                    }
                }
            }
            get
            {
                return videotitle;
            }
        }

        private string videodescription = "";
        public string VideoDescription
        {
            set
            {
                if (videodescription != value)
                {
                    videodescription = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("VideoDescription"));

                    }
                }
            }
            get
            {
                return videodescription;
            }
        }

        private string videourl = "";
        public string VideoURL
        {
            set
            {
                if (videourl != value)
                {
                    videourl = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("VideoURL"));

                    }
                }
            }
            get
            {
                return videourl;
            }
        }

        private string videoduration = "";
        public string VideoDuration
        {
            set
            {
                if (videoduration != value)
                {
                    videoduration = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("VideoDuration"));

                    }
                }
            }
            get
            {
                return videoduration;
            }
        }

        private string videodate = "";
        public string VideoDate
        {
            set
            {
                if (videodate != value)
                {
                    videodate = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("VideoDate"));

                    }
                }
            }
            get
            {
                return videodate;
            }
        }

        private string videodateviews = "";
        public string VideoViews
        {
            set
            {
                if (videodateviews != value)
                {
                    videodateviews = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("VideoViews"));

                    }
                }
            }
            get
            {
                return videodateviews;
            }
        }

    }

   
    public class ListPage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double novideoheight = 0.0;
        public double NoVideoHeight
        {
            set
            {
                if (novideoheight != value)
                {
                    novideoheight = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("NoVideoHeight"));

                    }
                }
            }
            get
            {
                return novideoheight;
            }
        }

        private bool isnovideovisible = false;
        public bool IsNoVideoVisible
        {
            set
            {
                if (isnovideovisible != value)
                {
                    isnovideovisible = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("IsNoVideoVisible"));

                    }
                }
            }
            get
            {
                return isnovideovisible;
            }
        }


        private int bpagenumber = 0;
        public int BPageNumber
        {
            set
            {
                if (bpagenumber != value)
                {
                    bpagenumber = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("BPageNumber"));
                    }
                }
            }
            get
            {
                return bpagenumber;
            }
        }

        private int curpagenumber = 0;
        public int CurPageNumber
        {
            set
            {
                if (curpagenumber != value)
                {
                    curpagenumber = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("CurPageNumber"));
                    }
                }
            }
            get
            {
                return curpagenumber;
            }
        }

        private int curpulledrecords = 0;
        public int CurPulledRecords
        {
            set
            {
                if (curpulledrecords != value)
                {
                    curpulledrecords = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("CurPulledRecords"));
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("NumberStatus"));
                    }
                }
            }
            get
            {
                return curpulledrecords;
            }
        }
        private int totalrecords = 0;
        public int TotalRecords
        {
            set
            {
                if (totalrecords != value)
                {
                   
                    totalrecords = value;
                    
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("TotalRecords"));
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("NumberStatus"));
                    }
                }
            }
            get
            {
                return totalrecords;
            }
        }

      
        public string NumberStatus
        {
          
            get
            {
                return CurPulledRecords.ToString() + "/" + TotalRecords.ToString();
            }
        }
    }

    public class ListSearch : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<FieldValuePair> allfields=new List<FieldValuePair>();
        public List<FieldValuePair> Allfields
        {
            set
            {
                if (allfields != value)
                {
                    allfields = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Allfields"));
                    }
                }
            }
            get
            {
                return allfields;
            }
        }

        private List<FieldValuePair> anyfields = new List<FieldValuePair>();
        public List<FieldValuePair> Anyfields
        {
            set
            {
                if (anyfields != value)
                {
                    anyfields = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Anyfields"));
                    }
                }
            }
            get
            {
                return anyfields;
            }
        }

        private SortOrder sortorder = SortOrder.Descending;
        public SortOrder SortOrder
        {
            set
            {
                if (sortorder != value)
                {
                    sortorder = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("SortOrder"));
                    }
                }
            }
            get
            {
                return sortorder;
            }
        }
        //  

        private SortBy sortby = SortBy.PublishDate;
        public SortBy SortBy
        {
            set
            {
                if (sortby != value)
                {
                    sortby = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("SortBy"));
                    }
                }
            }
            get
            {
                return sortby;
            }
        }

    }


     public class TestVM : INotifyPropertyChanged
    {
       
        private bool isloadfirstcalled = false;
        public  event  PropertyChangedEventHandler PropertyChanged;
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
        public void RaisePropertyEvent(string attributename)
        {
            this.PropertyChanged(this,new PropertyChangedEventArgs("SelectedCategoryDisplay"));
        }

        private string selectedcategory = "Video Home";
        public string SelectedCategory
        {
            set
            {
              
                    selectedcategory = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("SelectedCategory"));
                        PropertyChanged(this,
                        new PropertyChangedEventArgs("SelectedCategoryDisplay"));
                    }
                
            }
            get
            {
                return selectedcategory;
            }
        }

        public string SelectedCategoryDisplay
        {
            get
            {
                return selectedcategory + " (" + this.ListPage.TotalRecords.ToString()+ ")";
            }
        }


        private string searchstring = "";
        public string SearchString
        {
            set
            {
                if (searchstring != value)
                {
                    searchstring = value;
                   
                   
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("SearchString"));
                      
                    }

                    if (searchstring.Trim().Length == 0)
                    {
                        this.ListSearch.Anyfields.Clear();
                        if (PropertyChanged != null)
                        {
                            PropertyChanged(this,
                                new PropertyChangedEventArgs("ListSearch"));
                        }
                    }

                    if (searchstring.Trim().Length > 0)
                    {
                        this.ListSearch.Anyfields.Clear();
                        this.ListSearch.Anyfields.Add(new FieldValuePair(null, searchstring));
                        if (PropertyChanged != null)
                        {
                            PropertyChanged(this,
                                new PropertyChangedEventArgs("ListSearch"));
                        }
                    }

                    
                }
            }
            get
            {
                return searchstring;
            }
        }

        private ListHeader listheader = new ListHeader();
        public ListHeader ListHeader
        {
            set
            {
                if (listheader != value)
                {
                    listheader = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("ListHeader"));
                    }
                }
            }
            get
            {
                return listheader;
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

        public bool SetHeaderWithFeaturedVideo(CMSItemCollection<CMSApiVideo> fvideo)
        {
            MVideo mvideo = null;
            try
            {
                //var fvideo = await CMSApiServiceExtension.SearchVideosFeatured(this.ListSearch.Allfields, this.ListSearch.Anyfields, new List<FieldValuePair>(), 1, 0, SortBy.PublishDate, SortOrder.Descending, IncludePlaysTotal: true);
                if (fvideo != null && fvideo.Count > 0)
                {
                    var item = fvideo[0];
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

                    char[] charsToTrim = { '?' };
                    string turl = item.ThumbnailUrl == null ? "icon.png" : item.ThumbnailUrl.TrimEnd(charsToTrim);
                    mvideo = new MVideo { DisplayName = item.NameT, Heading = item.AccountId.ToString(), ImgUrl = "icon.png", NoOfViews = item.PlaysTotal.ToString(), PublishedDate = pubdate,Description=item.description };
                    mvideo.ImgUrl = turl;

                    int lengtht = 0;
                    if (item.duration != null)
                    {
                        lengtht = int.Parse(item.duration.ToString());
                    }
                    TimeSpan durationt = TimeSpan.FromMilliseconds(double.Parse(lengtht.ToString()));

                    this.ListHeader.SelectedVideoId = item.IdT;
                    this.ListHeader.VideoURL = "http://players.brightcove.net/4119874060001/321f0ea8-4a70-4101-8de4-b22992f451f5_default/index.html?videoId=" + item.IdT.ToString();
                    this.ListHeader.VideoTitle = item.NameT == null ? "No Title" : item.NameT;
                    this.ListHeader.VideoDescription = item.description == null ? "" : item.description;

                    if  (durationt.Hours == 0)
                    {
                        this.ListHeader.VideoDuration = item.duration == null ? "[0:00]" : "[" + string.Format("{0:D2}:{1:D2}", durationt.Minutes, durationt.Seconds) + "]";
                    }
                    else
                    {
                        this.ListHeader.VideoDuration = item.duration == null ? "[0:00]" : "[" + string.Format("{0:D2}:{1:D2}:{1:D3}", durationt.Hours, durationt.Minutes, durationt.Seconds) + "]";
                    }
                    this.ListHeader.VideoDate = pubdate ;
                    this.ListHeader.VideoViews = item.PlaysTotal.ToString() + " Views";
                    this.ListHeader.IsFeaturedAvailable = true;
                    this.ListHeader.VideoImgUrl = turl;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public void LoadMeForFirstTime()
        {
            try
            {
                Task<bool> x = LoadMeForFirstTimeAsync();
            }
            catch (Exception ex)
            {
                string s = "";
            }
        }
        
        public void ClearForSearch()
        {
            var x = MVideoList.ToList();
            x.Clear();
            MVideoList = new ObservableCollection<MVideo>(x);
            this.ListPage = new ListPage();
        }
        public  async Task<bool> LoadMeForFirstTimeAsync()
        {
           
            CMSItemCollection<CMSApiVideo> videos = new CMSItemCollection<CMSApiVideo>();
            try
            {
                this.IsLoading = true;
                //var x = MVideoList.ToList();
                //x.Clear();
                //MVideoList = new ObservableCollection<MVideo>(x);
                int newpage = 0;


                Task<CMSItemCollection<CMSApiVideo>> fvideoTask =  CMSApiServiceExtension.SearchVideosFeatured(this.ListSearch.Allfields, this.ListSearch.Anyfields, new List<FieldValuePair>(), 1, 0, SortBy.PublishDate, SortOrder.Descending, IncludePlaysTotal: true);

                Task<CMSItemCollection<CMSApiVideo>> tvideosTask =  CMSApiServiceExtension.SearchVideos(this.ListSearch.Allfields, this.ListSearch.Anyfields, new List<FieldValuePair>(), 10, newpage, this.ListSearch.SortBy, this.ListSearch.SortOrder, IncludePlaysTotal: true, IncludeVideoTotal: true);

                var fvideo = await fvideoTask;
                bool treturn = SetHeaderWithFeaturedVideo(fvideo);

                var tvideos = await tvideosTask;

                if (!tvideos.IsError && tvideos !=null)
                {
                   
                    this.ListPage.CurPageNumber = newpage;
                    this.ListPage.TotalRecords = tvideos.TotalCount;
                    this.ListPage.CurPulledRecords = tvideos.Count;

                    this.RaisePropertyEvent("SelectedCategoryDisplay");

                    videos.AddRange(tvideos);
                    int count = 0;
                    foreach (var item in videos)
                    {
                       
                        string pubdate = "No date found.";

                        try
                        {
                            pubdate = string.Format("{0} {1}, {2}", item.PublishedDate.ToString("MMMM"), item.PublishedDate.Day.ToString(), item.PublishedDate.ToString("yyyy"));
                         
                        }
                        catch(Exception ex)
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
                        MVideo temp = new MVideo { DisplayName = item.NameT, Heading = item.AccountId.ToString(), ImgUrl = "icon.png",NoOfViews = item.PlaysTotal.ToString(), PublishedDate=pubdate };
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
                        vdoListHeader.VideoImgUrl = turl;
                        temp.ListHeader = vdoListHeader;
                        MVideoList.Add(temp);
                        temp.ImgUrl = turl;

                        if (count == 0 && (!this.ListHeader.IsFeaturedAvailable))
                        {
                            this.ListHeader.SelectedVideoId = item.IdT;
                            this.ListHeader.VideoURL = "http://players.brightcove.net/4119874060001/321f0ea8-4a70-4101-8de4-b22992f451f5_default/index.html?videoId=" + item.IdT.ToString();
                            this.ListHeader.VideoTitle = item.NameT == null ? "No Title" : item.NameT;
                            this.ListHeader.VideoDescription = item.description == null ? "" : item.description;
                            if (durationt.Hours == 0)
                            {
                                this.ListHeader.VideoDuration = item.duration == null ? "[0:00]" : "[" + string.Format("{0:D2}:{1:D2}", durationt.Minutes, durationt.Seconds) + "]";
                            }
                            else
                            {
                                this.ListHeader.VideoDuration = item.duration == null ? "[0:00]" : "[" + string.Format("{0:D2}:{1:D2}:{1:D3}", durationt.Hours, durationt.Minutes, durationt.Seconds) + "]";
                            }

                            this.ListHeader.VideoDate = pubdate ;
                            this.ListHeader.VideoViews = item.PlaysTotal.ToString() + " Views";
                            this.listheader.VideoImgUrl = turl;


                        }
                        count = count + 1;
                    }
                    if (videos == null || (videos != null && videos.Count == 0))
                    {
                        MVideoList = new ObservableCollection<MVideo>();
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

        public async Task<bool> LoadMeForSubsequentTime()
        {
         

            CMSItemCollection<CMSApiVideo> videos = new CMSItemCollection<CMSApiVideo>();
            try
            {
                this.IsLoading = true;

                int newpage = this.ListPage.CurPageNumber + 1;
                if ((newpage * 10) >= this.ListPage.TotalRecords)
                {
                    this.IsLoading = false;
                    return true;
                }
                var tvideos = await CMSApiServiceExtension.SearchVideos(this.ListSearch.Allfields, this.ListSearch.Anyfields,null, 10, newpage, this.ListSearch.SortBy, this.ListSearch.SortOrder, IncludePlaysTotal: true, IncludeVideoTotal: false);

                if (!tvideos.IsError)
                {
                
                    this.ListPage.CurPageNumber = newpage;
                    videos.AddRange(tvideos);
                    int tnumber = this.ListPage.CurPulledRecords;
                    this.ListPage.CurPulledRecords = tnumber + tvideos.Count;
                    foreach (var item in videos)
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


                        char[] charsToTrim = { '?' };
                        string turl = item.ThumbnailUrl == null ? "icon.png" : item.ThumbnailUrl.TrimEnd(charsToTrim);
                        MVideo temp = new MVideo { DisplayName = item.NameT, Heading = item.AccountId.ToString(), ImgUrl = "icon.png", NoOfViews = item.PlaysTotal.ToString(), PublishedDate = pubdate };

                        int lengtht = 0;
                        if (item.duration != null)
                        {
                            lengtht = int.Parse(item.duration.ToString());
                        }
                        TimeSpan durationt = TimeSpan.FromMilliseconds(double.Parse(lengtht.ToString()));

                        ListHeader vdoListHeader = new ListHeader();
                        this.ListHeader.SelectedVideoId = item.IdT;
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
                        vdoListHeader.VideoImgUrl = turl;
                        temp.ListHeader = vdoListHeader;

                        MVideoList.Add(temp);
                        temp.ImgUrl = turl;
                    }

                    if (videos == null || (videos != null && videos.Count == 0))
                    {
                        MVideoList = new ObservableCollection<MVideo>();
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
      
        public static TestVM Clone(TestVM vm)
        {
            TestVM tvm = new TestVM();
            tvm.ListHeader = vm.ListHeader;
            tvm.ListPage = vm.ListPage;
            tvm.ListSearch = vm.ListSearch;
            tvm.MVideoList = new ObservableCollection<MVideo>(vm.MVideoList);
            tvm.SearchString = vm.SearchString; ;
            tvm.SelectedCategory = vm.SelectedCategory;

            return tvm;
        }

        public  void ResetVMDetails(TestVM vm)
        {
            TestVM tvm = this;
         
            tvm.MVideoList = new ObservableCollection<MVideo>(vm.MVideoList);
            tvm.ListHeader = vm.ListHeader;
            tvm.ListPage = vm.ListPage;
            tvm.ListSearch = vm.ListSearch;
            tvm.SearchString = vm.SearchString; ;
            tvm.SelectedCategory = vm.SelectedCategory;




        }
    }

    public class MPlaylist : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _discription = "";
        public string Discription
        {
            set
            {
                if (_discription != value)
                {
                    _discription = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Discription"));
                    }
                }
            }
            get
            {
                return _discription;
            }
        }

        private string _displayname = "";
        public string DisplayName
        {
            set
            {
                if (_displayname != value)
                {
                    _displayname = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("DisplayName"));
                    }
                }
            }
            get
            {
                return _displayname;
            }
        }
        private string _count = "";
        public string Count
        {
            set
            {
                if (_count != value)
                {
                    _count = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Count"));
                    }
                }
            }
            get
            {

                return "(" + _count +")";
            }
        }

        private ObservableCollection<MVideo> _videos = new ObservableCollection<MVideo>();
        public ObservableCollection<MVideo> Videos
        {
            set
            {
                if (_videos != value)
                {
                    _videos = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Videos"));
                    }
                }
            }
            get
            {
                return _videos;
            }
        }

    }
        public class MVideo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _imgurl = "";
        public string ImgUrl
        {
            set
            {
                if (_imgurl != value)
                {
                    _imgurl = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("ImgUrl"));
                    }
                }
            }
            get
            {
                return _imgurl;
            }
        }


        private string _displayname = "";
        public string DisplayName
        {
            set
            {
                if (_displayname != value)
                {
                    _displayname = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("DisplayName"));
                    }
                }
            }
            get
            {
                return _displayname;
            }
        }
        private string _heading = "";
        public string Heading
        {
            set
            {
                if (_heading != value)
                {
                    _heading = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Heading"));
                    }
                }
            }
            get
            {
                return _heading;
            }
        }

        private string _publisheddate = "";
        public string PublishedDate
        {
            set
            {
                if (_publisheddate != value)
                {
                    _publisheddate = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("PublishedDate"));
                    }
                }
            }
            get
            {
                return _publisheddate;
            }
        }

        private string _noofviews = "";
        public string NoOfViews
        {
            set
            {
                if (_noofviews != value)
                {
                    _noofviews = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("NoOfViews"));
                    }
                }
            }
            get
            {
                return _noofviews;
            }
        }

        private string _description = "";
        public string Description
        {
            set
            {
                if (_description != value)
                {
                    _description = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Description"));
                    }
                }
            }
            get
            {
                return _description;
            }
        }

        private ListHeader _listheader = null ;
        public ListHeader ListHeader
        {
            set
            {
                if (_listheader != value)
                {
                    _listheader = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("ListHeader"));
                    }
                }
            }
            get
            {
                return _listheader;
            }
        }
    }

}
