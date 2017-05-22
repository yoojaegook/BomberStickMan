using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern;

public class CBombManager : MonoBehaviour {
	public static CBombManager Instance = null;
	public CCameraWorkers _cameras;
	public float delay=0.01f;
	bool _isBombed;
	List<GameObject> _bombs = new List<GameObject>();
	public List<GameObject> _Prefabs = new List<GameObject>();
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		Instance = this;
	}
	// Use this for initialization
	void Start () {
		_isBombed = false;
	}
	
	
	public bool IsBombed()
	{
		return _isBombed;
	}
	public void defaultingBombed()
	{
		_cameras.TurnZoomIn();
		_isBombed = false;
	}

	public void TurnOnBomb()
	{
		_cameras.TurnZoomIOut();
		_isBombed = true;
	}
	public void AddBomb(GameObject go)
	{
		_bombs.Add(go);
	}
 
	public void OnSwitch()
	{
		StartCoroutine(StartSW());
	}
	IEnumerator StartSW()
	{
		if(_bombs.Count==0)yield break;
		foreach (GameObject b in _bombs)
		{
			yield return new WaitForSeconds(delay);
			b.SendMessage("Boom");
		}
		_bombs.Clear();
	}
	public GameObject GetPreFab(int index)
	{
		return _Prefabs[index-1];
	}
}
