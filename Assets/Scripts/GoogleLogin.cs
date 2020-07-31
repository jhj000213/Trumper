using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine.UI;

public class GoogleLogin : MonoBehaviour
{
    private static GoogleLogin instance = null;
    public static GoogleLogin Data
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(GoogleLogin)) as GoogleLogin;
                if (instance == null)
                {
                    Debug.Log("no instance");
                }
            }
            return instance;
        }
    }

    [SerializeField] GameObject _ErrorPopup;

    [SerializeField] Text Log;
    [SerializeField] Text[] _RankerName;
    [SerializeField] Text _MyRankText;

    [SerializeField] GameObject _NameInputPopup;
    [SerializeField] Text _InputNameText;

    public DatabaseReference reference;

    string UserName;
    string UserId = null;
    int _MyRank;

    private void Awake()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder()
            .RequestIdToken().RequestServerAuthCode(false).Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(ServerData.Data._FirebaseUrl);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        OnLogin();
#else
        Debug.Log("Login");
#endif
    }
    public void OnLogin()
    {
        UserName = "";
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool bSuccess) =>
            {
                if (bSuccess)
                {
                    Log.text += Social.localUser.userName;
                    UserId = Social.localUser.userName;
                    
                    reference.Child("users").Child(UserId).Child("name").GetValueAsync().ContinueWith(task =>
                    {

                        if (task.IsFaulted)
                        {
                            Log.text += "error\n";
                            _ErrorPopup.SetActive(true);
                        }
                        else if (task.IsCompleted)
                        {
                            DataSnapshot snap = task.Result;
                            if (snap.Value == null)
                                UserName = "";
                            else
                                UserName = snap.Value.ToString();
                            Log.text += UserName + ": name\n";
                            if (UserName == "")
                            {
                                _NameInputPopup.SetActive(true);
                            }
                            else
                                RankingRefresh();
                        }
                    });
                }
                else
                {
                    Debug.Log("Fall");
                    Log.text += "Fail\n";
                    _ErrorPopup.SetActive(true);
                }
            });
        }
    }

    public void SetRank()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        if(UserId!=null)
        {
            int score = StageMng.Data._NowClearStage_Challange;
            reference.Child("users").Child(UserId).Child("score").SetValueAsync(score);
        }
        else
        {
            reference.Child("users").Child("computer").Child("name").SetValueAsync("computer");
            reference.Child("users").Child("computer").Child("score").SetValueAsync("111");
            _ErrorPopup.SetActive(true);
        }
        RankingRefresh();
#endif
    }
    public void RankingRefresh()
    {
        reference.Child("users").OrderByChild("score").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Log.text += "error\n";
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                long max = snapshot.ChildrenCount;
                for (int i = 0; i < 3; i++)
                    _RankerName[i].text = "";
                foreach (var item in snapshot.Children)
                {
                    max--;
                    if (UserName == item.Child("name").Value.ToString())
                    {
                        _MyRank = (int)(max+1);
                        _MyRankText.text = "내 순위 : " + _MyRank + " / " + (int)snapshot.ChildrenCount;
                    }
                    if(max<3)
                    {
                        _RankerName[max].text = item.Child("name").Value + " : " + item.Child("score").Value;
                    }
                    Log.text += item.Child("name").Value +" / "+ item.Child("score").Value+"\n";
                }
            }
        });
    }

    public void SetNickName()
    {
        UserName = _InputNameText.text;
        reference.Child("users").Child(UserId).Child("name").SetValueAsync(UserName);
        reference.Child("users").Child(UserId).Child("score").SetValueAsync(0);
        _NameInputPopup.SetActive(false);
        RankingRefresh();
    }
}
