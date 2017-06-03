using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Pattern;

public class CGameData : Singleton<CGameData> {
	
	public T GetValue<T>(string valName,Component c)
	{
			Debug.Log(c.name);
			T returnvalue = (T) this.GetType().InvokeMember(valName,
        	BindingFlags.Instance | BindingFlags.NonPublic |
        	BindingFlags.GetField, null, this, null);
			return returnvalue;
	}
}
