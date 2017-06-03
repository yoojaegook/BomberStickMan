using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CEditorSlider : MonoBehaviour {

	public Slider slider;
	public Text text;
	string methodName;
	 /// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void Start()
	{
		methodName = slider.onValueChanged.GetPersistentMethodName(0);
		UnityEngine.Debug.Log(methodName);
		string name;
		string namemax;
		string namemin;
		if (!(string.Compare(methodName,this.name,true)==0))
		{
			Debug.Log("wrong value");
		}
		name = "_"+methodName;
		namemin = name+"Min";
		namemax = name+"Max";
		if (IsInt())
		{
			float t = ReturnValueToName<float>(name);
			int min = ReturnValueToName<int>(namemin);
			int max = ReturnValueToName<int>(namemax);
			ChangeSliderValue(t,min,max);
		}else
		{
			float t = ReturnValueToName<float>(name);
			float min = ReturnValueToName<float>(namemin);
			float max = ReturnValueToName<float>(namemax);
			ChangeSliderValue(t,min,max);
		}
		text.text = slider.value.ToString();
		slider.onValueChanged.AddListener(OnSlide);
	}
	
	bool IsInt()
	{
		if(slider.wholeNumbers)
		{
			Debug.Log("int");
			return true;
		}else
		{
			Debug.Log("float");
			return false;
		}
	}
	public void OnSlide(float value)
	{
		text.text = (Mathf.Round(value*100f)/100f).ToString();
//		CGameData.instance.SendMessage(methodName,value);
	}

	void ChangeSliderValue(float t,float min,float max)
	{
		slider.value = Mathf.Round(t*100f)/100f;
		slider.minValue = min;
		slider.maxValue = max;
	}

	void ChangeSliderValue(float t,int min, int max)
	{
		slider.value = t;
		slider.minValue = (float)min;
		slider.maxValue = (float)max;
	}
	
	T ReturnValueToName<T>(string name)
	{
		T returnvalue = CGameData.Instance.GetValue<T>(name,this);
		return returnvalue;
	}
	
}
