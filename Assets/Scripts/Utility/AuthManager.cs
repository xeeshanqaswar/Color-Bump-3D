using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase.Extensions;
//using Firebase.Auth;
//using Firebase;

namespace Custom.Scripts
{
    //public class AuthManager : MonoBehaviour
    //{

    //    #region FIELD DECLERATION

    //    [Header("FIELDS REFERENCES")]
    //    public TMP_InputField loginEmail;
    //    public TMP_InputField loginPass;

    //    public TMP_InputField registerEmail;
    //    public TMP_InputField registerPass;
    //    public TMP_InputField registerConfirmPass;


    //    private FirebaseAuth _auth;
    //    #endregion

    //    private void Awake()
    //    {
    //        if (Firebaseinit.FirebaseInitStatus)
    //        {
    //            _auth = FirebaseAuth.DefaultInstance;
    //        }
    //    }

    //    public void OnLoginPress()
    //    {
    //        _auth.SignInWithEmailAndPasswordAsync(loginEmail.text, loginPass.text).ContinueWithOnMainThread(task => {
                
    //            if (task.IsCanceled)
    //            {
    //                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
    //                return;
    //            }

    //            if (task.IsFaulted)
    //            {
    //                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
    //                return;
    //            }

    //            FirebaseUser newUser = task.Result;
    //            Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
    //        });
    //    }

    //    public void OnRegisterPress()
    //    {
    //        if (string.IsNullOrEmpty(registerEmail.text) || string.IsNullOrEmpty(registerPass.text))
    //        {
    //            Debug.Log("Please fill the form");
    //            return;
    //        }

    //        if (!string.Equals(registerPass.text, registerConfirmPass.text))
    //        {
    //            Debug.Log("Confirm Password not matched");
    //            return;
    //        }

    //        _auth.CreateUserWithEmailAndPasswordAsync(registerEmail.text, registerPass.text).ContinueWithOnMainThread(task => {
                
    //            if (task.IsCanceled)
    //            {
    //                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
    //                return;
    //            }

    //            if (task.IsFaulted)
    //            {
    //                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
    //                return;
    //            }

    //            // Firebase user has been created.
    //            FirebaseUser newUser = task.Result;
    //            Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
    //        });
    //    }

    //}
}
