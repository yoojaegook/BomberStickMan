using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBodyGroundCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		//Debug.Log(other.collider.name + " 충돌");
		if (other.collider.tag.Equals("Ground"))
		{
			StartCoroutine(Defaulting());
		}
	}

	IEnumerator Defaulting()
	{
		yield return new WaitForSeconds(2.5f);
		ItemManager.Instance.ResetScene();
	}
}
