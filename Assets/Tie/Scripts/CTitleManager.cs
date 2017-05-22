using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTitleManager : MonoBehaviour {
	public Text max;
	public Text last;
	
	void Start()
	{
		Debug.Log("2222222222");
		max.text = "Max " + CDataManager.instance.maxScore.ToString();
		last.text = "Last " + CDataManager.instance.lastScore.ToString();
	}
	
}
