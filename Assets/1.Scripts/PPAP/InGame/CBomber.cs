using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBomber : MonoBehaviour {

	float _forceSpeed=10f;
	Vector2 _dir = Vector2.zero;
	Rigidbody2D _rbody2D;
	// Use this for initialization
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		_rbody2D = GetComponent<Rigidbody2D>();
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ShootTheBall(new Vector2());
		}
	}

	void ShootTheBall(Vector2 dir)
	{
		_rbody2D.AddForce(_forceSpeed*dir);
	}
}
