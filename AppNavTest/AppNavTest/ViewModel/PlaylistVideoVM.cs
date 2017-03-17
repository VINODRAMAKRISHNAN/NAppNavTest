using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AppNavTest
{
    public class PlaylistVideoVM : INotifyPropertyChanged
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



        private string selectedplaylist = "";
        public string SelectedPlaylist
        {
            set
            {
                if (selectedplaylist != value)
                {
                    selectedplaylist = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("SelectedPlaylist"));
                       
                    }
                }
            }
            get
            {
                return "Playlist: " + selectedplaylist;
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

    }
}
