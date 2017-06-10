using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
 
public class CAdmobManager : MonoBehaviour
{
    public static CAdmobManager instance = null;
    public string android_banner_id;
    public string ios_banner_id;
 
    public string android_interstitial_id;
    public string ios_interstitial_id;
    
	public string android_Video_id;
    public string ios_Video_id;
 
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardBasedVideoAd videoAd;

    // === 내부 파라미터 ======================================
    string sceneName = "non";

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        videoAd = RewardBasedVideoAd.Instance;
        RequestInterstitialAd();
		RequestVideoAd();
    }
    void Update()
    {
        if("FirstTitleScene" == SceneManager.GetActiveScene().name)
        return;
        if (sceneName != SceneManager.GetActiveScene().name)
        {
            if(bannerView !=null)
            bannerView.Destroy();
            sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "TitleScene")
            {
                RequestBannerAd(true);
            }else
            {
                RequestBannerAd(false);
            }
            ShowBannerAd();
        }
    }
    public void RequestBannerAd(bool bottom)
    {
        string adUnitId = string.Empty;

#if UNITY_ANDROID
        adUnitId = android_banner_id;
#elif UNITY_IOS
        adUnitId = ios_banner_id;
#endif
		AdSize adSize = new AdSize(320,100);
        bannerView = new BannerView(adUnitId, adSize, bottom?AdPosition.TopLeft:AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }
#region InterstitialAd
    private void RequestInterstitialAd()
    {
        string adUnitId = string.Empty;

#if UNITY_ANDROID
        adUnitId = android_interstitial_id;
#elif UNITY_IOS
        adUnitId = ios_interstitial_id;
#endif

        interstitialAd = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();

        interstitialAd.LoadAd(request);

        interstitialAd.OnAdOpening += HandleOnInterstitialAdClosed;
        interstitialAd.OnAdClosed += HandleOnInterstitialAdClosed;
    }

    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        print("HandleOnInterstitialAdClosed event received.");

        interstitialAd.Destroy();

        RequestInterstitialAd();
    }
    public void HandleOnInterstitialAdOpening(object sender, EventArgs args)
    {
        print("HandleOnInterstitialAdOpening event received.");
    }

#endregion
#region VideoAd   
    private void RequestVideoAd()
    {
        string adUnitId = string.Empty;

#if UNITY_ANDROID
        adUnitId = android_Video_id;
#elif UNITY_IOS
        adUnitId = ios_Video_id;
#endif
        videoAd.LoadAd(new AdRequest.Builder().Build(),adUnitId);

		videoAd.OnAdRewarded += HandleOnVideoAdReward;
        videoAd.OnAdClosed += HandleOnVideoAdClosed;
        videoAd.OnAdOpening += HandleOnVideoAdOpening;
        videoAd.OnAdStarted += HandleOnVideoAdStarted;
        videoAd.OnAdFailedToLoad += HandleOnVideoAdFailedToLoad;
        videoAd.OnAdLeavingApplication += HandleOnVideoAdLeavOnAdLeavingApplication;
    }

    private void HandleOnVideoAdLeavOnAdLeavingApplication(object sender, EventArgs e)
    {
        Debug.Log("HandleOnVideoAdLeavOnAdLeavingApplication");
    }

    private void HandleOnVideoAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.Log("HandleOnVideoAdFailedToLoad");
    }

    private void HandleOnVideoAdStarted(object sender, EventArgs e)
    {
        //오디오 끄기 
        AppSound.instance.Mute();
        Debug.Log("HandleOnVideoAdStarted");
    }

    private void HandleOnVideoAdOpening(object sender, EventArgs e)
    {
        Debug.Log("HandleOnVideoAdOpening");
    }


	public void HandleOnVideoAdClosed(object sender, EventArgs args)
    {
        AppSound.instance.Unmute();
        Debug.Log("한배보상");
        RequestVideoAd();
    }
	public void HandleOnVideoAdReward(object sender, EventArgs args)
    {
        AppSound.instance.Unmute();
		Debug.Log("두배보상");
        RequestVideoAd();
    }
#endregion
    public void ShowBannerAd()
    {
        bannerView.Show();
    }
    public void ShowVideoAd()
    {
		if (videoAd.IsLoaded())
		{
            videoAd.Show();
		}
    }

    public void ShowInitAd()
    {
        if (!interstitialAd.IsLoaded())
        {
            RequestInterstitialAd();
            return;
        }
        interstitialAd.Show();
    }
}

