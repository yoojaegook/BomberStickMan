using System.Collections;
using UnityEngine;
using System.Collections.Generic; //for List
using System.Linq;//for ToList

public class BombScript : MonoBehaviour 
{
    bool firstBomb = true;

    Transform bombDirPoint = null;
    public bool bombDirOn = true;
    public float basicPower;

    int waponValue;
    float power;

    GameObject humanObject;
    List<Rigidbody2D> _rbody2D = new List<Rigidbody2D>();

    bool floorCheck;
    bool floorCheck2 = true;
    float time;

    void Start ()
	{
        waponValue = int.Parse(gameObject.name.Substring(4, 1));
        //waponValue = 1;
        power = basicPower * waponValue;
        if (bombDirOn)
        {
            bombDirPoint = gameObject.transform.GetChild(0).transform;
        }
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
        for (int i = 0; i < 13; i++)
        {
            GameObject obj = humanObject.transform.GetChild(i).gameObject;
            obj.AddComponent<FloorCheckSgript>();
        }
    }

    public void FindHuman()
    {
        humanObject = GameObject.FindWithTag("Human");
        _rbody2D = humanObject.GetComponentsInChildren<Rigidbody2D>().ToList();

        BombWorking();
    }

    public void BombWorking()
    {
        Vector2 dir = new Vector2();

        if (bombDirOn)
        {
            dir = bombDirPoint.transform.position - gameObject.transform.position;
        }
        else
        {
            dir = humanObject.transform.position - gameObject.transform.position;
        }

        if (humanObject.GetComponent<HumanInfoScript>().firstBomb)
        {
            humanObject.GetComponent<HumanInfoScript>().firstBomb = false;
            floorCheck = true;

            foreach (Rigidbody2D r in _rbody2D)
            {
                r.simulated = true;
                r.AddForceAtPosition(dir.normalized * power, gameObject.transform.position);
            }
        }   
        else
        {
            foreach (Rigidbody2D r in _rbody2D)
            {
                r.velocity += dir.normalized * power * 0.03f;
            }
        }
    }
}
