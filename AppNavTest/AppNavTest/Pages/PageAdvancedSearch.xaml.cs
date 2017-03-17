using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppNavTest
{
    public partial class PageAdvancedSearch :  BaseContentPage
    {
        public PageAdvancedSearch() : base()
        {
            InitializeComponent();
        }

        async void btnReturn_clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
