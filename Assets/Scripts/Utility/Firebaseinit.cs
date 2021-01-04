using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using Firebase;
//using Firebase.Extensions;
//using Firebase.Analytics;

public class Firebaseinit : MonoBehaviour
{
    public static bool FirebaseInitStatus;

    private void Start()
    {
       //FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
       //{
       //    if (task.Exception != null)
       //    {
       //        Debug.LogError("Firebase Initialization Failed");
       //        return;
       //    }
       //    FirebaseInitStatus = true;
       //    // Firebase Analytics Init
       //    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
       //});
    }
}
