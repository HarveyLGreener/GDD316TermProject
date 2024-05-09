using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerScoring : MonoBehaviour
{
    public UnityEvent onScore;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
        {
            onScore.Invoke();
        }
    }
}
