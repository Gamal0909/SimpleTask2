using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynametMan : MonoBehaviour
{
    [SerializeField] private Transform hidePoint, appearedPoint;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float appearRange, fireRange;
    private bool flagFired=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DynametManMovement(hidePoint.position, appearedPoint.position);
        FireMySelf();
    }
    private void DynametManMovement(Vector3 hidePosition , Vector3 appearPosition)
    {
        if(IsPlayerInAppearRange())
        {
            transform.position = Vector3.Slerp(hidePosition, appearPosition, 1);
        }
        else
        {
            transform.position = hidePosition;
        }
    }

    private void FireMySelf()
    {
        if(IsPlayerInFireRange()&&!flagFired) 
        {
            GameObject explosion = Instantiate(explosionPrefab,transform.position+new Vector3(0,.5f,0), Quaternion.identity);
            Destroy(explosion,1f);
            Destroy(transform.parent.gameObject, .1f);
            flagFired = true;
        }
    }
    private bool IsPlayerInAppearRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, appearRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
                return true;
        }
        return false;
    }
    private bool IsPlayerInFireRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, fireRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
                return true;
        }
        return false;
    }
}
