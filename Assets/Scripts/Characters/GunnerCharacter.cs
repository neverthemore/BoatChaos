using UnityEngine;

public class GunnerCharacter : CrewCharacter
{
    private bool _withBall = false;
    [SerializeField] private float _range = 4f;

    private Transform _ballPlacement;
    Ball ball;        
    private void TakeTheBall()
    {
        Ray ray = new Ray(cmCameraGameObject.transform.position, cmCameraGameObject.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _range))
        {            
            if (hit.collider.CompareTag("Ball"))
            {                
                ball = hit.collider.gameObject.GetComponent<Ball>();
                ball.transform.SetParent(_ballPlacement);
                ball.BallInHands();
                _withBall = true;
            }            
        }        
    }

    private void ThrowTheBall()
    {        
        ball.BallOutOfHands();
        _withBall = false;
    }

    private void LoadTheBall()
    {
        Ray ray = new Ray(cmCameraGameObject.transform.position, cmCameraGameObject.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _range))
        {
            if (hit.collider.CompareTag("Gun"))
            {
                Debug.Log("loading");
                _withBall = false;
                Cannon cannon = hit.collider.gameObject.GetComponent<Cannon>();
                cannon.ChangeState(true);
                Destroy(ball.gameObject);
            }
        }
    }
    void Fire()
    {
        Ray ray = new Ray(cmCameraGameObject.transform.position, cmCameraGameObject.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _range))
        {
            if (hit.collider.CompareTag("Gun"))
            {                
                Cannon cannon = hit.collider.gameObject.GetComponent<Cannon>();                
                cannon.RequestToFire();
            }
        }
    }

    protected override void Start()
    {        
        base.Start();        
        _ballPlacement = GameObject.Find("BallPlacement").transform;
        ball = null;
    }

    protected override void Update()
    {
        base.Update();
        if (inputActions.Crew.PickUp.IsPressed() && _withBall == false)
        {
            TakeTheBall();            
        }
        if (inputActions.Crew.PutDown.IsPressed() && _withBall == true)
        {
            ThrowTheBall();
        }
        if (inputActions.Crew.Use.IsPressed() && _withBall == true)
        {            
            LoadTheBall();
        }
        if (inputActions.Gunner.Shoot.IsPressed() && _withBall == false)
        {
            Fire();
        }
    }
}
