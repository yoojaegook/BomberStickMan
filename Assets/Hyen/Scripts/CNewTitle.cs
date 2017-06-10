using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CNewTitle : MonoBehaviour {

    public GameObject touch;
	// Use this for initialization
	void Start () {
        StartCoroutine(OnOffCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
		// if(Input.anyKey)
        // {
        //     CGameManager.Instance.LoadScene("InGameScene");
        // }
	}

    IEnumerator OnOffCoroutine()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.5f);
        while(true)
        {
            yield return wfs;
            touch.SetActive(false);
            yield return wfs;
            touch.SetActive(true);
        }
    }
    public void TurnSceneInGame()
    {
        CGameManager.Instance.LoadScene("InGameScene");
    }

// test
    public void Init()
    {
        CAdmobManager.instance.ShowInitAd();
    }
    public void Video()
    {
        CAdmobManager.instance.ShowVideoAd();
    }
    public void Achive_1()
    {
        CGooglePlayServiceManager.instane.Achievement_BOMB();
    }
    public void OpenAchive()
    {
        CGooglePlayServiceManager.instane.OnGooglePlayGamesLeaderBoardUI();
    }
    public void OpenLeaderBoard()
    {
        CGooglePlayServiceManager.instane.OnGooglePlayGamesAchievementUI();
    }
}
