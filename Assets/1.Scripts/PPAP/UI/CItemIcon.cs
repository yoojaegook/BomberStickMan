using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CItemIcon : MonoBehaviour {

	public bool _isLocked;
	public int _index;
	public Image _backImg;
	public Image _frontImg;
	public Sprite _bombSp;
	public Sprite _baseUnLockSp;
	public Sprite _baseLockedSp;

	public void Unlock()
	{
		_isLocked = false;
		_frontImg.sprite = _bombSp;
        _frontImg.color = new Color(1, 1, 1, 1);
		_backImg.sprite = _baseUnLockSp;
	}
	public void Onlock()
    {
		_isLocked = true;
        _frontImg.sprite = null;
		_frontImg.color = new Color(1,1,1,0);
        _backImg.sprite = _baseUnLockSp;
    }
	public void SendItemMsg()
	{
		if(!_isLocked)
		ItemManager.Instance.SetObj(_index);
	}
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		if(_isLocked)
		{
			Onlock();
		}else
		{
			Unlock();
		}
	}

}
