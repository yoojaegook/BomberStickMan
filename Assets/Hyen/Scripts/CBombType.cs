using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CBombType : MonoBehaviour {


    Image bombImage;
    RectTransform rectTransform;

    CBomb bomb;
    CHuman human;
    float size = 62.5f;
    private void Awake()
    {
        bombImage = GetComponentInChildren<Image>();
        rectTransform = GetComponent<RectTransform>();
    }
    public void Init(Vector3 pos, CBomb bomb)
    {
        this.bomb = bomb;
        bombImage.sprite = bomb.GetSprite();
        rectTransform.sizeDelta = new Vector2(bomb.GetRow() * size, bomb.GetCol() * size);
        //bombImage.rectTransform.localRotation = bomb.GetDirToRot();
    }
    public void Init(Vector3 pos, CHuman human)
    {
        this.human = human;
        bombImage.sprite = this.human.GetSprite();
        bombImage.color = Color.black;
        rectTransform.sizeDelta = new Vector2(this.human.GetRow() * size, this.human.GetCol() * size);
        //bombImage.rectTransform.localRotation = bomb.GetDirToRot();
    }
    public Sprite GetBombImg()
    {
        return bombImage.sprite;
    }

    public CBomb GetBomb()
    {
        return bomb;
    }

    public void SetRectTransform(Vector3 pos)
    {
        Vector3 setPos = pos;
        if(bomb != null)
        {
            setPos.x += (bomb.GetRow() - 1) * (size * 0.5f);
            setPos.y -= (bomb.GetCol() - 1) * (size * 0.5f);
        }
        else if (human != null)
        {
            setPos.x += (human.GetRow() - 1) * (size * 0.5f);
            setPos.y -= (human.GetCol() - 1) * (size * 0.5f);
        }
        //Debug.Log(bomb.GetCol() + " 가로 세로 " + bomb.GetRow() + " 이름 " + bomb.bombName); 
        rectTransform.position = setPos;
    }



}
