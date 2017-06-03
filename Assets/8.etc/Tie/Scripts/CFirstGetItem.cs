using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CFirstGetItem : MonoBehaviour {

    public Image _image;
    Image _imageBack;

    [Range(0f, 5f)]
    public float showTime;

    private void Awake()
    {
        _imageBack = GetComponent<Image>();
        _imageBack.enabled = false;
    }

    private void Start()
    {
        _image.enabled = false;
    }

    public void FirstGetItem(Sprite sprite)
    {
        _image.sprite = sprite;
        _image.enabled = true;
        _imageBack.enabled = true;
        StopAllCoroutines();
        StartCoroutine(FirstGetItemCoroutine());

    }

    IEnumerator FirstGetItemCoroutine()
    {
        yield return new WaitForSeconds(showTime);
        _image.enabled = false;
        _imageBack.enabled = false;
    }


}
