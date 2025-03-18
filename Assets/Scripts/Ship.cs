using Unity.VisualScripting;
using UnityEngine;

public class Ship : MonoBehaviour
{
    Wheel wheel;
    Collider collider;
    CharacterController controller;
    float _maxAngle = 24f;
    float _shipSpeed = 2f;
    float _maxIncline = 6f;
    float _incline = 1f;
    bool _rightIncline = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wheel = FindAnyObjectByType<Wheel>();
        collider = GetComponent<Collider>();
        controller = GetComponent<CharacterController>();
    }

    private void MoveShip()
    {
        Vector3 move = new Vector3(0f, 0f, _shipSpeed);
        controller.Move(move);
    }

    private void RotateShip()
    {
        float angle = wheel.GetCurrentAngle();
        float shipAngle = Mathf.Clamp(angle / (1080 / _maxAngle), -_maxAngle, _maxAngle);

        float deltaIncline = (_shipSpeed / Mathf.Abs(_incline + 0.00001f)) * Time.deltaTime;
        Mathf.Clamp(deltaIncline, 0.05f, 0.1f);
        _incline += deltaIncline * ((_rightIncline) ? 1f : -1f);
        if (_incline >= _maxIncline) _rightIncline = false;
        else if (_incline <= -_maxIncline) _rightIncline = true;        

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, shipAngle, _incline);
    }

    // Update is called once per frame
    void Update()
    {
        //MoveShip();

        RotateShip();
    }
}
