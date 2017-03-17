//-----------------------------------------------------------------------
// <copyright file="MainActivity.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Microsoft.Intune.Mam.Client.App;
using Microsoft.Intune.Mam.Client.Identity;
using Java.IO;
using Java.Util.Logging;

namespace IntuneMAMSampleAndroid
{

    [Activity(Label = "IntuneMAMSampleAndroid")]
    public class MainActivity : MAMActivity
    {
        private static Logger LOGGER = Logger.GetLogger(typeof(MainActivity).Name);

        private const int TESTAPP_TEXT_REQUEST = 1;
        private const string IS_MULTI_IDENTITY_ENABLED_KEY = "com.microsoft.intune.mam.MAMMultiIdentity";
        private const string SET_RESULT_AND_FINISH_KEY = "com.microsoft.mdm.testappbase.setResultAndFinish";
        
        private bool usePersistentClipboard = false;
        private ClipboardManager persistentClipboard;

        public override void OnMAMCreate(Bundle bundle)
        {
            base.OnMAMCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            this.Window.SetSoftInputMode(SoftInput.StateHidden);

            InitializeStateCheckboxes();

            FindViewById<Button>(Resource.Id.copyTextButton).Click += OnClickCopy;
            FindViewById<Button>(Resource.Id.pasteTextButton).Click += OnClickPaste;
            FindViewById<Button>(Resource.Id.showIdentityOptionsButton).Click += OnClickToggleIdentityOptions;
            FindViewById<Button>(Resource.Id.showContentProviderButton).Click += OnClickShowContentProviderFragment;

            // Identity options and buttons
            FindViewById<RadioGroup>(Resource.Id.threadedIdentityOptionRadioGroup).Click += OnClickThreadedIdentityOption;
            FindViewById<Button>(Resource.Id.identitySetUIButton).Click += OnIdentityFragmentClick;
            FindViewById<Button>(Resource.Id.identityClearUIButton).Click += OnIdentityFragmentClick;
            FindViewById<Button>(Resource.Id.identityLaunchButton).Click += OnIdentityFragmentClick;
            FindViewById<Button>(Resource.Id.identitySetThreadButton).Click += OnIdentityFragmentClick;
            FindViewById<Button>(Resource.Id.identityClearThreadButton).Click += OnIdentityFragmentClick;
            FindViewById<Button>(Resource.Id.identitySetProcessButton).Click += OnIdentityFragmentClick;
            FindViewById<Button>(Resource.Id.identityClearProcessButton).Click += OnIdentityFragmentClick;

            // Intent handling buttons
            FindViewById<Button>(Resource.Id.copyIntentButton).Click += OnClickCopyIntent;
            FindViewById<Button>(Resource.Id.pasteIntentButton).Click += OnClickPasteIntent;
            FindViewById<Button>(Resource.Id.sendIntentButton).Click += OnClickActionSend;
            FindViewById<Button>(Resource.Id.returnResultButton).Click += OnClickReturnResult;
            FindViewById<Button>(Resource.Id.saveAsButton).Click += OnClickSaveAs;
            FindViewById<Button>(Resource.Id.httpButton).Click += OnClickHttp;

            // File operation buttons.
            FindViewById<Button>(Resource.Id.write5kToFileButton).Click += OnClickFileWrite;
            FindViewById<Button>(Resource.Id.readFromFileButton).Click += OnClickFileRead;
        }

        /// <summary>
        /// Performed when "Paste" button is clicked.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickPaste(object sender, EventArgs args)
        {
            ClipboardManager clipboard = GetClipboard();
            ClipData clipData = clipboard.PrimaryClip;
            string pasteText = (clipData == null) ? "<null>" : clipboard.PrimaryClip.GetItemAt(0).Text;
            pasteText = string.IsNullOrEmpty(pasteText) ? "<empty>" : pasteText;
            TextView pasteTextView = (TextView)FindViewById<TextView>(Resource.Id.pasteText);
            pasteTextView.Text = pasteText;
        }

        /// <summary>
        /// Gets the clipboard to use for various copy/paste buttons in the MainActivity.
        /// </summary>
        /// <returns>The ClipboardManager to copy/paste from.</returns>
        private ClipboardManager GetClipboard()
        {
            if (usePersistentClipboard && persistentClipboard != null)
                return persistentClipboard;

            ClipboardManager mgr;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                mgr = (ClipboardManager)GetSystemService(Java.Lang.Class.FromType(typeof(ClipboardManager)));
            else
                mgr = (ClipboardManager)GetSystemService(Context.ClipboardService);

            if (usePersistentClipboard)
                persistentClipboard = mgr;
            return mgr;
        }

