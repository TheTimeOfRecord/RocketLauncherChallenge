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
    private float doubleTabTimeInterval = 0f;

    private readonly float ENERGY_TURN = 0.5f;
    private readonly float ENERGY_BURST = 2f;

    private void Awake()
    {
        _energySystem = GetComponent<EnergySystemC>();
        _rocketMovement = GetComponent<RocketMovementC>();
    }

    private void Update()
    {
        ApplyDoubleTabTimeInterval();
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
        float rotaionDirection = value.Get<float>() > 0 ? 1 : -1;
        OnMoveEvent?.Invoke(rotaionDirection);
    }


    // OnBoost 구현
    // private void OnBoost...
    private void OnBoost(InputValue value)
    {
        doubleTabTimeInterval = 0f;
        Debug.Log("OnBoost");
        if (isTabed)
        {
            Debug.Log("OnBoost1");
            OnBoostEvent?.Invoke(true);
            isTabed = false;
        }
        else
        {
            Debug.Log("OnBoost2");
            isTabed = true;
        }
    }
    private void ApplyDoubleTabTimeInterval()
    {
        doubleTabTimeInterval += Time.deltaTime * 0.2f;
        if (doubleTabTimeInterval > 0.2f)
        {
            isTabed = false;
            doubleTabTimeInterval = 0f;
        }
    }

}