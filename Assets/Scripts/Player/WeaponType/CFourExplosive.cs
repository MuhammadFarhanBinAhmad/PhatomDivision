using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFourExplosive : MonoBehaviour
{
    public GameObject s_Explosion;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            ExplodeGrenade();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.SetParent(other.transform);
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void ExplodeGrenade()
    {
        Instantiate(s_Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
