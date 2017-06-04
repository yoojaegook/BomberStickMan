using System.Collections;
using UnityEngine;
using System.Collections.Generic; //for List
using System.Linq;//for ToList

public class FindBombScript : MonoBehaviour 
{
    //List<GameObject> bombs = new List<GameObject>();
    GameObject bomb1;
    //GameObject bomb2;

    void Start ()
	{
        bomb1 = GameObject.Find("1_Bomb");
        //bomb2 = GameObject.Find("2_Bomb");
        //bombs = GameObject.FindWithTag("Bomb")< GameObject > ().ToList();
    }
	
	void Update () 
	{

	}

    public void BombOn()
    {
        bomb1.SendMessage("FindHuman");
        //bomb2.SendMessage("FindHuman");
    }
}
