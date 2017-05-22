using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraWorkers : MonoBehaviour {

	public float _orginOrth = 5.0f;
	public float _targetOrth = 10.0f;
	public float _distPX = 2.0f;
	public float _distPY = -5.0f;
	public float _currentOrth;
	public float _smoothTime = 2.0f;
	public float _zoomOutSmoothTime = 2.0f;
	public GameObject _player;
	public Camera _cmr;
	bool _zoomOn = true;
	// Use this for initialization
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		_currentOrth = _orginOrth;
		_cmr = GetComponent<Camera>();
	}
	void Start () {
		
	}
	// Update is called once per frame
	void FixedUpdate () {
		float targetOrth = Mathf.Lerp(_cmr.orthographicSize,_currentOrth,_zoomOutSmoothTime*Time.fixedDeltaTime);
		_cmr.orthographicSize = targetOrth;
		float targetX = Mathf.Lerp(transform.position.x,_player.transform.position.x+_distPX,_smoothTime*Time.fixedDeltaTime);
		_cmr.transform. position = new Vector3(targetX,targetOrth+_distPY,transform.position.z);
	}
	public void TurnZoomIn()
	{
		_currentOrth = _orginOrth;
	}
	
	public void TurnZoomIOut()
	{
		_currentOrth = _targetOrth;
	}
}
