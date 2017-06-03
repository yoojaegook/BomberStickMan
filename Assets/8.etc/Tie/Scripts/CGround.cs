using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGround : MonoBehaviour {

    SpriteRenderer _spriteRenderer;

    float dropItemProbability;
    int[] itemProbability;
    GameObject[] items;

    int total;
       

    GameObject item = null;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }



    public void Init(Sprite sprtie, float dropItemProbability, int[] itemProbability, GameObject[] items, bool first)
    {
        this.dropItemProbability = dropItemProbability;
        this.itemProbability = itemProbability;
        this.items = items;
        total = 0;
        for (int i = 0; i < itemProbability.Length; i++)
        {
            total += itemProbability[i];
        }
        _spriteRenderer.sprite = sprtie;
        if(!first)
        ItemReSetting();
    }

	public void ItemReSetting()
    {
        if(item != null)
        {
            Destroy(item);
            item = null;
        }

        if(Random.Range(0f, 100f) <= dropItemProbability)
        {
            // 아이템 생성
            int itemR = Random.Range(0, total);
            int nextItem = 0;
            for (int i = 0; i < itemProbability.Length; i++)
            {
                nextItem += itemProbability[i];
                if (itemR <= nextItem)
                {
                    // 아이템 생성
                    item = Instantiate(items[i], transform);
                    item.transform.localPosition = items[i].transform.localPosition;
                    break;
                }
                
            }

                
        }


    }

   


}
