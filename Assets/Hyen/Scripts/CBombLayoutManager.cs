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

    CBombBlock[][] bombBlockS;

    CBomb bombChoice;
    CHuman humanChoice;
    bool isHuman = false;
    List<CBombBlock> choiceBombBlock;
    List<CPlacementBomb> placementBomb;
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

        for (int i = 0; i < bombBlockS.GetLength(0); i++)
        {
            for (int j = 0; j < bombBlockS[i].GetLength(0); j++)
            {
                bombBlockS[i][j].Init(i, j, onOff);
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

   
    public CBombType CreateBombType(Vector2 pos, CBomb bomb)
    {
        isHuman = false;
        GameObject go = Instantiate(bombType, pos, Quaternion.identity, dragParent) as GameObject;
        CBombType bt = go.GetComponent<CBombType>();
        bt.Init(pos, bomb);
        bombChoice = bomb;
        return bt;
    }
    public CBombType CreateBombType(Vector2 pos, CHuman human)
    {
        isHuman = true;
        GameObject go = Instantiate(bombType, pos, Quaternion.identity, dragParent) as GameObject;
        CBombType bt = go.GetComponent<CBombType>();
        bt.Init(pos, human);
        humanChoice = human;
        return bt;
    }

    public void ChoiceBomb(int posX, int posY)
    {
        RemoveChoiceBombBlocks();
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
    
	public void PlacementBomb(Vector3 pos, bool exist)
    {
        if(!isHuman)
        {
            if (bombChoice == null) return;
            if(!exist)
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
            if (!exist)
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
        human.ReSetting();
    }

    public void RotBomb()
    {
        for (int i = 0; i < bombs.Length; i++)
        {
            bombs[i].BombRot();
        }
    }
}
