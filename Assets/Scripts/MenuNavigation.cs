using ColliderEventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private BoolVariable doctorRescued;
    [SerializeField] private IntVariable playerHp;

    public void PlayGame()
    {
        if (doctorRescued == null)
        {
            Debug.LogError("MenuNavigation requires the doctor_rescued BoolVariable.", this);
            return;
        }

        if (playerHp == null)
        {
            Debug.LogError("MenuNavigation requires the player_hp IntVariable.", this);
            return;
        }

        doctorRescued.RuntimeValue = false;
        playerHp.RuntimeValue = 3;
        SceneManager.LoadScene("Niveau_1");
    }
}
