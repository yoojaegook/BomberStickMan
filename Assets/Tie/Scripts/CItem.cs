using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItem : MonoBehaviour {

    public int itemNumber;
    public GameObject effect;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Body")
        {
            Debug.Log("아아팀 획득" + itemNumber);
            ItemManager.Instance.CheckItem(itemNumber);
            ItemManager.Instance.SetItemActive(itemNumber);

            //이펙트
            Destroy(Instantiate(effect, transform.position, Quaternion.identity), 5f);

            Destroy(gameObject);
        }
    }

    //ItemManager
    //public CFirstGetItem firstGetItme;

    //public void CheckItem(int itemNumber)
    //{
    //    if(!true)
    //    {
    //        firstGetItme.FirstGetItem(GetSprite(itmeNumber));
    //    }

    //}

}