        private void InitializeStateCheckboxes()
        {
            bool multiIdentity = false;
            try
            {
                Bundle metaData = this.PackageManager.GetApplicationInfo(this.PackageName, PackageInfoFlags.MetaData).MetaData;
                if (metaData != null)
                {
                    multiIdentity = metaData.GetBoolean(IS_MULTI_IDENTITY_ENABLED_KEY);
                }
            }
            catch (PackageManager.NameNotFoundException e)
            {
                System.Diagnostics.Debug.Fail(string.Format("NameNotFoundException {0}", e));
            }

            CheckBox debuggableCheckBox = FindViewById<CheckBox>(Resource.Id.isDebuggableCheckBox);
            debuggableCheckBox.Checked = 0 != (ApplicationInfo.Flags & ApplicationInfoFlags.Debuggable);
            CheckBox testOnlyCheckBox = FindViewById<CheckBox>(Resource.Id.isTestOnlyCheckBox);
            testOnlyCheckBox.Checked = 0 != (ApplicationInfo.Flags & ApplicationInfoFlags.TestOnly);
            CheckBox multiIdentityCheckBox = FindViewById<CheckBox>(Resource.Id.isMultiIdentityCheckBox);
            multiIdentityCheckBox.Checked = multiIdentity;
        }

        /// <summary>
        /// Called when "Show Identity Options" or "Hide Identity Options" button is clicked.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        private void OnClickToggleIdentityOptions(object sender, EventArgs e)
        {
            Button buttonView = (Button)sender;
            View optionsView = FindViewById(Resource.Id.identityFragmentHolder);
            if (optionsView.Visibility != ViewStates.Visible)
            {
                // Show the options, set the text to "Hide Identity Options"
                optionsView.Visibility = ViewStates.Visible;
                buttonView.Text = GetString(Resource.String.btn_hide_identity_options);
            }
            else
            {
                // Hide the options, set the text to "Show Identity Options"
                optionsView.Visibility = ViewStates.Gone;
                buttonView.Text = GetString(Resource.String.btn_show_identity_options);
            }
        }

        /// <summary>
        /// Shows the content provider TestFragment.
        /// </summary>
        private void OnClickShowContentProviderFragment(object sender, EventArgs e)
        {
            if (this.FindViewById<Button>(Resource.Id.hideFragmentButton) != null)
            {
                return;
            }

            var ft = FragmentManager.BeginTransaction();
            var fragment = new TestFragment();
            ft.Add(Resource.Id.fragmentHolder, fragment);
            ft.Commit();
        }

        /// <summary>
        /// Called when buttons in the identity fragment are clicked.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        private void OnIdentityFragmentClick(object sender, EventArgs e)
        {
            View view = (View)sender;
            MultiIdentityFragment frag = FragmentManager.FindFragmentById<MultiIdentityFragment>(Resource.Id.identityFragment);
            frag.OnClick(view);
        }

        /// <summary>
        /// Called when radio options in the identity fragment are clicked.
        /// </summary>
        /// <param name="view">The clicked radio button.</param>
        private void OnClickThreadedIdentityOption(object sender, EventArgs e)
        {
            View view = (View)sender;
            MultiIdentityFragment frag = FragmentManager.FindFragmentById<MultiIdentityFragment>(Resource.Id.identityFragment);
            frag.OnClickThreadedIdentityOption(view);
        }

        private void OnClickCopy(object sender, EventArgs args)
        {
            ClipboardManager clipboard = GetClipboard();
            EditText copyText = FindViewById<EditText>(Resource.Id.copyText);
            ClipData clip = ClipData.NewPlainText("label", copyText.Text);
            clipboard.PrimaryClip = clip;
        }

        private void OnClickCopyIntent(object sender, EventArgs e)
        {
            Intent copyIntent = new Intent(this, Java.Lang.Class.FromType(typeof(MainActivity)));
            ClipData clip = ClipData.NewIntent("CopyIntent", copyIntent);
            ClipboardManager clipboard = GetClipboard();
            clipboard.PrimaryClip = clip;
        }

        private void OnClickPasteIntent(object sender, EventArgs e)
        {
            ClipboardManager clipboard = GetClipboard();
            ClipData clipData = clipboard.PrimaryClip;
            Intent pastedIntent = clipData.GetItemAt(0).Intent;
            this.StartActivity(pastedIntent);
        }

        private void OnClickHttp(object sender, EventArgs e)
        {
            Intent intent = IntentFactory.NewIntent(Intent.ActionView, Android.Net.Uri.Parse("http://www.microsoft.com/"));
            this.StartActivity(intent);
        }

        /// <summary>
        /// Performed when "Send" button is clicked.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickActionSend(object sender, EventArgs e)
        {
            SendIntent(null);
        }

