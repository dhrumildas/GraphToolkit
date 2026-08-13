using UnityEngine;

public class PlayerPitFall : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private TPP_Controller playerController;

    [Header("Fall")]
    [SerializeField] private Transform fallTarget;
    [SerializeField] private float fallSpeed = 7f;

    private bool falling;
    private CharacterController characterController;

    private void Start()
    {
        if (player != null)
            characterController = player.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!falling || player == null || fallTarget == null)
            return;

        player.transform.position =
            Vector3.MoveTowards(
                player.transform.position,
                fallTarget.position,
                fallSpeed * Time.deltaTime);

        if (Vector3.Distance(
                player.transform.position,
                fallTarget.position) <= 0.05f)
        {
            falling = false;

            Debug.Log("[PlayerPitFall] Player reached bottom of pit.");
        }
    }

    public void BeginFall()
    {
        if (falling)
            return;

        falling = true;

        Debug.Log("[PlayerPitFall] Player fall started.");

        if (playerController != null)
            playerController.enabled = false;

        if (characterController != null)
            characterController.enabled = false;

        Collider[] playerColliders =
            player.GetComponentsInChildren<Collider>();

        foreach (Collider col in playerColliders)
            col.enabled = false;
    }
}