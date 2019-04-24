﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

public class DataManager : MonoBehaviour {

	// Use this for initialization
	private void Start () {
		LoadAll();
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

	public static void LoadAll()
	{
		if (_isLoad == true)
		{
			return;
		}

		LoadObject(out ContTaskData, "TaskData");
		_isLoad = true;
	}

	private static void SaveAll()
	{
		//SaveObject(ContTaskData, "TaskData");
	}
	
	private static void LoadObject<T>(out T kData, string xmlName)
	{
		var xmlPatch = "Database/" + xmlName;
		var textAsset = Resources.Load(xmlPatch, typeof(TextAsset)) as TextAsset;
/*
		if (textAsset == null)
		{
			var kTemp = new T();
			kData = T();
			return;
		}
*/
//		Debug.Log("Failed load file " + xmlPatch);

		var xml = new XmlSerializer(typeof(T));
		using (var reader = new StringReader(textAsset.text))
		{
			kData = (T)xml.Deserialize(reader);
		}
	}
/*
	private static void SaveObject<T>(T kData, string xmlName)
	{
		var xml = new XmlSerializer(typeof(T));
		using (var ff = new FileStream(xmlName, FileMode.OpenOrCreate))
		{
			xml.Serialize(ff, kData);
		}
	}
*/

	[Serializable]
	public class CTaskData
	{
		public string Answer;
		public string Quest;

	}

	public static List<CTaskData> ContTaskData = new List<CTaskData>();
	private static bool _isLoad;
}
