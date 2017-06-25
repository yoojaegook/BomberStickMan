using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class CBomb : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    float size = 62.5f;
    public Image image;
    public GameObject lockImg;
    public Text purchaseText;
    public Text noumberText;
    CBombType dragBombType;
    RectTransform rectTransform;
    CDataBombInfo dataBombInfo;
    int bombNumber = 0;

    public enum BombDir
    {
        Up,
        Right,
        Down,
        Left,
    }
    BombDir bombDir = BombDir.Up;
    bool noPurchase = false;

    public void Init(CDataBombInfo dataBombInfo)
    {
        this.dataBombInfo = dataBombInfo;
        image.sprite = dataBombInfo.DataBomb.GetImg();
        bombNumber = this.dataBombInfo.BombNumber;
        Setting();
    }

    private void Setting()
    {
        if (dataBombInfo.BombUnLock)
        {
            lockImg.SetActive(false);
            purchaseText.text = dataBombInfo.DataBomb.GetCost().ToString() + "G";
            if (dataBombInfo.DataBomb.GetNumber() == 0)
                noPurchase = true;
            //purchaseText.text = "구매";
            noumberText.gameObject.SetActive(true);
            //Debug.Log(dataBombInfo.DataBomb.GetNumber());
            noumberText.text = bombNumber.ToString();
        }
        else
        {
            lockImg.SetActive(true);
            purchaseText.text = dataBombInfo.DataBomb.GetUnLockingCost().ToString() + "G";
            //purchaseText.text = "잠금 해제";
            noumberText.gameObject.SetActive(false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        if (bombNumber <= 0) return;
        dragBombType = CBombLayoutManager.Instance.CreateBombType(transform.position, this);

        //var canvas = FindInParents<Canvas>(gameObject);
        //if (canvas == null)
        //    return;

       
        rectTransform = transform as RectTransform;
        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        if (dragBombType != null)
            SetDraggedPosition(eventData);
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
            rectTransform = eventData.pointerEnter.transform as RectTransform;

        RectTransform rt = dragBombType.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            dragBombType.SetRectTransform(globalMousePos);
            //rt.rotation = rectTransform.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragDelete();
        //Debug.Log("OnEndDrag");
        //if (m_DraggingIcons[eventData.pointerId] != null)
        //    Destroy(m_DraggingIcons[eventData.pointerId]);

        //m_DraggingIcons[eventData.pointerId] = null;
    }

    public void DragDelete()
    {
        if (dragBombType != null)
        {
            //Debug.Log("삭제 실행 체크");
            Destroy(dragBombType.gameObject);
            dragBombType = null;
            CBombLayoutManager.Instance.ReMoveCreateAndDrag();
        }
    }

    public Sprite GetSprite()
    {
        return dataBombInfo.DataBomb.GetImg();
    }
    public BombDir GetBombDir()
    {
        return bombDir;
    }

    public float GetSize()
    {
        return size;
    }
    public int GetRow()
    {
        int toRow = dataBombInfo.DataBomb.GetRow();
        switch (bombDir)
        {
            case BombDir.Right:
            case BombDir.Left:
                toRow = dataBombInfo.DataBomb.GetCol();
                break;
            case BombDir.Up:
            case BombDir.Down:
                toRow = dataBombInfo.DataBomb.GetRow();
                break;
        }
        //Debug.Log(" 가로 " + toRow + " " + row + " " + col);
        return toRow;
    }
    public int GetCol()
    {
        int toCol = dataBombInfo.DataBomb.GetCol();
        switch (bombDir)
        {
            case BombDir.Right:
            case BombDir.Left:
                toCol = dataBombInfo.DataBomb.GetRow();
                break;
            case BombDir.Up:
            case BombDir.Down:
                toCol = dataBombInfo.DataBomb.GetCol();
                break;
        }
        //Debug.Log(" 세로 " + toCol + " " + row + " " + col);
        return toCol;
    }

    //public Quaternion GetDirToRot()
    //{
    //    Vector3 rot = Vector3.zero;
    //    switch (bombDir)
    //    {
    //        case BombDir.Up:
    //            rot = Vector3.zero;
    //            break;
    //        case BombDir.Right:
    //            rot = new Vector3(0f, 0f, -90f);
    //            break;
    //        case BombDir.Down:
    //            rot = new Vector3(0f, 0f, 180f);
    //            break;
    //        case BombDir.Left:
    //            rot = new Vector3(0f, 0f, 90f);
    //            break;

    //    }
    //    return Quaternion.Euler(rot);
    //}
    public void BombRot()
    {
        switch (bombDir)
        {
            case BombDir.Up:
                bombDir = BombDir.Right;
                image.rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
                break;
            case BombDir.Right:
                bombDir = BombDir.Down;
                image.rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
                break;
            case BombDir.Down:
                bombDir = BombDir.Left;
                image.rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                break;
            case BombDir.Left:
                bombDir = BombDir.Up;
                image.rectTransform.rotation = Quaternion.identity;
                break;
        }
        //image.rectTransform.localRotation = GetDirToRot();
    }

    public Quaternion GetBombRotQuater()
    {
        Quaternion qu = Quaternion.identity;
        switch (bombDir)
        {
            case BombDir.Up:
                qu = Quaternion.identity;
                break;
            case BombDir.Right:
                qu = Quaternion.Euler(new Vector3(0f, 0f, 270f));
                break;
            case BombDir.Down:
                qu = Quaternion.Euler(new Vector3(0f, 0f, 180f));
                break;
            case BombDir.Left:
                qu = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                break;
        }
        return qu;
    }

    public void BombPurchase()
    {
        if (noPurchase) return;
        // 골드 체크
        if(!dataBombInfo.BombUnLock)
        {
            if(CGameManager.Instance.Purchase(dataBombInfo.DataBomb.GetUnLockingCost()))
            {
                CGameManager.Instance.BombUnLock(dataBombInfo.DataBomb.GetNumber());
                bombNumber = dataBombInfo.BombNumber;
            }
        }
        else
        {
            if(CGameManager.Instance.Purchase(dataBombInfo.DataBomb.GetCost()))
            {
                CGameManager.Instance.BombNumberAdd(dataBombInfo.DataBomb.GetNumber());
                bombNumber++;
            }
        }
        Setting();
    }

    public bool NumberDown()
    {
        if(bombNumber > 0)
        {
            bombNumber--;
            Setting();
            return true;
        }
        return false;
    }

    public void ReSetting()
    {
        bombNumber = this.dataBombInfo.BombNumber;
        Setting();
    }

    public void UseBomb()
    {
        if(dataBombInfo.DataBomb.GetNumber() != 0)
        dataBombInfo.BombNumber = bombNumber;
    }
    public CDataBombInfo GetDataBombInfo()
    {
        return dataBombInfo;
    }

}

