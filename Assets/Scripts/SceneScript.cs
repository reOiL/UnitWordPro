using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour {

	// Use this for initialization
	[FormerlySerializedAs("Quest Text")] public Text questText;
	[FormerlySerializedAs("Word Template")] public Transform wordTemplate;
	[FormerlySerializedAs("Input Word Box")] public Transform inputWordBox;
	[SerializeField] private float _moveSpeed = 10.0f;
	
	private Transform _wordTemplatePool;
	private Transform _inputWordBoxPool;
	private Dictionary<int, Vector3> _dictSourcesWordPosition  = new Dictionary<int, Vector3>();
	private void Start ()
	{
		DataManager.LoadAll();
		var spriteResources = Resources.LoadAll<Sprite>("Word");
		questText.text = DataManager.ContTaskData[0].Quest;
		BuildWordInputBox(DataManager.ContTaskData[0].Answer.ToUpper());
		for (var i = 0; i < 33; i++)
		{
			var vecPos = new Vector3(-2.0f, -1.7f, 0.0f); //  + 0.65f * i
			if (6 < i && i < 14)
			{
				vecPos.x += 0.65f * (i - 7);
				vecPos.y -= 0.7f;
			}
			else if (13 < i && i < 21)
			{
				vecPos.x += 0.65f * (i - 14);
				vecPos.y -= 0.7f * 2.0f;
			}
			else if (20 < i && i < 28)
			{
				vecPos.x += 0.65f * (i - 21);
				vecPos.y -= 0.7f * 3.0f;
			}
			else if (27 < i)
			{
				vecPos.x += 0.65f * (i - 27);
				vecPos.y -= 0.7f * 4.0f;
			}
			else
			{
				vecPos.x += 0.65f * i;
			}

			_wordTemplatePool = Instantiate(wordTemplate);
			_wordTemplatePool.name = "Word_" + i.ToString();
			_wordTemplatePool.position = vecPos;
			_wordTemplatePool.GetComponent<SpriteRenderer>().sprite = spriteResources[i];
			_dictSourcesWordPosition.Add(i, vecPos);
		}
	}

	private void BuildWordInputBox(string word)
	{
		for (var i = 0; i < word.Length; i++)
		{
			_inputWordBoxPool = Instantiate(inputWordBox);
			var charCode = Convert.ToInt32(word[i]) - 1040;
			if (charCode > 5)
			{
				charCode += 1;
			}
			_inputWordBoxPool.name = "WordBox_" + charCode;
			_inputWordBoxPool.position = new Vector3(-2.2f + 0.63f * i, -0.39f, 0);
		}
	}
	// Update is called once per frame
	private Transform _transformNowMove;
	public static int GetWordNumberByName(string name)
	{
		var iWordPos = name.IndexOf("_", StringComparison.Ordinal);
		if (iWordPos == -1)
		{
			return -1;
		}

		var indexName = name.Substring(iWordPos + 1);
		Debug.Log(indexName);
		return Convert.ToInt32(indexName);
	}
	private void Update ()
	{
		if (Input.touchCount == 0)
		{
			if (_transformNowMove != null)
			{
				var wordId = GetWordNumberByName(_transformNowMove.name);
				if (wordId == -1)
				{
					goto EXIT;
				}
				_transformNowMove.position = Vector3.MoveTowards(
					_transformNowMove.position,
					_dictSourcesWordPosition[wordId],
					_moveSpeed);
				EXIT:
				_transformNowMove = null;
			}
		}

		if (Input.touchCount == 0) return;
		
		if (_transformNowMove == null)
		{
			if (Camera.main == null) return;
			var mousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
			var hit = Physics2D.Raycast(mousePos, Vector2.zero);

			if (hit.transform == null) return;
			if (!hit.transform.CompareTag("Word")) return;
			
			_transformNowMove = hit.transform;
		}
		else
		{
			if (Camera.main == null) return;
			var vecNewPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
			vecNewPos.z = -1;
			_transformNowMove.transform.position = Vector3.MoveTowards(
				_transformNowMove.transform.position,
				vecNewPos,
				_moveSpeed);
		}
	}
}
