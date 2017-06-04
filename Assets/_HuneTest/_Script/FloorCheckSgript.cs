using System.Collections;
using UnityEngine;

public class FloorCheckSgript : MonoBehaviour 
{
    GameObject parentObject;
    string floorName;

	void Start ()
	{
        parentObject = gameObject.transform.parent.gameObject;
        floorName = parentObject.GetComponent<HumanInfoScript>().floorName;
	}
	
	void Update () 
	{

	}

    void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.transform.name == floorName)
        {
           gameObject.GetComponent<FloorCheckSgript>().enabled = false;
           // touch the floor sendmessage
       }
    }
}
