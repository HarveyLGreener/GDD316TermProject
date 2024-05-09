using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    public bool holdingObject = false;
    public GameObject objectGrabbed;
    public Vector3 screenPoint;
    public Vector3 offset;
    public float forceAmount = 10f;
    public float yDirectionMulti = 1.5f;
    public GameObject ball;
    private void Start()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        ball = FindFirstObjectByType<Ball>().gameObject;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !holdingObject)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10))
            {
                if (hit.collider.gameObject.GetComponent<Ball>() != null)
                {
                    objectGrabbed = hit.collider.gameObject;
                    objectGrabbed.GetComponent<Rigidbody>().isKinematic = true;
                    objectGrabbed.transform.parent = this.gameObject.transform;
                    objectGrabbed.transform.localPosition = Vector3.zero;
                    objectGrabbed.transform.localPosition = new Vector3(offset.x, offset.y, offset.z);
                    holdingObject = true;
                }
                else if (hit.collider.gameObject.GetComponent<StateController>() != null && hit.collider.gameObject.transform.childCount > 0)
                {
                    hit.collider.gameObject.transform.DetachChildren();
                    ball.GetComponent<Rigidbody>().isKinematic = false;
                    ball.GetComponent<Ball>().grabbable = false;
                    ball.GetComponent<Rigidbody>().AddForce(Vector3.one);
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && holdingObject)
        {
            objectGrabbed.transform.parent = null;
            objectGrabbed.GetComponent<Rigidbody>().isKinematic = false;
            Rigidbody rb = objectGrabbed.GetComponent<Rigidbody>();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;
            //direction.z = 0f; // Ensure z is 0 to keep the force in 2D space
            float yDirection = Camera.main.transform.eulerAngles.x;
            if (yDirection > 90f)
            {
                yDirection -= 360f;
            }
            Debug.Log(yDirection);
            direction.y = yDirection /90f * -1;

            // Normalize the direction vector
            direction.Normalize();

            // Apply force to the object in the direction of the mouse pointer
            rb.AddForce(new Vector3(direction.x * forceAmount, direction.y * forceAmount * yDirectionMulti, direction.z * forceAmount), ForceMode.Impulse);
            objectGrabbed = null;
            holdingObject = false;
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<StateController>() != null)
        {
            objectGrabbed.transform.parent = null;
            objectGrabbed.GetComponent<Rigidbody>().isKinematic = false;
            objectGrabbed = null;
            holdingObject = false;
        }
    }*/
}
