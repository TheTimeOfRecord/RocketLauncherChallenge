using UnityEngine;
using UnityEngine.UI;

public class EnergyDashboardC : MonoBehaviour
{
    [SerializeField] private EnergySystemC energySystem;
    [SerializeField] private Image fillBar;
    private void Start()
    {
        // 에너지시스템의 에너지 사용에 대해 fillBar가 변경되도록 수정
    }

}