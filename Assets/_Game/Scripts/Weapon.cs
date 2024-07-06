using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float range = 10;
    private float speed = 2f;
    private Vector3 startPosition;
    private void Start()
    {
        startPosition = transform.position;
        rb.velocity = speed * transform.forward;
    }
    private void Update()
    {
        CheckRange();
    }
    public virtual void Throw(Transform throwTransform)
    {
        Instantiate(gameObject, throwTransform.position, throwTransform.rotation);
    }
    private void CheckRange()
    {
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        if (distanceTravelled >= range)
        {
            OnWeaponMaxRangeReached();
        }
    }
    protected virtual void OnWeaponMaxRangeReached()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            Destroy(gameObject);
        }
    }
}
