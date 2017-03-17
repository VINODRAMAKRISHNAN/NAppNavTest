
The Intune App SDK Xamarin component enables mobile app management features with Microsoft Intune mobile iOS and Android apps built with Xamarin. The component allows a developer to easily build in data protection features into their app. You will find that you can enable most of the SDK features without changing your app’s behavior. There are few APIs provided for your app to provide better customization into the user experience, however, these APIs are optional and aren't required to get your mobile app enabled for management. Once you’ve enabled your app, IT administrators can deploy policies to Intune managed apps and take advantage of these features to protect their corporate data.

Once you've built the component into your mobile app, the IT admin will be able to deploy policy via Microsoft Intune supporting a variety of features. Below we've listed all the features your app can support without any additional app changes.


##Control users’ ability to move corporate documents

IT administrators can control file relocation of corporate documents in Intune managed apps. For instance, they can deploy a policy that disables file backup apps from backing up corporate data to the cloud.

##Configure clipboard restrictions

IT administrators can configure the clipboard behavior in Intune managed apps. For instance, they can deploy a policy so that end users are unable to use the clipboard to cut/copy from an Intune managed app and paste into a non-managed app.

##Configure screen capture restrictions

IT administrators can prevent users from capturing the screen if an Intune-managed app is displayed. Applying this restriction prevents the capture and release of confidential corporate content. This feature is only available for Android devices.

##Enforce encryption on saved data

IT administrators can enforce a policy that ensures that data saved to the device by the app is encrypted.

##Remotely wipe corporate data

IT administrators can remotely wipe corporate data from an Intune-managed app when the device is unenrolled from Microsoft Intune. This feature is identity-based and will delete only the files that relate to the corporate identity of the end user. To do that, the feature requires the app’s participation. The app can specify the identity for which the wipe should occur based on user settings. In the absence of these specified user settings from the app, the default behavior is to wipe the application directory and notify the end user that company resource access has been removed.

##Enforce the use of a Managed Browser

IT administrators can enforce the use of a Managed Browser when opening links from within Intune-managed apps. Using the Microsoft Intune Managed Browser helps to ensure that links that appear in emails (which are in an Intune-managed mail client) are kept within the domain of Intune managed apps.

##Enforce a PIN policy

IT administrators can enforce a PIN policy when an Intune-managed app is started. This policy helps to ensure that the end users who enrolled their devices with Microsoft Intune are the same individuals who are starting the apps. When end users configure their PIN, Intune App SDK uses Azure Active Directory to verify the credentials of end users against the device enrollment credentials.

##Require users to enter credentials before they can start apps

IT administrators can require users to enter their credentials before users can start an Intune-managed app. Intune App SDK uses Azure Active Directory to provide a single sign-on experience, where the credentials, once entered, are reused for subsequent logins. We also support authentication of identity management solutions federated with Azure Active Directory.

##Check device health and compliance

IT administrators can a check the health of the device and its compliance with corporate policies before end users access Intune-managed apps. On the iOS platform, this policy checks if the device has been jailbroken. On the Android platform, this policy checks if the device has been rooted.

##Enforce encryption on saved data
IT administrators can enforce a policy that ensures that data saved to the device by the app is encrypted.
