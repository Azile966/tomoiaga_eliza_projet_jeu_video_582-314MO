using ColliderEventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private BoolVariable doctorRescued;

    public void PlayGame()
    {
        if (doctorRescued == null)
        {
            Debug.LogError("MenuNavigation requires the doctor_rescued BoolVariable.", this);
            return;
        }

        doctorRescued.RuntimeValue = false;
        SceneManager.LoadScene("Niveau_1");
    }
}
