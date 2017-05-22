using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern;

public class CEffectManager : MonoBehaviour {

	public static CEffectManager Instance=null;
	public List<GameObject> _efList = new List<GameObject>();
	Dictionary<int,GameObject> _efDic = new Dictionary<int,GameObject>();
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		Instance = this;
		for (int i = 1; i <= _efList.Count; i++)
		{
			_efDic.Add(i,_efList[i-1]);
		}
	}

	public GameObject GetEffectObj(int index)
	{
		return _efDic[index];
	}
}
