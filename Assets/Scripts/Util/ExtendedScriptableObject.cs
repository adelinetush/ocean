using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ExtendedScriptableObject : ScriptableObject {
	public string ID => name;


	public static T Get<T>(string ID) where T : ExtendedScriptableObject {
		return GetAll<T>().Find(x => x.ID == ID);
    }


	public static List<T> GetAll<T>() where T : ExtendedScriptableObject {
		return Resources.LoadAll<T>("Data").ToList();
	}
}


