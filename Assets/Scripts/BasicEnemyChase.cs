using ColliderEventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public sealed class BasicEnemyChase : MonoBehaviour
{
    [SerializeField] private IntVariable playerHp;
    [SerializeField] private PlayerHealthBar playerHealthBar;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float detectionDistance = 12f;
    [SerializeField] private float attackDistance = 1.5f;
    [SerializeField] private float moveSpeed = 2.2f;
    [SerializeField] private float attackCooldown = 1f;

    private Transform player;
    private float nextAttackTime;

    private void Awake()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }

        if (characterController == null)
        {
            Debug.LogError("BasicEnemyChase requires a CharacterController.", this);
        }
    }

    private void Start()
    {
        if (playerHp == null)
        {
            Debug.LogError("BasicEnemyChase requires the player_hp IntVariable.", this);
        }

        if (playerHealthBar == null)
        {
            Debug.LogError("BasicEnemyChase requires the PlayerHealthBar component.", this);
        }

        FindPlayer();
    }

    private void Update()
    {
        if (characterController == null)
        {
            return;
        }

        if (player == null)
        {
            characterController.SimpleMove(Vector3.zero);
            return;
        }

        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f;
        float distanceToPlayer = directionToPlayer.magnitude;
        Vector3 horizontalVelocity = Vector3.zero;

        if (distanceToPlayer <= detectionDistance)
        {
            if (distanceToPlayer > attackDistance)
            {
                Vector3 direction = directionToPlayer / distanceToPlayer;
                horizontalVelocity = direction * moveSpeed;
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
            else
            {
                TryAttack();
            }
        }

        characterController.SimpleMove(horizontalVelocity);
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null)
        {
            Debug.LogError("BasicEnemyChase could not find a GameObject tagged Player.", this);
            return;
        }

        player = playerObject.transform;
    }

    private void TryAttack()
    {
        if (playerHp == null || playerHealthBar == null || Time.time < nextAttackTime)
        {
            return;
        }

        nextAttackTime = Time.time + attackCooldown;
        playerHp.RuntimeValue = Mathf.Max(0, playerHp.RuntimeValue - 1);
        playerHealthBar.RefreshDisplay();

        if (playerHp.RuntimeValue == 0)
        {
            SceneManager.LoadScene("Acceuil");
        }
    }
}
