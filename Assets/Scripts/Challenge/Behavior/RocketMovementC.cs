using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RocketMovementC : MonoBehaviour
{
    RocketControllerC controller;
    private Rigidbody2D _rb2d;
    private readonly float SPEED = 10f;
    private readonly float ROTATIONSPEED = 20f;

    private float rotateDirection;
    private bool isBoost;

    private float highScore = -1;

    public static Action<float> OnHighScoreChanged;
    
    private void Awake()
    {
        controller = GetComponent<RocketControllerC>();
        _rb2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rotateDirection = transform.rotation.z;
        isBoost = false;
        controller.OnMoveEvent += Move;
        controller.OnBoostEvent += Boost;
    }
    private void Update()
    {
        ApplyMovement(rotateDirection);
        ApplyBoost();
    }
    private void FixedUpdate()
    {
        if (!(highScore < transform.position.y)) return;
        highScore = transform.position.y;
        OnHighScoreChanged?.Invoke(highScore);

    }
    private void Move(float inputDirection)
    {
        rotateDirection = inputDirection;
    }
    private void Boost(bool inputIsBoost)
    {
        isBoost = inputIsBoost;
    }

    public void ApplyMovement(float inputDirection)
    {
        Rotate(inputDirection);
    }

    public void ApplyBoost()
    {
        if (isBoost)
        {
            _rb2d.AddForce(transform.up * SPEED, ForceMode2D.Impulse);
            isBoost = false;
        }
    }

    private void Rotate(float inputDirection)
    {
        // 움직임에 따라 회전을 바꿈 -> 회전을 바꾸고 그 방향으로 발사를 해야 그쪽으로 가겠죠?
        float curRotaionZ = transform.rotation.z;
        transform.Rotate(Vector3.forward, -inputDirection * ROTATIONSPEED * Time.deltaTime);
    }
}