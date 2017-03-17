// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace IntuneMAMSampleiOS
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton buttonSave { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton buttonShare { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton buttonUrl { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField textCopy { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField textUrl { get; set; }

		[Action ("buttonSave_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void buttonSave_TouchUpInside (UIButton sender);

		[Action ("buttonShare_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void buttonShare_TouchUpInside (UIButton sender);

		[Action ("buttonUrl_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void buttonUrl_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (buttonSave != null) {
				buttonSave.Dispose ();
				buttonSave = null;
			}
			if (buttonShare != null) {
				buttonShare.Dispose ();
				buttonShare = null;
			}
			if (buttonUrl != null) {
				buttonUrl.Dispose ();
				buttonUrl = null;
			}
			if (textCopy != null) {
				textCopy.Dispose ();
				textCopy = null;
			}
			if (textUrl != null) {
				textUrl.Dispose ();
				textUrl = null;
			}
		}
	}
}
