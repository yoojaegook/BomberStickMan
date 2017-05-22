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
    public Transform groundParent;

    int skyNumber = 0;
    int groundNumber = 0;

    public int skyStartCreate;
    public int groundStaretCreate;

    public float createRange;

    List<CGround> groundList;

    public Sprite[] groundSprites;

    public Transform player;


    [Range(0f, 100f)]
    public float dropItemProbability;

    public int[] itemProbability;
    public GameObject[] items;

    private void Awake()
    {
        groundList = new List<CGround>();
    }
    
    // Use this for initialization
    void Start () {
      
        for (int i = 0; i < skyStartCreate; i++)
        {
            Sky();
        }
        for (int i = 0; i < groundStaretCreate; i++)
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

    public void ItemReSetting()
    {
        for (int i = 0; i < groundList.Count; i++)
        {
            groundList[i].ItemReSetting();
        }
    }
    public void Sky()
    {
        GameObject ob;
        Vector3 pos;
        ob = Instantiate(skyPrefabs, skyParent);
        pos.x = skyStartX + skySize * skyNumber;
        pos.y = pos.z = 0f;
        ob.transform.localPosition = pos;
        skyNumber++;
        //Debug.Log("Sky 생성");
    }
    public void Ground(bool first)
    {
        GameObject ob;
        Vector3 pos;
        ob = Instantiate(groundPrefabs, groundParent);
        pos.x = groundStartX + groundSize * groundNumber;
        pos.y = pos.z = 0f;
        ob.transform.localPosition = pos;
        groundNumber++;
        CGround ground = ob.GetComponent<CGround>();
        ground.Init(groundSprites[Random.Range(0, 3)], dropItemProbability, itemProbability, items, first);
        groundList.Add(ground);
        //Debug.Log("Ground 생성");
    }
}
