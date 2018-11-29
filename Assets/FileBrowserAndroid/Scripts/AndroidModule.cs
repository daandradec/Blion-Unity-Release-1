using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class AndroidModule : MonoBehaviour {

	public string GetAndroidRootExternalStorage()
    {
        try{
            using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Environment")){
                androidJavaClass.CallStatic<AndroidJavaObject>("getExternalStorageDirectory").Call<string>("getAbsolutePath");
                return "/storage";
            }
        }catch (Exception e){
            Debug.LogWarning("Error fetching native Android external storage dir: " + e.Message);
            return Directory.GetCurrentDirectory();
        }
    }
}


/* Documents */

/* 
    https://answers.unity.com/questions/1279669/how-can-i-browse-files-on-android-outside-of-the-u.html 
    https://docs.microsoft.com/en-US/dotnet/api/system.io.directory?view=netframework-4.7.2
    https://docs.unity3d.com/ScriptReference/AndroidJavaClass.html
    https://developer.android.com/reference/java/io/File
    https://developer.android.com/reference/android/os/Environment
    https://docs.unity3d.com/Manual/android-manifest.html
    https://developer.android.com/guide/topics/manifest/uses-permission-element
    https://developer.android.com/guide/topics/security/permissions
    https://www.tutorialspoint.com/android/android_sending_sms.htm
*/
