using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

	[FormerlySerializedAs("Starts")][SerializeField] private Transform[] starts;
	[FormerlySerializedAs("Timer Text")] [SerializeField] private Text timerText;
	[FormerlySerializedAs("Timer time")] [SerializeField]private int _time;
	// Use this for initialization
	private void Start () {
		InvokeRepeating("RunTimer", 1, 1);
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

	public void Reload()
	{
		foreach (var star in starts)
		{
			star.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Star_Normal");
		}

		SetTimer(30);
	}

	private void SetTimer(int iValue)
	{
		_time = iValue;
		timerText.text = _time.ToString();
	}
	private void RunTimer()
	{
		SetTimer(_time - 1);
		if (_time == -1)
		{
			timerText.color = Color.red;
		}
		else if (_time == 20)
		{
			starts[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Star_Black");
		}
		else if (_time == 10)
		{
			starts[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Star_Black");
		}
		else if (_time == 0)
		{
			starts[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Star_Black");
		}
	}
}
