using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Utils;

namespace UITest100
{
    public class AppInitializer
    {
        //public static string Path = @"F:\AAA-DEV\AAA-TEST\AAA-LATEST\AppNavTest\AppNavTest\UITest123\App.Test.AppNavTest.apk";
        // public static string Path = @"..\..\App.Test.AppNavTest.apk";

        //public static string Path = @"..\..\apk\App.Test.AppNavTest-Signed.apk";
        public static string Path = @"F:\AAA-DEV\AAA-newTEST\AppNavTest\UITest100\apk\App.Test.AppNavTest.apk";

        public static IApp StartApp(Platform platform)
        {
            //if (platform == Platform.Android)
            //{
            //    return ConfigureApp
            //        .Android
            //        .StartApp();
            //}

            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .ApkFile(Path)
                    .EnableLocalScreenshots()
                    .WaitTimes(new WaitTimes())
                    .StartApp();
                //return ConfigureApp.Android
                ////.Debug()                
                //.EnableLocalScreenshots()
                //.ApkFile(Path)
                //.WaitTimes(new WaitTimes())
                //.StartApp(Xamarin.UITest.Configuration.AppDataMode.Clear);

            }

            return ConfigureApp
                    .iOS
                    .StartApp();
            }
        }
    
    public class WaitTimes : IWaitTimes
    {
        public TimeSpan GestureCompletionTimeout
        {
            get
            {
                return TimeSpan.FromMinutes(3);
            }
        }

        public TimeSpan GestureWaitTimeout
        {
            get
            {
                return TimeSpan.FromMinutes(3);
            }
        }

        public TimeSpan WaitForTimeout
        {
            get
            {
                return TimeSpan.FromMinutes(3);
            }
        }
    }
}

