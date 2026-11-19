using ColliderEventSystem;
using UnityEngine;

public sealed class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private IntVariable playerHp;
    [SerializeField] private GameObject healthSegment1;
    [SerializeField] private GameObject healthSegment2;
    [SerializeField] private GameObject healthSegment3;

    private void Start()
    {
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        if (playerHp == null)
        {
            Debug.LogError("PlayerHealthBar requires the player_hp IntVariable.", this);
            return;
        }

        if (healthSegment1 == null || healthSegment2 == null || healthSegment3 == null)
        {
            Debug.LogError("PlayerHealthBar requires all three health segment GameObjects.", this);
            return;
        }

        int displayedHp = Mathf.Clamp(playerHp.RuntimeValue, 0, 3);

        healthSegment1.SetActive(displayedHp >= 1);
        healthSegment2.SetActive(displayedHp >= 2);
        healthSegment3.SetActive(displayedHp >= 3);
    }
}
