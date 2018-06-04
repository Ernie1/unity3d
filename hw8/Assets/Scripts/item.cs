using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems; 

public class item : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler { 

	public GameObject background;
	public GameObject cell;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrag(PointerEventData eventData) {
		transform.position = Input.mousePosition;
		transform.SetParent (background.transform);
	}

	public void OnPointerDown(PointerEventData eventData) {
		transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
		transform.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnPointerUp(PointerEventData eventData) {
		transform.localScale = new Vector3 (1f, 1f, 1f);
		GameObject p = eventData.pointerCurrentRaycast.gameObject;
		if (p.tag == "cell")
			transform.SetParent (p.transform);
		else if (p.tag == "item") {
			transform.SetParent (p.transform.parent.transform);
			p.transform.SetParent (cell.transform);
			p.GetComponent<item> ().cell = p.transform.parent.gameObject;
		}
		else
			transform.parent = cell.transform;
		cell = transform.parent.gameObject;
		transform.GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
}
