using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // 追加
    private Vector3 screenPoint;
    private Vector3 offset;

    // Start is called before the first frame update
    private Vector3 prevMousePos;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
		prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb = gameObject.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if ( Input.GetMouseButton(0) ) {
			// マウスの移動分だけ速度与える
			rb.velocity = (mousePos - prevMousePos) / Time.deltaTime;
		}

		prevMousePos = mousePos;
	}

    
    // 追加
    void OnMouseDown()
    {
        rb.useGravity = false;
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    // 追加
    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        transform.position = currentPosition;
    }
}
