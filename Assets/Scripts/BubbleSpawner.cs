using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
	public GameObject bubblePrefab;
	public static BubbleSpawner BubbleBath;
	public List<GameObject> pooledBubbles;
	public int poolSize;

	void Awake(){
		//set this script as a bubble pool
		BubbleBath = this;
	}
	
	void Start()
	{
		//create the object pool
		pooledBubbles = new List<GameObject>();
		GameObject tmp;
		for(int i=0; i<poolSize; i++){
			tmp=Instantiate(bubblePrefab);
			tmp.SetActive(false);
			pooledBubbles.Add(tmp);
		}
	}
	
	public GameObject GetPooledObject()
	{
		for(int i = 0; i < poolSize; i++){
			if(!pooledBubbles[i].activeInHierarchy){
				return pooledBubbles[i];
			}
		}
		return null;
	}
	
	// Update is called once per frame
	void Update()
	{
		// spawn bubbles if mouse button is pressed
		if (Input.GetMouseButtonDown(0)){
			Debug.Log("Pressed left-click.");
			GameObject bubble = GetPooledObject();
			if (bubble!=null){
				Debug.Log("spawned Bubble");
				bubble.transform.position=transform.position;
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mousePos.z=10;
				bubble.GetComponent<BubbleScript>().targetPosition=mousePos;
				bubble.SetActive(true);
			}
		}
	}
}
