using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class RocketControllerC : MonoBehaviour
{
    private EnergySystemC _energySystem;
    private RocketMovementC _rocketMovement;

    public Action<float> OnMoveEvent;
    public Action<bool> OnBoostEvent;
    
    private bool _isMoving;
    private float _movementDirection;
    private bool isTabed = false;
    private float doubleTabIntervalTime = 0f;

    private readonly float ENERGY_TURN = 0.5f;
    private readonly float ENERGY_BURST = 2f;

    private void Awake()
    {
        _energySystem = GetComponent<EnergySystemC>();
        _rocketMovement = GetComponent<RocketMovementC>();
    }

    private void Update()
    {
        ApplyDoubleTabIntervalTime();
    }

    private void FixedUpdate()
    {
        if (!_isMoving) return;
        
        if(!_energySystem.UseEnergy(Time.fixedDeltaTime * ENERGY_TURN)) return;
        
        _rocketMovement.ApplyMovement(_movementDirection);
    }

    // OnMove 구현
    // private void OnMove...
    private void OnMove(InputValue value)
    {
        float rotaionDirection = value.Get<float>();
        OnMoveEvent?.Invoke(rotaionDirection);
    }


    // OnBoost 구현
    // private void OnBoost...
    private void OnBoost(InputValue value)
    {
        if (isTabed)
        {
            OnBoostEvent?.Invoke(true);
            isTabed = false;
        }
        else
        {
            isTabed = true;
        }
    }
    private void ApplyDoubleTabIntervalTime()
    {
        doubleTabIntervalTime += Time.deltaTime * 0.2f;
        if (doubleTabIntervalTime > 0.2f)
        {
            isTabed = false;
            doubleTabIntervalTime = 0f;
        }
    }

}