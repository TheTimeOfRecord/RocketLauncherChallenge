using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class RocketMovementC : MonoBehaviour
{
    RocketControllerC controller;
    private Rigidbody2D _rb2d;
    private readonly float SPEED = 10f;
    private readonly float ROTATIONSPEED = 0.02f;

    private float rotateDirection;

    private float highScore = -1;

    public static Action<float> OnHighScoreChanged;
    
    private void Awake()
    {
        controller = GetComponent<RocketControllerC>();
        _rb2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rotateDirection = _rb2d.rotation;
        controller.OnMoveEvent += Move;
        controller.OnBoostEvent += Boost;
    }
    private void FixedUpdate()
    {
        if (!(highScore < transform.position.y)) return;
        highScore = transform.position.y;
        OnHighScoreChanged?.Invoke(highScore);

        ApplyBoost();
        ApplyMovement(rotateDirection);
    }
    private void Move(float obj)
    {
        throw new NotImplementedException();
    }
    private void Boost(bool obj)
    {
        throw new NotImplementedException();
    }

    public void ApplyMovement(float rotationInput)
    {
        Rotate(rotationInput);
    }

    public void ApplyBoost()
    {
        _rb2d.AddForce(transform.up * SPEED, ForceMode2D.Impulse);
    }

    private void Rotate(float rotationInput)
    {
        // 움직임에 따라 회전을 바꿈 -> 회전을 바꾸고 그 방향으로 발사를 해야 그쪽으로 가겠죠?
        transform.Rotate(0, 0, -rotationInput * ROTATIONSPEED * Time.fixedDeltaTime);
    }
}