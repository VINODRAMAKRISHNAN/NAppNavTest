using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace AppNavTest.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (object sender, UnhandledExceptionEventArgs e) => {
                //handle exception here
                //Insights.Report((Exception)e.ExceptionObject, ReportSeverity.Error);
                string ss = "";
              
            };
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");

            // var dummyObject = new Octane.Xam.VideoPlayer.PreserveAttribute();



        }
    }
}
