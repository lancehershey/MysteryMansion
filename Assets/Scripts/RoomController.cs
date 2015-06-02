﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : MonoBehaviour {

	public List<GameObject> charactersInRoom;
	public float cameraSize = 4f;

	private GameObject killer;
	private Sprite normalSprite;

	void OnTriggerEnter(Collider character)
	{
		charactersInRoom.Add(character.gameObject);
		if(character.tag == "Player")
		{
			Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
			Camera.main.orthographicSize = cameraSize;
		}

	}

	void OnTriggerExit(Collider character)
	{
		charactersInRoom.Remove(character.gameObject);
	}

	void Start()
	{
		charactersInRoom.Clear();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(charactersInRoom.Count == 2)
		{
			foreach(GameObject c in charactersInRoom)
			{
				if(c.tag == "Killer")
				{
					int target;
					int killerIndex = charactersInRoom.IndexOf(c);
					if(killerIndex == 0)
						target = 1;
					else
						target = 0;
					c.GetComponent<AIController>().Kill(charactersInRoom[target]);
					charactersInRoom.RemoveAt(target);
				}
			}
		}
	}
}
