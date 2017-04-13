using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest100
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

       


        [Test]
        public void NewTest()
        {
            try
            {

                app.Screenshot("Video Home");
                app.WaitForElement(x => x.Text("New"));
                app.Tap(c => c.Marked("Featured"));
                app.Screenshot("Featured Videos");

                app.Tap(x => x.Class("FormsImageView").Index(2));
                app.Screenshot("MainMenu >> Navigate to >> Products");
                app.Tap(c => c.Marked("Products        >>"));
                app.WaitForElement(x => x.Text("New"));
                app.Screenshot("Products");

                app.Tap(x => x.Class("FormsImageView").Index(1));
                app.Screenshot("MainMenu >> Navigate to >> Services");
                app.Tap(c => c.Marked("Services         >>"));
                app.WaitForElement(x => x.Text("New"));
                app.Screenshot("Services");

                app.Tap(x => x.Class("FormsImageView").Index(1));
                app.Screenshot("MainMenu >> Navigate to >> Corporate");
                app.Tap(c => c.Marked("Corporate     >>"));
                app.WaitForElement(x => x.Text("New"));
                app.Screenshot("Corporate");

                app.Tap(x => x.Class("FormsImageView").Index(1));
                app.Screenshot("MainMenu >> Navigate to >> Events");
                app.Tap(c => c.Marked("Events           >>"));
                app.WaitForElement(x => x.Text("New"));
                app.Screenshot("Events");

                app.Tap(x => x.Class("FormsImageView").Index(1));
                app.Screenshot("MainMenu >> Navigate to >> Video Home");
                app.Tap(c => c.Marked("Video Home   >>"));
                app.WaitForElement(x => x.Text("New"));
                app.Screenshot("Video Home");
                
                app.Tap(x => x.Class("FormsImageView").Index(5));
                app.WaitForElement(x => x.Text("Return"));
                app.Screenshot("Selected Video Playing");
                //app.Tap(x => x.Class("Platform_DefaultRenderer").Index(10));
                app.Tap(x => x.Marked("Return"));
                app.Screenshot("Back to Video Home");

                Assert.True(true);
            }
            catch (Exception ee)
            {
                Assert.True(false);
            }
        }

        #region commented code
        //[Test]
        //public void NewTest1()
        //{
        //    app.Screenshot("Screenshot");
        //    app.Screenshot("Screenshot");
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.Tap(x => x.Class("FormsImageView").Index(1));
        //    app.Tap(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Text("Products        >>"));
        //    app.WaitForElement(x => x.Text("Products        >>"));
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Text("Products        >>"));
        //    app.WaitForElement(x => x.Text("Products        >>"));
        //    app.WaitForElement(x => x.Text("Featured"));
        //    app.WaitForElement(x => x.Text("Featured"));
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Text("Services         >>"));
        //    app.WaitForElement(x => x.Text("Services         >>"));
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Class("Platform_DefaultRenderer").Index(1));
        //    app.WaitForElement(x => x.Class("Platform_DefaultRenderer").Index(1));
        //    app.WaitForElement(x => x.Text("Corporate     >>"));
        //    app.WaitForElement(x => x.Text("Corporate     >>"));
        //    app.Tap(x => x.Text("Corporate     >>"));
        //    app.Tap(x => x.Text("Corporate     >>"));
        //    app.WaitForElement(x => x.Text("Events           >>"));
        //    app.WaitForElement(x => x.Text("Events           >>"));
        //    app.WaitForElement(x => x.Text("Video Home   >>"));
        //    app.WaitForElement(x => x.Text("Video Home   >>"));
        //}

        //[Test]
        //public void NewTest()
        //{
        //    app.Screenshot("Screenshot");
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.Tap(x => x.Class("FormsImageView").Index(1));
        //    app.WaitForElement(x => x.Text("Events           >>"));
        //    app.WaitForElement(x => x.Text("Events           >>"));
        //}

        //[Test]
        //public void NewTest()
        //{
        //    app.Screenshot("Screenshot");
        //    app.WaitForElement(x => x.Text("New"));
        //    app.Tap(c => c.Marked("Top"));
        //    Assert.True(true);
        //    ////app.Screenshot("Waited for view with class: AppCompatButton with text: New");
        //    ////app.Screenshot("sss");
        //    ////app.WaitForElement(x => x.Class("FormsImageView").Index(2));
        //    ////app.Screenshot("Waited for view with class: FormsImageView");
        //}

        //[Test]
        //public void NewTest()
        //{
        //    app.Screenshot("Screenshot");
        //    app.Screenshot("222");
        //    app.Screenshot("33");
        //    app.Screenshot("6666");
        //    app.WaitForElement(x => x.Class("FormsImageView").Index(1));
        //    app.Tap(x => x.Text("Services         >>"));
        //    app.SwipeLeftToRight();
        //}

        #endregion



    }
}

