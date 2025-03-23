using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    public void BallInHands()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;        
        _rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void BallOutOfHands()
    {        
        _rb.constraints = RigidbodyConstraints.None;
        transform.parent = null;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }    
}
