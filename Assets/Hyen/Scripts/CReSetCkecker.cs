using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CReSetCkecker : MonoBehaviour {

    public CCreateMapManager createMapManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Sky")
        {
            createMapManager.WaitToSky(collision.gameObject);
        }
        if(collision.tag == "Ground")
        {
            createMapManager.WaitToGround(collision.gameObject);
        }
    }


}
