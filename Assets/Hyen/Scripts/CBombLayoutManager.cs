using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CBombLayoutManager : MonoBehaviour {
    public GameObject bombType;
    public Transform dragParent;
    public Transform placementParent;
    public GameObject placementBombGO;

    public GameObject ui;

    public Sprite[] onOff;

    public Text reTry;
    public Text goldText;
    public Text distanceText;
    [Header("Bomb")]
    public CBomb[] bombs;
    public CHuman human;
    [Header("BombBlocks")]
    public CBombBlock[] bombBlockY1;
    public CBombBlock[] bombBlockY2;
    public CBombBlock[] bombBlockY3;
    public CBombBlock[] bombBlockY4;
    public CBombBlock[] bombBlockY5;
    public CBombBlock[] bombBlockY6;
    public CBombBlock[] bombBlockY7;
    public CBombBlock[] bombBlockY8;

    [Header("AreaPUrchase")]
    public Text areaPUrchaseText;
    public Button areaPUrchaseButton;

    public int offX;
    public int offY;

    Area[] areaList;
    
    CBombBlock[][] bombBlockS;
    bool areaUpPossible = true;


    CBomb bombChoice;
    CHuman humanChoice;
    bool isHuman = false;
    List<CBombBlock> choiceBombBlock;
    List<CPlacementBomb> placementBomb;

    bool createAndDrag = false;

    private static CBombLayoutManager instance;

    public static CBombLayoutManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("CBombLayoutManager null");
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        choiceBombBlock = new List<CBombBlock>();
        placementBomb = new List<CPlacementBomb>();
    }
    // Use this for initialization
    void Start()
    {
        bombBlockS = new CBombBlock[8][];
        bombBlockS[0] = bombBlockY1;
        bombBlockS[1] = bombBlockY2;
        bombBlockS[2] = bombBlockY3;
        bombBlockS[3] = bombBlockY4;
        bombBlockS[4] = bombBlockY5;
        bombBlockS[5] = bombBlockY6;
        bombBlockS[6] = bombBlockY7;
        bombBlockS[7] = bombBlockY8;

        areaList = new Area[9];
        areaList[0] = new Area(4, 4);
        areaList[1] = new Area(4, 3);
        areaList[2] = new Area(3, 3);
        areaList[3] = new Area(3, 2);
        areaList[4] = new Area(2, 2);
        areaList[5] = new Area(2, 1);
        areaList[6] = new Area(1, 1);
        areaList[7] = new Area(1, 0);
        areaList[8] = new Area(0, 0);

        Setting();
        AreaButtonSetting();




    }

    public void CreateAndDrag(bool drag)
    {
        createAndDrag = drag;
        for (int i = 0; i < placementBomb.Count; i++)
        {
            placementBomb[i].RaycastCon(!createAndDrag);
        }
    }

    public void AreaButtonSetting()
    {
        int areaLevel = CGameManager.Instance.GetAreaLevel();
        if (areaLevel < 8)
        {
            areaPUrchaseButton.interactable = true;
            areaPUrchaseText.text = areaLevel < 8? ( 8 - areaList[areaLevel+1].GetX()) + " X " +
                (8 - areaList[areaLevel+1].GetY()) + "\n" + (100 * (areaLevel + 1)) + "G" : "MAX";
        }
        else
        {
            areaPUrchaseText.text = "MAX";
            areaPUrchaseButton.interactable = false;
        }
    }

    public void Blasting()
    {
        if (!human.GetIsPlacement()) return;
        List<CCreateBombInfo> cbiList = new List<CCreateBombInfo>();
        for (int i = 0; i < placementBomb.Count; i++)
        {
            
            CCreateBombInfo cbi = new CCreateBombInfo(placementBomb[i].IsHuman()? 8 : 
                placementBomb[i].GetBomb().GetDataBombInfo().DataBomb.GetNumber(),
                placementBomb[i].GetBombDir(), placementBomb[i].GetPos());
            cbiList.Add(cbi);
        }
        CInGameManager.Instance.CreateBomb(cbiList);
        ui.SetActive(false);
        for (int i = 0; i < bombs.Length; i++)
        {
            bombs[i].UseBomb();
        }

    }

    public void AreaUp()
    {
        if (!areaUpPossible) return;
        // 가격 체크부분
        int areaLevel = CGameManager.Instance.GetAreaLevel();
        if (areaLevel >= 8) return;
        if( CGameManager.Instance.Purchase(100 * (areaLevel + 1)))
        {
            if (CGameManager.Instance.GetAreaLevel() < 8)
            CGameManager.Instance.AreaLevelUp();
            AreaButtonSetting();
            ReSetBombBlocks();
            Setting();

        }
        else
        {
            //돈이 부족
        }
    }

    void Setting()
    {
        int areaLevel = CGameManager.Instance.GetAreaLevel();
        areaPUrchaseButton.interactable = true;
        for (int i = 0; i < bombBlockS.GetLength(0); i++)
        {
            for (int j = 0; j < bombBlockS[i].GetLength(0); j++)
            {
                bombBlockS[i][j].Init(i, j, onOff);
                if (j < areaList[areaLevel].GetX() || i < areaList[areaLevel].GetY())
                {
                    bombBlockS[i][j].SetUnLock(true);
                }
                else
                {
                    bombBlockS[i][j].SetUnLock(false);
                }
            }
        }
        for (int i = 0; i < bombs.Length; i++)
        {
            bombs[i].Init(CGameManager.Instance.GetDataBombInfo(i));
        }
        ui.SetActive(true);
        reTry.text = CGameManager.Instance.GetGameNumber() + "/5";
        CGameManager.Instance.SetGoldText(goldText);
    }

   
    public CBombType CreateBombType(Vector2 pos, CBomb bomb)
    {
        isHuman = false;
        GameObject go = Instantiate(bombType, pos, Quaternion.identity, dragParent) as GameObject;
        CBombType bt = go.GetComponent<CBombType>();
        bt.Init(pos, bomb);
        bombChoice = bomb;
        CreateAndDrag(true);
        return bt;
    }
    public CBombType CreateBombType(Vector2 pos, CHuman human)
    {
        isHuman = true;
        GameObject go = Instantiate(bombType, pos, Quaternion.identity, dragParent) as GameObject;
        CBombType bt = go.GetComponent<CBombType>();
        bt.Init(pos, human);
        humanChoice = human;
        CreateAndDrag(true);
        return bt;
    }

    public void ReMoveCreateAndDrag()
    {
        CreateAndDrag(false);
    }

    public void ChoiceBomb(int posX, int posY)
    {
        RemoveChoiceBombBlocks();
        if (!createAndDrag) return;
        if (bombChoice != null)
        {
            for (int i = posX; i < posX + bombChoice.GetCol(); i++)
            {
                if (i >= bombBlockS.GetLength(0))
                {
                    break;
                }
                for (int j = posY; j < posY + bombChoice.GetRow(); j++)
                {
                    if (j >= bombBlockS[i].GetLength(0))
                    {
                        break;
                    }

                    if (bombBlockS[i][j].GetExist())
                    {
                        break;
                    }
                    else if(bombBlockS[i][j].GetUnLock())
                    {
                        break;
                    }
                    else
                    {
                        choiceBombBlock.Add(bombBlockS[i][j]);
                        bombBlockS[i][j].SetChoice(true);
                    }

                }
            }
        }else if (humanChoice != null)
        {
            for (int i = posX; i < posX + humanChoice.GetCol(); i++)
            {
                if (i >= bombBlockS.GetLength(0))
                {
                    break;
                }
                for (int j = posY; j < posY + humanChoice.GetRow(); j++)
                {
                    if (j >= bombBlockS[i].GetLength(0))
                    {
                        break;
                    }

                    if (bombBlockS[i][j].GetExist())
                    {
                        break;
                    }
                    else if(bombBlockS[i][j].GetUnLock())
                    {
                        break;
                    }
                    else
                    {
                        choiceBombBlock.Add(bombBlockS[i][j]);
                        bombBlockS[i][j].SetChoice(true);
                    }

                }
            }
        }
        //Debug.Log(bombChoice.bombName);
        // 배치하기전에 검사
        
        
    }

    public void RemoveChoiceBombBlocks()
    {
        for (int i = 0; i < choiceBombBlock.Count; i++)
        {
            choiceBombBlock[i].SetChoice(false);
        }
        choiceBombBlock.Clear();
    }

    private void ExistChoiceBombBlocks()
    {
        for (int i = 0; i < choiceBombBlock.Count; i++)
        {
            choiceBombBlock[i].SetExist(true);
            choiceBombBlock[i].SetChoice(false);
        }
        choiceBombBlock.Clear();
    }
    
	public void PlacementBomb(Vector3 pos, bool exist, bool unLock)
    {
        if(!isHuman)
        {
            if (bombChoice == null) return;
            if(!exist && !unLock)
            {
                if (choiceBombBlock.Count == (bombChoice.GetRow() * bombChoice.GetCol()))
                {
                    if(bombChoice.NumberDown())
                    {
                        GameObject go = Instantiate(placementBombGO, pos, Quaternion.identity, placementParent);
                        CPlacementBomb pb = go.GetComponent<CPlacementBomb>();
                        pb.init(pos, bombChoice);
                        placementBomb.Add(pb);
                        //Debug.Log("배치");
                        ExistChoiceBombBlocks();
                    }
                    // 배치
                    //placementBomb
                }
                else
                {
                    RemoveChoiceBombBlocks();
                    Debug.Log("배치 안됨 " + choiceBombBlock.Count + " " + bombChoice.GetRow() + " " + bombChoice.GetCol() + " " + bombChoice.name);
                }

            }
            else
            {
                RemoveChoiceBombBlocks();
            }
        }
        else
        {
            if (humanChoice == null) return;
            if (!exist && !unLock)
            {
                if (choiceBombBlock.Count == (humanChoice.GetRow() * humanChoice.GetCol()))
                {
                 
                        GameObject go = Instantiate(placementBombGO, pos, Quaternion.identity, placementParent);
                        CPlacementBomb pb = go.GetComponent<CPlacementBomb>();
                        pb.init(pos, humanChoice);
                        placementBomb.Add(pb);
                    //Debug.Log("배치");
                        humanChoice.PlaceMent();
                        ExistChoiceBombBlocks();

                    // 배치
                    //placementBomb
                }
                else
                {
                    RemoveChoiceBombBlocks();
                    //Debug.Log("배치 안됨 " + choiceBombBlock.Count + " " + bombChoice.GetRow() + " " + bombChoice.GetCol() + " " + bombChoice.name);
                }

            }
            else
            {
                RemoveChoiceBombBlocks();
            }
        }
        areaUpPossible = false;
        areaPUrchaseButton.interactable = false;
        bombChoice = null;
    }

    public void ReSetBombBlocks()
    {
        for (int i = placementBomb.Count-1; i >= 0; i--)
        {
            Destroy(placementBomb[i].gameObject);
        }
        placementBomb.Clear();
        for (int i = 0; i < bombBlockS.Length; i++)
        {
            for (int j = 0; j < bombBlockS[i].Length; j++)
            {
                bombBlockS[i][j].ReSetting();
            }
        }
        for (int i = 0; i < bombs.Length; i++)
        {
            bombs[i].ReSetting();
        }
        areaPUrchaseButton.interactable = true;
        areaUpPossible = true;
        human.ReSetting();
    }

    public void RotBomb()
    {
        for (int i = 0; i < bombs.Length; i++)
        {
            bombs[i].BombRot();
        }
    }

    public class Area
    {
        int x;
        int y;
        public Area(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }
    }

}
