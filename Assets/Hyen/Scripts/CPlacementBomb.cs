using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPlacementBomb : MonoBehaviour {

    Image image;
    Text numberText;
    Vector2 oriPos;
    RectTransform rectTransform;
    CBomb bomb;
    CHuman human;
    bool isHuman = false;
    CBomb.BombDir bombDir = CBomb.BombDir.Up;
    int number = 0;
    private void Awake()
    {
        numberText = GetComponentInChildren<Text>();
        image = GetComponentInChildren<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void init(Vector3 pos, CBomb bomb)
    {
        image.sprite = bomb.GetSprite();
        rectTransform.sizeDelta = new Vector2(bomb.GetRow() * bomb.GetSize(),
            bomb.GetCol() * bomb.GetSize());
        Vector3 setPos = pos;
        setPos.x += (bomb.GetRow() * 0.5f) * bomb.GetSize();
        setPos.y -= (bomb.GetCol() * 0.5f) * bomb.GetSize();
        transform.position = setPos;
        this.bomb = bomb;
        oriPos = pos;
        bombDir = bomb.GetBombDir();
        if(bombDir == CBomb.BombDir.Left || bombDir == CBomb.BombDir.Right)
        {
            image.rectTransform.sizeDelta = new Vector2(bomb.GetCol() * bomb.GetSize(), 
                bomb.GetRow() * bomb.GetSize());
        }
        else
        {
            image.rectTransform.sizeDelta = rectTransform.sizeDelta;
        }
        image.rectTransform.rotation = bomb.GetBombRotQuater();
        isHuman = false;
        numberText.text = number.ToString();
        //image.rectTransform.localRotation = bomb.GetDirToRot();

    }
    public void init(Vector3 pos, CHuman human)
    {
        image.sprite = human.GetSprite();
        image.color = Color.black;
        rectTransform.sizeDelta = new Vector2(human.GetRow() * human.GetSize(),
            human.GetCol() * human.GetSize());
        image.rectTransform.sizeDelta = rectTransform.sizeDelta;
        Vector3 setPos = pos;
        setPos.x += (human.GetRow() * 0.5f) * human.GetSize();
        setPos.y -= (human.GetCol() * 0.5f) * human.GetSize();
        transform.position = setPos;
        this.human = human;
        oriPos = pos;
        isHuman = true;
        numberText.enabled = false;
        //image.rectTransform.localRotation = bomb.GetDirToRot();

    }
    public Vector2 GetPos()
    {
        return oriPos;
    }
    public CBomb GetBomb()
    {
        return bomb;
    }
    public CBomb.BombDir GetBombDir()
    {
        return bombDir;
    }

    public bool IsHuman()
    {
        return isHuman;
    }

    public void RaycastCon(bool onOff)
    {
        image.raycastTarget = onOff;
    }

    public void NumberUP()
    {
        number++;
        if(number > 10)
        {
            number = 0;
        }
        numberText.text = number.ToString();
    }

    public int GetNumber()
    {
        return number;
    }

}
