using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppNavTest
{
    public partial class PageVideoView : BaseContentPage
    {
        ViewVideoVM vm = null;
        public PageVideoView() : base()
        {
            InitializeComponent();
        }

        public async Task<ViewVideoVM> SetVM()
        {
            vm = new ViewVideoVM();
            this.id1mainpagecontent.BindingContext = vm;
            return vm;
        }

        async void btnReturn_clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }


}
