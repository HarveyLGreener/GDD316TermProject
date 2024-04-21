using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    public bool holdingObject = false;
    public GameObject objectGrabbed;
    public Vector3 screenPoint;
    public Vector3 offset;
    private void Start()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !holdingObject)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10))
            {
                if (hit.collider.gameObject.GetComponent<Rigidbody>() != null)
                {
                    objectGrabbed = hit.collider.gameObject;
                    Debug.Log("Trying to grab object");
                    objectGrabbed.GetComponent<Rigidbody>().isKinematic = true;
                    objectGrabbed.transform.parent = this.gameObject.transform;
                    objectGrabbed.transform.localPosition = Vector3.zero;
                    //objectGrabbed.transform.rotation = Quaternion.identity;
                    objectGrabbed.transform.localPosition = new Vector3(offset.x, offset.y, offset.z);
                    holdingObject = true;
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && holdingObject)
        {
            objectGrabbed.transform.parent = null;
            objectGrabbed.GetComponent<Rigidbody>().isKinematic = false;
            objectGrabbed = null;
            holdingObject = false;
        }
    }
}
