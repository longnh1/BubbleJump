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

	// Update is called once per frame
	void Update()
	{
	}
}
