using UnityEngine;

public class GunnerCharacter : CrewCharacter
{
    private bool _withBall = false;
    private Transform _ballPlacement;
    private void TakeTheBall()
    {
        Ray ray = new Ray(cmCameraGameObject.transform.position, cmCameraGameObject.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {            
            if (hit.collider.CompareTag("Ball"))
            {
                GameObject ball = hit.collider.gameObject;
                ball.transform.position = _ballPlacement.position;
                ball.SetActive(false);
                _withBall = true;
            }            
        }
    }
    protected override void Start()
    {        
        base.Start();        
        _ballPlacement = GameObject.Find("BallPlacement").transform;
    }

    protected override void Update()
    {
        base.Update();
        if (inputActions.Crew.PickUp.IsPressed())
        {
            TakeTheBall();
            Debug.Log("pressed");
        }
        
    }
}