        /// <summary>
        /// Performed when "Save As" button is clicked. No save action is taken here. The intention is that an app queries to see if
        /// 'save to personal' is allowed. If it's disabled, it's up to them to the app that action from happening.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickSaveAs(object sender, EventArgs e)
        {
            string text = (MAMPolicyManager.GetPolicy(this).IsSaveToPersonalAllowed) ? "allowed" : "disabled";
            TextView pasteTextView = FindViewById<TextView>(Resource.Id.pasteText);
            pasteTextView.Text = text;
        }

        /// <summary>
        /// Performed when "Reply to Intent" button is clicked.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickReturnResult(object sender, EventArgs e)
        {
            ReplyIntent(true);
        }

        /// <summary>
        /// Send a default intent to the given target.
        /// </summary>
        /// <param name="target">String representation of the ComponentName of the receiver of the intent. If set to null, a chooser will be prompted.</param>
        private void SendIntent(string target)
        {
            Intent temp = IntentFactory.NewIntent(Intent.ActionSend);
            temp.SetType("text/plain");
            temp.PutExtra(Intent.ExtraText, GetString(Resource.String.ApplicationName));

            Intent intent = temp;
            if (null != target)
            {
                intent.PutExtra(SET_RESULT_AND_FINISH_KEY, true);
                intent.SetComponent(ComponentName.UnflattenFromString(target));
            }
            else
            {
                intent = IntentFactory.CreateChooser(temp, "Chooser Title");
            }
            this.StartActivityForResult(intent, TESTAPP_TEXT_REQUEST);
        }

        /// <summary>
        /// Reply an intent to the activity that started this activity.
        /// </summary>
        /// <param name="finishActivity">Finish the current activity afterwards if true, false otherwise.</param>
        private void ReplyIntent(bool finishActivity)
        {
            // Create intent to deliver some kind of result data
            Intent result = IntentFactory.NewIntent(Intent.ActionSend);
            result.PutExtra(Intent.ExtraText, GetString(Resource.String.ApplicationName) + "-reply");
            result.SetType("text/plain");
            if (Parent == null)
            {
                SetResult(Result.Ok, result);
            }
            else
            {
                Parent.SetResult(Result.Ok, result);
            }
            if (finishActivity)
            {
                Finish();
            }
        }

        /// <summary>
        /// Performed when "Write 5K to file" button is clicked. This is for debugging file encryption by creating a new file on external
        /// storage and write 5K (cross the fblock boundary) ASCII content to it.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        /// <exception cref="IOException">on error</exception>
        public void OnClickFileWrite(object sender, EventArgs args)
        {
            DoFileOperation(System.IO.File.WriteAllText);
        }

        /// <summary>
        /// Performed when "Read from File" button is clicked. This is for debugging file encryption reading the previously created test
        /// file back and verify the content.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        /// <exception cref="IOException">on error</exception>
        public void OnClickFileRead(object sender, EventArgs args)
        {
            DoFileOperation((path, expected) =>
            {
                var actual = System.IO.File.ReadAllText(path);
                if (actual.Length != expected.Length)
                {
                    string err = String.Format("Read size error ({0} should be {1}) for file {3}", actual.Length, expected.Length, path);
                    LOGGER.Severe(err);
                    throw new System.IO.IOException(err);
                }

                var pairs = Enumerable.Zip(actual, expected, (a, e) => new { Actual = a, Expected = e });
                foreach (var it in pairs.Select((x, i) => new { Value = x, Index = i }))
                {
                    if (it.Value.Actual != it.Value.Expected)
                    {
                        var err = string.Format("Read char error ({0} should be {1}) at offset {2} of file {3}", it.Value.Actual, it.Value.Expected, it.Index, path);
                        LOGGER.Severe(err);
                        throw new System.IO.IOException(err);
                    }
                }
            });
        }

        /// <summary>
        /// Abstraction over file operations. Use same file for each.
        /// </summary>
        /// <param name="op">Operation to perform on file</param>
        private void DoFileOperation(Action<string, string> op)
        {
            // Content seed data.
            var seedContent = "abcde01234";
            // Iterations for seed data.
            var seedIterations = 500;
            // File name.
            var filename = "IntuneMAMSample-testfile.txt";

            try
            {
                var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(documentsPath, filename);
                op(filePath, string.Concat(Enumerable.Repeat(seedContent, seedIterations)));
            }
            catch (Exception e)
            {
                ShowError(e);
            }
        }

        /// <summary>
        /// Shows a user dialog for error text contained in an exception.
        /// </summary>
        /// <param name="err">The exception.</param>
        private void ShowError(Exception err)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(err.Message);
            builder.SetTitle("Error!");
            builder.SetPositiveButton(new Java.Lang.String("OK"), (sender, args) => { });
            AlertDialog dialog = builder.Create();
            dialog.Show();
        }
    }
}

