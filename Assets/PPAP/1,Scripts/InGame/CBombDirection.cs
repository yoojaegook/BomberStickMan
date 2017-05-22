using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBombDirection : MonoBehaviour {

	GameObject _Target;
	public Collider2D _coll;  
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject GetObj()
	{
		return _Target;
	}

	/// <summary>
	/// Sent each frame where another object is within a trigger collider
	/// attached to this object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.Equals("Player"))
		{
			_Target = other.transform.gameObject;
		}
	}
}
