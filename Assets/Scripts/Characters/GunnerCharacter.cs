using UnityEngine;

public class GunnerCharacter : CrewCharacter
{
    private bool _withBall = false;
    [SerializeField] private float _range = 4f;

    private Transform _ballPlacement;
    GameObject ball;

    private void BallInHands()
    {
        ball.transform.SetParent(_ballPlacement);        
        ball.transform.localPosition = Vector3.zero;
        ball.transform.localEulerAngles = Vector3.zero;
        Rigidbody ballRB = ball.GetComponent<Rigidbody>();
        ballRB.constraints = RigidbodyConstraints.FreezeAll;            
        _withBall = true;
    }

    private void BallOutOfHands()
    {
        Rigidbody ballRB = ball.GetComponent<Rigidbody>();
        ballRB.constraints = RigidbodyConstraints.None;
        ball.transform.parent = null;
    }
    private void TakeTheBall()
    {
        Ray ray = new Ray(cmCameraGameObject.transform.position, cmCameraGameObject.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _range))
        {            
            if (hit.collider.CompareTag("Ball"))
            {                
                ball = hit.collider.gameObject;
                BallInHands();
                _withBall = true;
            }            
        }
        Debug.DrawRay(cmCameraGameObject.transform.position,
             cmCameraGameObject.transform.forward * 100f,
             Color.red,
             1f);
    }

    private void ThrowTheBall()
    {
        _withBall = false;
        BallOutOfHands();
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
    }
}
