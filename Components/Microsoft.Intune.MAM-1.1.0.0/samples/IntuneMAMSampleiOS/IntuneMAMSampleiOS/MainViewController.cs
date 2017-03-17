//-----------------------------------------------------------------------
// <copyright file="MainViewController.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation;
using System;
using System.IO;
using UIKit;
using Microsoft.Intune.MAM;

namespace IntuneMAMSampleiOS
{
	partial class MainViewController : UIViewController
	{
		public MainViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad ()
        {
			this.textCopy.ShouldReturn += this.DismissKeyboard;
			this.textUrl.ShouldReturn += this.DismissKeyboard;
        }

		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
		}

        partial void buttonUrl_TouchUpInside (UIButton sender)
        {
            var url = textUrl.Text;

            if (string.IsNullOrWhiteSpace (url))
				url = "http://www.microsoft.com/en-us/server-cloud/products/microsoft-intune/";
            
            UIApplication.SharedApplication.OpenUrl (NSUrl.FromString (url));
        }

        partial void buttonShare_TouchUpInside (UIButton sender)
        {
            var text = new NSString ("Test Content Sharing from Intune Sample App");
            var avc = new UIActivityViewController (new NSObject[] { text }, null);
            // Added this (purloined from IntuneMAMFormsSample.iOS) to prevent the following crash in iOS10:
            // 
            // "Your application has presented a UIActivityViewController. In its current trait environment, the modalPresentationStyle 
            // of a UIActivityViewController with this style is UIModalPresentationPopover. You must provide location information for 
            // this popover through the view controller's popoverPresentationController. You must provide either a sourceView and 
            // sourceRect or a barButtonItem."
            //
            // (pre-iOS10, the modalPresentationStyle of a UIActivityViewController is UIModalPresentationStyle.Custom, and no
            // *PresentationController population is needed.)
            if (avc.PopoverPresentationController != null)
            {
                var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
                var frame = UIScreen.MainScreen.Bounds;
                frame.Height /= 2;
                avc.PopoverPresentationController.SourceView = topController.View;
                avc.PopoverPresentationController.SourceRect = frame;
                avc.PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Unknown;
            }

            PresentViewController(avc, true, null);
        }

        partial void buttonSave_TouchUpInside (UIButton sender)
        {
			// App to enforce IsSaveToPersonalAllowed (optional)
			if (!IntuneMAMPolicyManager.Instance.Policy.IsSaveToPersonalAllowed)
			{
				ShowAlert ("Blocked", "Blocked from writing to personal location");
				return;
			}

			var fileName = "intune-test.txt";
			var path = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), fileName);

            try
			{
                File.WriteAllText (path, "Test Save to Personal");
				ShowAlert ("Succeeded", "Wrote to " + fileName);
            }
			catch (Exception)
			{
				ShowAlert ("Failed", "Failed to write to " + fileName);
            }
        }

        void ShowAlert (string title, string message)
        {
            BeginInvokeOnMainThread (() => {
                var av = new UIAlertView (title, message, null, "OK");
                av.Show ();
            });
        }

		bool DismissKeyboard (UITextField textField)
		{
			textField.ResignFirstResponder ();
			return true;
		}
	}
}
