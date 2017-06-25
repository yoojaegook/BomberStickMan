using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCreateMapManager : MonoBehaviour {

    public const float skySize = 10f;
    public const float groundSize = 2.56f;

    public const float skyStartX = -40f;
    public const float groundStartX = -40f;

    public GameObject skyPrefabs;
    public GameObject groundPrefabs;

    public Transform skyParent;
    public Transform waitSkyParent;
    public Transform groundParent;
    public Transform waitGroundParent;

    int skyNumber = 0;
    int groundNumber = 0;

    public int skyStartCreate;
    public int groundStartCreate;

    public float createRange;

    List<CGround> useGroundList;
    List<CGround> waitGroundList;
    List<GameObject> useSkyList;
    List<GameObject> waitSkyList;
    public Sprite[] groundSprites;

    public Transform player;


    [Range(0f, 100f)]
    public float dropItemProbability;

    public int[] itemProbability;
    public GameObject[] items;

    private void Awake()
    {
        useGroundList = new List<CGround>();
        waitGroundList = new List<CGround>();
        useSkyList = new List<GameObject>();
        waitSkyList = new List<GameObject>();
    }
    
    // Use this for initialization
    void Start () {

        //CreateSky();
        StartCreate();
        
        for (int i = 0; i < skyStartCreate; i++)
        {
            Sky();
        }
        for (int i = 0; i < groundStartCreate; i++)
        {
            Ground(true);

        }
	}
	
	// Update is called once per frame
	void Update () {
		if((skySize * skyNumber) + skyStartX < player.position.x + createRange)
        {
            Sky();
        }
        if ((groundSize * groundNumber) + groundStartX < player.position.x + createRange)
        {
            Ground(false);
        }
        
	}

    //public void ItemReSetting()
    //{
    //    for (int i = 0; i < groundList.Count; i++)
    //    {
    //        groundList[i].ItemReSetting();
    //    }
    //}

    public void StartCreate()
    {
        for (int i = 0; i < skyStartCreate; i++)
        {
            CreateSky();
        }
        for (int i = 0; i < groundNumber; i++)
        {
            CreateGround();
        }
    }

    public void CreateSky()
    {
        GameObject go = Instantiate(skyPrefabs, Vector3.zero, Quaternion.identity, waitSkyParent);
        go.SetActive(false);
        waitSkyList.Add(go);
    }

    public void Sky()
    {
        GameObject ob;
        Vector3 pos;
        if(waitSkyList.Count > 0)
        {
            ob = waitSkyList[0];
            ob.SetActive(true);
            waitSkyList.Remove(ob);
            useSkyList.Add(ob);
            ob.transform.SetParent(skyParent);
        }
        else
        {
            ob = Instantiate(skyPrefabs, Vector3.zero, Quaternion.identity, skyParent);
            useSkyList.Add(ob);
        }
        ob = Instantiate(skyPrefabs, skyParent);
        pos.x = skyStartX + skySize * skyNumber;
        pos.y = pos.z = 0f;
        ob.transform.localPosition = pos;
        skyNumber++;
        //Debug.Log("Sky 생성");
    }
    public void WaitToSky(GameObject waitSky)
    {
        waitSky.SetActive(false);
        waitSky.transform.position = Vector3.zero;
        waitSky.transform.SetParent(waitSkyParent);
        if(useSkyList.Contains(waitSky))
        {
            useSkyList.Remove(waitSky);
        }
        waitSkyList.Add(waitSky);
    }
    public void CreateGround()
    {
        GameObject go = Instantiate(groundPrefabs, Vector3.zero, Quaternion.identity, waitGroundParent);
        CGround gr = go.GetComponent<CGround>();
        go.SetActive(false);
        waitGroundList.Add(gr);
    }
    public void Ground(bool first)
    {
        //GameObject ob;
        //Vector3 pos;
        //ob = Instantiate(groundPrefabs, groundParent);
        //ob.transform.localPosition = pos;
        //groundNumber++;
        //CGround ground = ob.GetComponent<CGround>();
        //ground.Init(groundSprites[Random.Range(0, 3)], dropItemProbability, itemProbability, items, first);
        //groundList.Add(ground);
        //Debug.Log("Ground 생성");
        CGround gr;
        Vector3 pos;
        if (waitGroundList.Count > 0)
        {
            gr = waitGroundList[0];
            gr.gameObject.SetActive(true);
            waitGroundList.Remove(gr);
            useGroundList.Add(gr);
            gr.transform.SetParent(groundParent);
            // ground 기능 실행
        }
        else
        {
            GameObject go = Instantiate(groundPrefabs, Vector3.zero, Quaternion.identity, groundParent) as GameObject;
            gr = go.GetComponent<CGround>();
            useGroundList.Add(gr);
        }
        gr.Init(groundSprites[Random.Range(0, 3)], dropItemProbability, itemProbability, items, first);
        pos.x = groundStartX + groundSize * groundNumber;
        pos.y = pos.z = 0f;
        gr.transform.localPosition = pos;
        groundNumber++;
    }

    public void WaitToGround(GameObject waitGroundGo)
    {
        CGround waitGround = waitGroundGo.GetComponent<CGround>();
        waitGround.gameObject.SetActive(false);
        waitGround.transform.position = Vector3.zero;
        waitGround.transform.SetParent(waitGroundParent);
        if(useGroundList.Contains(waitGround))
        {
            useGroundList.Remove(waitGround);
        }
        waitGroundList.Add(waitGround);
    }
}
