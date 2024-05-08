using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<AgentAdder>() != null)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.transform.parent = collision.collider.gameObject.transform;
            this.transform.localPosition = new Vector3(1,0,1);
        }
    }
}
