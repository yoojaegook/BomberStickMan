using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPlacementBomb : MonoBehaviour {

    Image image;
    Vector2 oriPos;
    RectTransform rectTransform;
    CBomb bomb;
    CHuman human;
    bool isHuman = false;
    CBomb.BombDir bombDir = CBomb.BombDir.Up;
    private void Awake()
    {
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
        isHuman = false;
        //image.rectTransform.localRotation = bomb.GetDirToRot();

    }
    public void init(Vector3 pos, CHuman human)
    {
        image.sprite = human.GetSprite();
        image.color = Color.black;
        rectTransform.sizeDelta = new Vector2(human.GetRow() * human.GetSize(),
            human.GetCol() * human.GetSize());
        Vector3 setPos = pos;
        setPos.x += (human.GetRow() * 0.5f) * human.GetSize();
        setPos.y -= (human.GetCol() * 0.5f) * human.GetSize();
        transform.position = setPos;
        this.human = human;
        oriPos = pos;
        isHuman = true;
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
}
