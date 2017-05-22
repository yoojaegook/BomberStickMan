using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CDataManager : MonoBehaviour {


	public static CDataManager instance;

	public bool[] unlock = new bool[6];
	public int reSetCount = 0;

	public int score = 0;
	public int maxScore = 0;
	public int lastScore = 0;

	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(this);
		unlock[0] = true;
		Debug.Log("데이터 실행 체크");	
	}

	public void UnLockReSet()
	{
		for(int i = 1; i < unlock.Length; i++)
		{
			unlock[i] = false;
		}
	}

	public void ReSetCountUp()
	{
		reSetCount++;
	}
	public int GetReSetCount()
	{
		return reSetCount;
	}
	public void ResetReSetCount()
	{
		reSetCount = 0;
		if(maxScore < score)
		{
			maxScore = score;
		}
		lastScore = score;
		score = 0;
		Debug.Log("111111111111" + " " + maxScore + " " + lastScore);
	}

	public void AddScore(int score)
	{
		this.score += score;
	}
}
