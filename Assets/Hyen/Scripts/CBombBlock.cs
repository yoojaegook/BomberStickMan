using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CBombBlock : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    int posX;
    int posY;
    Sprite[] onOff;

    bool exist = false;
    bool choice = false;

    Image image;
    bool unLock = false;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void Init(int posX, int posY, Sprite[] onOff)
    {
        this.posX = posX;
        this.posY = posY;
        this.onOff = onOff;
    }

    public void SetUnLock(bool unLock)
    {
        this.unLock = unLock;
        SpriteChange();
    }
    public bool GetUnLock()
    {
        return unLock;
    }


    public void OnDrop(PointerEventData eventData)
    {
        CBombLayoutManager.Instance.PlacementBomb(transform.position, exist, unLock);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("온!!!");
        CBombLayoutManager.Instance.ChoiceBomb(posX, posY);
    }

    public bool GetExist()
    {
        return exist;
    }

    public void SetExist(bool exist)
    {
        this.exist = exist;
        SpriteChange();
    }

    public void SetChoice(bool choice)
    {
        this.choice = choice;
        SpriteChange();
    }

    private void SpriteChange()
    {
        if(unLock)
        {
            image.sprite = onOff[2];
        }
        else if(exist)
        {
            image.sprite = onOff[2];
        }
        else if (choice)
        {
            image.sprite = onOff[1];
        }
        else
        {
            image.sprite = onOff[0];
        }
    }

    public void ReSetting()
    {
        exist = false;
        choice = false;
        SpriteChange();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CBombLayoutManager.Instance.RemoveChoiceBombBlocks();
    }
}
