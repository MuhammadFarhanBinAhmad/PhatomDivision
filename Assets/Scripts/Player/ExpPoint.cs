using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPoint : MonoBehaviour
{
    public int expPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerSkills>() !=null)
        {
            other.GetComponent<PlayerSkills>().AddExp(expPoint);
            Destroy(transform.parent.gameObject);
        }
    }
}
