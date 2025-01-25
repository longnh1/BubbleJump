using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
	public Transform player;
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
			tmp=Instantiate(bubblePrefab, player.position, Quaternion.identity, transform);
			tmp.SetActive(false);
			pooledBubbles.Add(tmp);
		}
	}
	
	public GameObject GetPooledObject()
	{
		// return the first available pooled object
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
			//Debug.Log("Pressed left-click.");
			GameObject bubble = GetPooledObject();
			if (bubble!=null){
				//Debug.Log("spawned Bubble");
				bubble.transform.position=player.position;
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mousePos.z=10;
				bubble.GetComponent<BubbleScript>().targetPosition=mousePos;
				bubble.SetActive(true);
			}
		}
		
		// shoot the bubbles if mouse right click
		if (Input.GetMouseButtonDown(1)){
			RaycastHit2D hit = Physics2D.Raycast(player.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)-player.position);
			GameObject oth=hit.collider.gameObject;
			if (oth.CompareTag("bubble")){
				oth.GetComponent<BubbleScript>().pop();
			}
		}
	}
}
