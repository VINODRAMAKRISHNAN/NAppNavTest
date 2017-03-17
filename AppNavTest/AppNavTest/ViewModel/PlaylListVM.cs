using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNavTest
{
    public class PlayListVM : INotifyPropertyChanged
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

        public void RaisePropertyEvent(string attributename)
        {
            PropertyChanged(this,
                       new PropertyChangedEventArgs("SelectedCategoryDisplay"));
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

        private long selectedvideoid = 0;
        public long SelectedVideoId
        {
            set
            {
                if (selectedvideoid != value)
                {
                    selectedvideoid = value;

                    var x = MPlayList.ToList();
                    x.Clear();
                    MPlayList = new ObservableCollection<MPlaylist>(x);

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

        private ObservableCollection<MPlaylist> _mplayList = new ObservableCollection<MPlaylist>();
        public ObservableCollection<MPlaylist> MPlayList
        {
            get
            {
                return _mplayList;
            }
            set
            {
                _mplayList = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
                        new PropertyChangedEventArgs("MPlayList"));
                }

            }
        }

        public async Task<bool> LoadMeForFirstTimeNew()
        {

            try
            {

                this.IsLoading = true;

                var x = MPlayList.ToList();
                x.Clear();
                MPlayList = new ObservableCollection<MPlaylist>(x);

                int newpage = 0;
                // var ttvideos = await CMSApiServiceExtension.SearchVideos(this.ListSearch.Allfields, this.ListSearch.Anyfields, new List<FieldValuePair>(), 10, newpage, SortBy.PublishDate, SortOrder.Descending, IncludePlaysTotal: false, IncludeVideoTotal: false);

                var apiResponse = await CMSPlaylistApiServiceExtension.SendGetRequestAction(this.SelectedVideoId.ToString());
                var ttplist = CMSApiServiceExtension.DeserializeJsonStringToPlaylistSearchResult(apiResponse);

                if (ttplist != null)
                {
                    this.ListPage.CurPageNumber = newpage;
                    this.ListPage.TotalRecords = ttplist.Count;
                    this.ListPage.CurPulledRecords = ttplist.Count;
                    foreach (var item in ttplist)
                    {
                        MPlaylist temp = new MPlaylist { DisplayName = item.name == null ? "No Title" : item.name, Discription = item.description == null ? "No desc" : item.description.ToString(), Count = item.videos == null ? "0" : item.videos.Count.ToString() };
                        #region add videos to playlist
                        if (item.videos != null && item.videos.Count > 0)
                        {
                            foreach (var itemvdo in item.videos)
                            {
                                #region enter video values
                                    string pubdate = "No date found.";

                                    try
                                    {
                                        pubdate = string.Format("{0} {1}, {2}", itemvdo.PublishedDate.ToString("MMMM"), itemvdo.PublishedDate.Day.ToString(), itemvdo.PublishedDate.ToString("yyyy"));

                                    }
                                    catch (Exception ex)
                                    {
                                        //swallow exception
                                    }

                                    char[] charsToTrim = { '?' };
                                    string turl = itemvdo.ThumbnailUrl == null ? "icon.png" : itemvdo.ThumbnailUrl.TrimEnd(charsToTrim);
                                    MVideo tempvdo = new MVideo { DisplayName = itemvdo.NameT, Heading = itemvdo.AccountId.ToString(), ImgUrl = "icon.png", NoOfViews = itemvdo.PlaysTotal.ToString(), PublishedDate = pubdate, Description = itemvdo.description };

                                    int lengtht = 0;
                                    if (itemvdo.duration != null)
                                    {
                                        lengtht = int.Parse(itemvdo.duration.ToString());
                                    }
                                    TimeSpan durationt = TimeSpan.FromMilliseconds(double.Parse(lengtht.ToString()));


                                    ListHeader vdoListHeader = new ListHeader();
                                    vdoListHeader.SelectedVideoId = itemvdo.IdT;
                                    vdoListHeader.VideoURL = "http://players.brightcove.net/4119874060001/321f0ea8-4a70-4101-8de4-b22992f451f5_default/index.html?videoId=" + itemvdo.IdT.ToString();
                                    vdoListHeader.VideoTitle = itemvdo.NameT == null ? "No Title" : itemvdo.NameT;
                                    vdoListHeader.VideoDescription = itemvdo.description == null ? "" : itemvdo.description;

                                    if (durationt.Hours == 0)
                                    {
                                        vdoListHeader.VideoDuration = itemvdo.duration == null ? "[0:00]" : "[" + string.Format("{0:D2}:{1:D2}", durationt.Minutes, durationt.Seconds) + "]";
                                    }
                                    else
                                    {
                                        vdoListHeader.VideoDuration = itemvdo.duration == null ? "[0:00]" : "[" + string.Format("{0:D2}:{1:D2}:{1:D3}", durationt.Hours, durationt.Minutes, durationt.Seconds) + "]";
                                    }

                                    vdoListHeader.VideoDate = pubdate;
                                    vdoListHeader.VideoViews = itemvdo.PlaysTotal.ToString() + " Views";
                                    vdoListHeader.VideoImgUrl = turl;

                                    tempvdo.ListHeader = vdoListHeader;
                                    temp.Videos.Add(tempvdo);
                                    tempvdo.ImgUrl = turl;

                                #endregion

                            }
                        }
                        #endregion
                        MPlayList.Add(temp);

                    }

                    if (ttplist == null || (ttplist != null && ttplist.Count == 0))
                    {
                        MPlayList = new ObservableCollection<MPlaylist>();
                    }

                    if (MPlayList != null && MPlayList.Count > 0)
                    {
                        this.ListPage.IsNoVideoVisible = false;
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
