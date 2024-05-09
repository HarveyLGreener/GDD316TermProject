using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool grabbable = true;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<AgentAdder>() != null && grabbable)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.transform.parent = collision.collider.gameObject.transform;
            this.transform.localPosition = new Vector3(1,0,1);
            collision.gameObject.GetComponent<StateController>().TransitionToState(collision.gameObject.GetComponent<StateController>().ballState);
        }
    }

    private void LateUpdate()
    {
        grabbable = true;
    }
}
