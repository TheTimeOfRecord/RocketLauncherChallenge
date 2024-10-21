using System.Linq;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    private int currentThresholdIndex;

    [SerializeField] private AchievementSO[] achievements;
    [SerializeField] private AchievementView achievementView;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        achievementView.CreateAchievementSlots(achievements);  // UI 생성
    }

    // 최고 높이를 달성했을 때 업적 달성 판단, 이벤트 기반으로 설계할 것
    private void CheckAchievement(float height)
    {
    }
}