using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour
{    
    private bool _ballLoaded; 
    
    public void ChangeState(bool t)
    {
        _ballLoaded = t;
    }
    public void RequestToFire()
    {
        if (_ballLoaded)
        {
            Fire();
            Debug.Log("FIRE!!!");
        }
    }
    private void Fire()
    {
        _ballLoaded = false;
    }

    void Start()
    {
        
    }        
}
