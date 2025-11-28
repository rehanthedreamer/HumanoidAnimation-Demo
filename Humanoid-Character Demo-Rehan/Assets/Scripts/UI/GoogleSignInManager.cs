using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Google;
using System.Threading.Tasks;
using Firebase.Auth;
using UnityEngine.Networking;
using TMPro;
using System;
using UnityEditor;

public class GoogleSignInManager : MonoBehaviour
{
    public string GoogleAPI = "917697126732-u0ek3gd6vb84bj7a9plr8fugmilkkrjn.apps.googleusercontent.com";
    private GoogleSignInConfiguration configuration;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    [SerializeField] TMP_Text userName, userEmail;
    [SerializeField] Image userProfile;
    string imageURL;
    bool isGoogleSignInInit = false;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Button appleSignInBtn;
    [SerializeField] RectTransform humanoidUIPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnEnable()
    {
        appleSignInBtn.onClick.AddListener(SignInWithGoogle);
    }
    void OnDisable()
    {
          appleSignInBtn.onClick.RemoveListener(SignInWithGoogle);
    }

    void Start()
    {
      InitFirebase();
    }

    void InitFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }



    public void SignInWithGoogle()
    {
        // on Editor
       #if UNITY_EDITOR
        Debug.Log("Google Sign-In does not work in Unity Editor. Using temporary login.");

        canvasGroup.DOFade(0, .1f);
        humanoidUIPanel.DOScale(Vector3.one, .2f);

        // Fake user
        userName.text = "Editor User";
        userEmail.text = "editor@test.com";

        return;
        #endif
        // On Device
        #if UNITY_ANDROID
       if(!isGoogleSignInInit)
        {
            GoogleSignIn.Configuration = new GoogleSignInConfiguration
            {
                RequestIdToken = true,
                WebClientId = GoogleAPI,
                RequestEmail = true
            };
            isGoogleSignInInit = true;
        }
        GoogleSignIn.Configuration = new GoogleSignInConfiguration
        {
            RequestIdToken = true,
            WebClientId = GoogleAPI
        };
        GoogleSignIn.Configuration.RequestEmail = true;

        Task<GoogleSignInUser> signInUser = GoogleSignIn.DefaultInstance.SignIn();
        TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser>();
        signInUser.ContinueWith(task =>
        {
            if(task.IsCanceled)
            {
                signInCompleted.SetCanceled();
                Debug.Log("Canclled");
                TostMsgManager.instance.ShowToatMsg("Canclled");
            }else if(task.IsFaulted)
            {
                signInCompleted.SetException(task.Exception);
                  Debug.Log("Fauled "+ task.Exception);
                   TostMsgManager.instance.ShowToatMsg("Fauled "+ task.Exception);
            }
            else
            {
               Credential credential = GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)task).Result.IdToken, null);
               auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
               {
                  if(authTask.IsCanceled)
                   {
                       signInCompleted.SetCanceled();
                        TostMsgManager.instance.ShowToatMsg("Canclled ");
                   }else if(authTask.IsFaulted)
                   {
                       signInCompleted.SetException(authTask.Exception);
                        TostMsgManager.instance.ShowToatMsg("Fauled "+ task.Exception);
                   }else
                   {
                       signInCompleted.SetResult(((Task<FirebaseUser>)authTask).Result);
                       Debug.Log("Success");
                       TostMsgManager.instance.ShowToatMsg("Success ");
                       user = auth.CurrentUser;
                       userName.text = user.DisplayName;
                       userEmail.text = user.Email;
                       canvasGroup.DOFade(0,.1f);
                       humanoidUIPanel.DOScale(Vector3.one,.2f);
                   }
               });

            }
        });
        #endif

    }

  
 
}
