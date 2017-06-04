using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CResultManager : MonoBehaviour {

    public Text maximumDistanceText;
    public Text thisGameMaximumDistanceText;
    public Text gainGoldText;
    public Text viewAdsText;

    private void Start()
    {
        maximumDistanceText.text = CGameManager.Instance.MaximumDistence.ToString() + "M";
        thisGameMaximumDistanceText.text = CGameManager.Instance.ThisGameMaximumDistence.ToString() + "M";
        gainGoldText.text = CGameManager.Instance.GetGainGold().ToString() +"G";
        viewAdsText.text = "1000G";
    }

    public void ShowAds()
    {

    }
    public void ReStart()
    {
        CGameManager.Instance.ThisGameInit();
        CGameManager.Instance.LoadScene("TitleScene");
    }
}
