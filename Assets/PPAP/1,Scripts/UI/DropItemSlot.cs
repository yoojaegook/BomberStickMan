using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

public class DropItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    void Awake()
    {
    }

    public void OnDrop(PointerEventData data)
    {
        Debug.Log("ondrop");
        Vector3 dropPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject go = Instantiate(CBombManager.Instance.GetPreFab(ItemManager.Instance.choosedData.INDEX)
                        , new Vector3(dropPosition.x, dropPosition.y, 0.0f)
                        , ItemManager.Instance._image.transform.rotation) as GameObject;
        go.SendMessage("Init",ItemManager.Instance.choosedData);
        CBombManager.Instance.AddBomb(go);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("Enterdrop");
    }

    public void OnPointerExit(PointerEventData data)
    {
        Debug.Log("ExitDrop");
    }
}
