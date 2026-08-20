using UnityEngine;

public class GuardPitFall : MonoBehaviour
{
    [Header("Guard")]
    [SerializeField] private GameObject guard;
    [SerializeField] private Pursuit pursuit;

    [Header("Forced Fall")]
    [SerializeField] private Transform fallTarget;
    //[SerializeField] private float triggerDistance = 1.2f;
    [SerializeField] private float fallSpeed = 7f;

    [Header("After Fall")]
    [SerializeField] private bool hideGuardAtBottom = true;
    [SerializeField] private GameObject keyOnFloor;
    [SerializeField] private VaultGuardSequence guardSequence;
    [SerializeField] private GameObject A;
    [SerializeField] private GameObject B;
    [SerializeField] private GameObject C;

    private bool falling;
    private bool fallCompleted;

    private CharacterController characterController;

    private void Start()
    {
        if (guard != null)
            characterController = guard.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (fallCompleted)
            return;

        if (guard == null || fallTarget == null)
            return;

        if (!falling)
        {
            //CheckForPit();
            return;
        }

        MoveGuardIntoPit();
    }

    //private void CheckForPit()
    //{
    //    Vector2 guardPos = new Vector2(
    //        guard.transform.position.x,
    //        guard.transform.position.z);

    //    Vector2 pitPos = new Vector2(
    //        transform.position.x,
    //        transform.position.z);

    //    float distance = Vector2.Distance(
    //        guardPos,
    //        pitPos);

    //    if (distance <= triggerDistance)
    //        BeginFall();
    //}


    private void OnTriggerEnter(Collider other)
    {
        if (falling || fallCompleted)
            return;

        if (other.CompareTag("Guard") ||
            other.transform.root.CompareTag("Guard"))
        {
            BeginFall();
        }
    }

    public void BeginFall()
    {
        if (falling)
            return;

        falling = true;

        Debug.Log("[GuardPitFall] Guard fall started.");

        if (pursuit != null)
            pursuit.enabled = false;

        if (characterController != null)
            characterController.enabled = false;

        Collider[] guardColliders =
            guard.GetComponentsInChildren<Collider>();

        foreach (Collider col in guardColliders)
            col.enabled = false;
    }

    private void MoveGuardIntoPit()
    {
        guard.transform.position =
            Vector3.MoveTowards(
                guard.transform.position,
                fallTarget.position,
                fallSpeed * Time.deltaTime);

        if (Vector3.Distance(
                guard.transform.position,
                fallTarget.position) <= 0.05f)
        {
            FinishFall();
        }
    }

    private void FinishFall()
    {
        falling = false;
        fallCompleted = true;

        Debug.Log("[GuardPitFall] Guard reached fall target.");

        if (hideGuardAtBottom && guard != null)
            guard.SetActive(false);

        if (keyOnFloor != null)
        {
            keyOnFloor.SetActive(true);
            A.SetActive(true);
            B.SetActive(true);
            C.SetActive(true);
            Debug.Log("[GuardPitFall] Key dropped.");
            FG2GameServices.Instance.RaiseEvent("GuardFellIntoPit",guard.transform);
        }
    }
}