using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInputBox : MonoBehaviour {

	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Word"))
		{
			if (SceneScript.GetWordNumberByName(this.name) == SceneScript.GetWordNumberByName(other.name))
			{
				this.GetComponent<SpriteRenderer>().sprite = other.GetComponent<SpriteRenderer>().sprite;
				this.GetComponent<Rigidbody2D>().simulated = false;
			}
		}
	}
}
