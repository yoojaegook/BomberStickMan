using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;
using System.Collections;

public class CGooglePlayServiceManager : MonoBehaviour {

	//--------parameter----------
    public static CGooglePlayServiceManager instane = null;
	public Text _messageText;



	
	//구글 플레이 게임즈 인증 요청
	public void GoolePlayActivate()
	{
// #if UNITY_ANDROID
 
//         PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
//             .EnableSavedGames()
//             .Build();
 
//         PlayGamesPlatform.InitializeInstance(config);
 
//         PlayGamesPlatform.DebugLogEnabled = true;
 
//         PlayGamesPlatform.Activate();
 
// #elif UNITY_IOS

//         GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
// #endif

         //구글 플레이 게임즈 인증
        Social.localUser.Authenticate(GooglePlayGamesLoginCallBack);
	}
    //----------Monobehaviour------------
    
    void Awake()
    {
        instane = this;
    }   
    void Start()
    {
        _messageText = GameObject.Find("LogText").GetComponent<Text>();
        GoolePlayActivate();
    }
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	//--------public funtions----------
	public void OnGooglePlayLogoutButtonClick()
	{
		_messageText.text = "";
		GooglePlayDeActive();
	}

	//구글 플레이 게임즈 인증 해제 요청
	public void GooglePlayDeActive()
	{
        PlayGamesPlatform play = (PlayGamesPlatform)Social.Active;

		if (play != null)
		{
			//구글 게임즈 플레이로 인증 했을 경우 인증을 해제함
			play.SignOut();
			_messageText.text = "구글 플레이 로그아웃 성공함";
		}
		else
		{
			_messageText.text = "구글 플레이에 로그인 하지 않았음";
		}
	}
    public void OnGooglePlayGamesLeaderBoardUI()
    {
        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Social.ShowLeaderboardUI();
                    return;
                }
                else
                {
                    return;
                }
            });
        }

#if UNITY_ANDROID
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
#elif UNITY_IOS
        GameCenterPlatform.ShowLeaderboardUI("Score_Meter_Leaderboard", UnityEngine.SocialPlatforms.TimeScope.AllTime);
#endif
    }

	//--------Leaderboard-------------

	public void RefreshLeaderboard(int meter)
	{
#if UNITY_ANDROID
        PlayGamesPlatform.Instance.ReportScore(meter, CGooglePlayServiceDefine.leaderboard_fly_away, LeaderBoardScoreSetCallback);
#elif UNITY_IOS
        Social.ReportScore(meter, "Leaderboard_ID", LeaderBoardScoreSetCallback);
#endif

    }


    //-------Achievement--------------

    public void OnGooglePlayGamesAchievementUI()
    {
        //구글업적 게시판 생성
        Social.ShowAchievementsUI();
    }
	public void Achievement_BOMB()
	{
#if UNITY_ANDROID
            PlayGamesPlatform.Instance.ReportProgress(CGooglePlayServiceDefine.achievement_demolitionist, 100f, AchievementSetCallback);
#elif UNITY_IOS
        Social.ReportProgress("BOMB", 100f, AchievementSetCallback);
#endif
    }
    public void Achievement_Grenade()
    {
#if UNITY_ANDROID
            PlayGamesPlatform.Instance.ReportProgress(CGooglePlayServiceDefine.achievement_grenade_unlocked, 100f, AchievementSetCallback);
#elif UNITY_IOS
        Social.ReportProgress("Grenade", 100f, AchievementSetCallback);
#endif
    }
    public void Achievement_Dynamite()
    {
#if UNITY_ANDROID
            PlayGamesPlatform.Instance.ReportProgress(CGooglePlayServiceDefine.achievement_dynamite_expert, 100f, AchievementSetCallback);
#elif UNITY_IOS
        Social.ReportProgress("Dynamite", 100f, AchievementSetCallback);
#endif
    }
    public void Achievement_TNT()
    {
#if UNITY_ANDROID
            PlayGamesPlatform.Instance.ReportProgress(CGooglePlayServiceDefine.achievement_scientist, 100f, AchievementSetCallback);
#elif UNITY_IOS
        Social.ReportProgress("TNT", 100f, AchievementSetCallback);
#endif
    }
    public void Achievement_High_bomb()
    {
#if UNITY_ANDROID
            PlayGamesPlatform.Instance.ReportProgress(CGooglePlayServiceDefine.achievement_explosion, 100f, AchievementSetCallback);
#elif UNITY_IOS
        Social.ReportProgress("High_bomb", 100f, AchievementSetCallback);
#endif
    }
    public void Achievement_C4()
    {
#if UNITY_ANDROID
            PlayGamesPlatform.Instance.ReportProgress(CGooglePlayServiceDefine.achievement_big_picture, 100f, AchievementSetCallback);
#elif UNITY_IOS
        Social.ReportProgress("C4", 100f, AchievementSetCallback);
#endif
    }
    public void Achievement_MOBA()
    {
#if UNITY_ANDROID
            PlayGamesPlatform.Instance.ReportProgress(CGooglePlayServiceDefine.achievement_mother_of_bomb, 100f, AchievementSetCallback);
#elif UNITY_IOS
        Social.ReportProgress("MOBA", 100f, AchievementSetCallback);
#endif
    }
    public void Achievement_Nuclear_bomb()
    {
#if UNITY_ANDROID
            PlayGamesPlatform.Instance.ReportProgress(CGooglePlayServiceDefine.achievement_a_huge_explosion, 100f, AchievementSetCallback);
#elif UNITY_IOS
        Social.ReportProgress("Nuclear_bomb", 100f, AchievementSetCallback);
#endif
    }


#region CallBackFuntions

    void LeaderBoardScoreSetCallback(bool result)
    {
        if (result)
        {

        }
        else
        {

        }
    }
    void GooglePlayGamesLoginCallBack(bool result)
    {
        if (result)
        {
            _messageText.text = "로그인 성공함 +" + Social.localUser.id.ToString();
        }
        else
        {
            //구글 플레이 계정 로그인 실패
            _messageText.text = "로그인 실패함";
        }
    }

    void AchievementSetCallback(bool result)
    {
        if (result)
        {
            _messageText.text = "업적 달성";
            Social.CreateAchievement();
        }
        else
        {
            _messageText.text = "업적 실패";
        }
    }
#endregion
}
