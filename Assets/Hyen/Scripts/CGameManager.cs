using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CGameManager : MonoBehaviour {

    private static CGameManager instance;

    // 게임 메이저로 넘어가야됨
    CDataBombInfo[] dataBombInfo;
    CDataBomb[] dataBomb;
    public Sprite[] bombImgs1;
    public Sprite[] bombImgs2;
    public Sprite[] bombImgs3;
    public Sprite[] bombImgs4;
    public Sprite[] bombImgs5;
    public Sprite[] bombImgs6;
    public Sprite[] bombImgs7;
    public Sprite[] bombImgs8;

    int gameNumber = 1;

    int maximumDistence = 0;
    int thisGameMaximumDistence = 0;
    int gold = 0;
    int thisGameGold = 0;

    Text goldText;

    public int MaximumDistence { get { return maximumDistence; } set
        { if (maximumDistence <= value) { maximumDistence = value; } } }
    public int ThisGameMaximumDistence { get { return thisGameMaximumDistence; } set { if (thisGameMaximumDistence <= value)
            { thisGameMaximumDistence = value; } } }

    public static CGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("CGameManager null");
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;

        dataBomb = new CDataBomb[8];
        dataBomb[0] = new CDataBomb(0, "Bomb", 0, 0, 10, CDataBomb.PowerDirection.U, 2, 2, bombImgs1);
        dataBomb[1] = new CDataBomb(1, "Grenade", 10, 20, 50, CDataBomb.PowerDirection.A, 2, 3, bombImgs2);
        dataBomb[2] = new CDataBomb(2, "HighBomb", 20, 40, 100, CDataBomb.PowerDirection.D, 4, 2, bombImgs3);
        dataBomb[3] = new CDataBomb(3, "C4", 30, 60, 200, CDataBomb.PowerDirection.U, 3, 4, bombImgs4);
        dataBomb[4] = new CDataBomb(4, "MOBA", 40, 80, 250, CDataBomb.PowerDirection.U, 3, 5, bombImgs5);
        dataBomb[5] = new CDataBomb(5, "NuclearBomb", 50, 100, 300, CDataBomb.PowerDirection.A, 3, 3, bombImgs6);
        dataBomb[6] = new CDataBomb(6, "Dynamite", 60, 120, 350, CDataBomb.PowerDirection.LR, 5, 5, bombImgs7);
        dataBomb[7] = new CDataBomb(7, "TNT", 70, 140, 400, CDataBomb.PowerDirection.U, 5, 6, bombImgs8);

        dataBombInfo = new CDataBombInfo[8];

        // 저장된 데이터...
        dataBombInfo = new CDataBombInfo[8];
        dataBombInfo[0] = new CDataBombInfo(true, 5, dataBomb[0]);
        dataBombInfo[1] = new CDataBombInfo(false, 0, dataBomb[1]);
        dataBombInfo[2] = new CDataBombInfo(false, 0, dataBomb[2]);
        dataBombInfo[3] = new CDataBombInfo(false, 0, dataBomb[3]);
        dataBombInfo[4] = new CDataBombInfo(false, 0, dataBomb[4]);
        dataBombInfo[5] = new CDataBombInfo(false, 0, dataBomb[5]);
        dataBombInfo[6] = new CDataBombInfo(false, 0, dataBomb[6]);
        dataBombInfo[7] = new CDataBombInfo(false, 0, dataBomb[7]);
        //Debug.Log(dataBombInfo[0].BombNumber + " 갯수 체크");


    }

    IEnumerator LogoLoading()
    {
        yield return new WaitForSeconds(2f);
        LoadScene("TitleScene");
    }

    public CDataBombInfo GetDataBombInfo(int number)
    {
        return dataBombInfo[number];
    }

    public void BombUnLock(int number)
    {
        //Debug.Log("실행 되었는가");
        dataBombInfo[number].BombUnLock = true;
        dataBombInfo[number].BombNumber = 1;
    }

    public void BombNumberAdd(int number)
    {
        if(dataBombInfo[number].BombUnLock)
        {
            dataBombInfo[number].BombNumber++;
        }
        BombSave();
    }

    public void BombSave()
    {

    }
    public void Load()
    {

    }

    public void NextGame()
    {
        if(gameNumber >= 5)
        {
            // 결과
            LoadScene("Result");
            gameNumber = 1;
        }
        else
        {
            gameNumber++;
            LoadScene("InGameScene");
            // 다음 게임
        }
    }
    
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(LogoLoading());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadScene(string sceneName)
    {
        goldText = null;
        if (sceneName == "InGameScene")
        {
            SceneManager.LoadScene(sceneName);
            SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
            Debug.Log("인게임");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public int GetGameNumber()
    {
        return gameNumber;
    }

    public void ThisGameInit()
    {
        thisGameMaximumDistence = 0;
        thisGameGold = 0;
    }

    public int GetGold()
    {
        return gold;
    }
    public int GetGainGold()
    {
        return thisGameGold;
    }
    public void AddGold(int gold)
    {
        this.gold += gold;
        thisGameGold += gold;
        if(goldText != null)
        {
            goldText.text = gold + "G";
        }
    }
    public bool Purchase(int gold)
    {
        if(this.gold >= gold)
        {
            this.gold -= gold;
            if (goldText != null)
            {
                goldText.text = this.gold + "G";
                //Debug.Log(gold);
            }
            return true;
        }
        return false;
        
    }
    public void SetGoldText(Text goldText)
    {
        this.goldText = goldText;
        if (this.goldText != null)
            this.goldText.text = gold.ToString() + "G";
    }
}
