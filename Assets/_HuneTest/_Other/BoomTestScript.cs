using System.Collections;
using UnityEngine;
using System.Collections.Generic; //for List
using System.Linq;//for ToList

public class BoomTestScript : MonoBehaviour 
{
    int bombCount;

    float _forceSpeed;

    GameObject _doll;
    List<Rigidbody2D> _rbody2D = new List<Rigidbody2D>();

    public bool dirOn = true;
    bool firstBomb = true;

    List<GameObject> bombObjects = new List<GameObject>();
    List<Transform> bombDirs = new List<Transform>();

    bool floorCheck ;
    bool floorCheck2 = true;
    float time;

    int testcount;

    void Start ()
	{
        _doll = GameObject.Find("2D ragdoll front view");
        //_rbody2D = _doll.GetComponentsInChildren<Rigidbody2D>().ToList();

        bombObjects = GameObject.FindGameObjectsWithTag("Bomb").ToList();
        print(bombObjects.Count);
        for (int i = 0; i < 4; i++)
        {
            print("aaa");
            bombDirs[i] = bombObjects[i].transform.GetChild(0).transform;
        }

        _forceSpeed = 200;
    }
	
	void Update () 
	{
        if (floorCheck)
        {
            if (floorCheck2)
            {
                time += Time.deltaTime;

                if (time > 0.5)
                {
                    time = 0;
                    floorCheck = false;
                    floorCheck2 = false;

                    AddCheck();
                }
            }
        }
	}

    void AddCheck()
    {
        testcount = testcount + 1;
        print(gameObject.name + "_" + testcount);
        for (int i = 0; i < 13; i++)
        {
            GameObject obj = _doll.transform.GetChild(i).gameObject;
            obj.AddComponent<FloorCheckSgript>();
        }
    }

    public void BoomTest()
    {
        //GameObject testBox = GameObject.Find("TestBox");
        //Rigidbody2D testBoxRig = testBox.GetComponent<Rigidbody2D>();
        //testBoxRig.AddForce((testBox.transform.position - boomObject.transform.position) * 100);

        floorCheck = true;
        Vector2[] dirs = new Vector2[bombObjects.Count];

        if (dirOn)
        {
            for (int i = 0; i < bombObjects.Count; i++)
            {
                dirs[i] = bombDirs[i].transform.position - bombObjects[i].transform.position;
            }
        }
        else
        {
            for (int i = 0; i < bombObjects.Count; i++)
            {
                dirs[i] = _doll.transform.position - bombObjects[i].transform.position;
            }
        }

        if (firstBomb)
        {
            firstBomb = false;
            for (int i = 0; i < bombObjects.Count; i++)
            {
                print(bombCount);
                bombCount = bombCount + 1;

                foreach (Rigidbody2D r in _rbody2D)
                {
                    r.simulated = true;
                    r.AddForceAtPosition(dirs[i].normalized * _forceSpeed, bombObjects[i].transform.position);
                }
            }
            
        }
        else
        {
            for (int i = 0; i < bombObjects.Count; i++)
            {
                foreach (Rigidbody2D r in _rbody2D)
                {
                    r.velocity += dirs[i].normalized * _forceSpeed * 0.03f;
                }
            }
        }
    }
}
