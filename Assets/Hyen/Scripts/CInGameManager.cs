using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInGameManager : MonoBehaviour {
    private static CInGameManager instance;

    public GameObject devNext;
    public GameObject[] bombObject;

    List<GameObject> createBombs;
    Transform createBombParent;

    public static CInGameManager Instance
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
        createBombs = new List<GameObject>();
    }
    private void Start()
    {
        devNext.SetActive(false);
        
    }
    public void NextGame()
    {
        CGameManager.Instance.AddGold(500);
        CGameManager.Instance.NextGame();
    }

    public void CreateBomb(List<CCreateBombInfo> createBombInfo)
    {
        
        for (int i = 0; i < createBombInfo.Count; i++)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3( createBombInfo[i].GetBombPos().x, createBombInfo[i].GetBombPos().y
                ,-Camera.main.transform.position.z));
            
            //Debug.Log(i+"변경 전 : " + createBombInfo[i].GetBombPos());
            //Debug.Log(i+"변경 후 : " + pos);
            GameObject go = Instantiate(bombObject[createBombInfo[i].GetBombNumber()],
                pos,
                SetRot(createBombInfo[i].GetBombDir()), createBombParent);
            createBombs.Add(go);
        }
        devNext.SetActive(true);
        //Vector3 poss = Camera.main.ScreenToWorldPoint(new Vector3(500f, 500f, 10f));
        //Debug.Log(10 + "변경 전 : " + new Vector3(500, 500, 10f));
        //Debug.Log(10 + "변경 후 : " + poss);
        StartCoroutine(WiatBlasting());
    }

    IEnumerator WiatBlasting()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < createBombs.Count; i++)
        {
            createBombs[i].SendMessage("FindHuman", SendMessageOptions.DontRequireReceiver);
        }
    }


    private Quaternion SetRot(CBomb.BombDir bombDir)
    {
        Quaternion qu = Quaternion.identity;
        switch (bombDir)
        {
            case CBomb.BombDir.Up:
                qu = Quaternion.identity;
                break;
            case CBomb.BombDir.Right:
                qu = Quaternion.Euler(0f, 0f, -90f);
                break;
            case CBomb.BombDir.Down:
                qu = Quaternion.Euler(0f, 0f, 180f);
                break;
            case CBomb.BombDir.Left:
                qu = Quaternion.Euler(0f, 0f, 90f);
                break;

        }
        return qu;
    }
}
