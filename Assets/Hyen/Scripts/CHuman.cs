using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class CHuman : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    float size = 62.5f;
    public Image humanImg;
    RectTransform rectTransform;
    CBombType dragBombType;
    int row = 2;
    int col = 3;
    bool isPlacement = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlacement) return;
        Debug.Log("드레그 시작");
            
        dragBombType = CBombLayoutManager.Instance.CreateBombType(transform.position, this);

        //var canvas = FindInParents<Canvas>(gameObject);
        //if (canvas == null)
        //    return;


        rectTransform = transform as RectTransform;
        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragBombType != null)
            SetDraggedPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragBombType != null)
        {
            //Debug.Log("삭제 실행 체크");
            Destroy(dragBombType.gameObject);
            dragBombType = null;
            CBombLayoutManager.Instance.ReMoveCreateAndDrag();
        }
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

    public Sprite GetSprite()
    {
        return humanImg.sprite;
    }

    public int GetRow()
    {
        return row;
    }

    public int GetCol()
    {
        return col;
    }
    public float GetSize()
    {
        return size;
    }
    public void PlaceMent()
    {
        isPlacement = true;
        humanImg.enabled = false;
        Debug.Log("배치 완료");
    }

    public void ReSetting()
    {
        isPlacement = false;
        humanImg.enabled = true;
    }

    public bool GetIsPlacement()
    {
        return isPlacement;
    }
}
