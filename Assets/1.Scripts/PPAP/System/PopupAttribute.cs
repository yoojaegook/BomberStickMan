using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupAttribute : PropertyAttribute {

	public string[] value;
	public PopupAttribute(params string[] input)
	{
		value = input;
	}
}
