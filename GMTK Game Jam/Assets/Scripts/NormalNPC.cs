using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NormalNPC : MonoBehaviour
{
    public string firstDestinationTag = "Destination";
    public string secondDestinationTag = "Destination2";
    public float moveSpeed = 3f;
    public float stopDistance = 0.5f;

    private Rigidbody rb;
    private Animator animator;
    private Transform destination;

    private bool isWalking = false;
    private bool goingToSecondDestination = false;
    private bool reachedSecond = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        animator = GetComponent<Animator>();

        // Walk to first destination immediately
        SetDestinationByTag(firstDestinationTag);
        isWalking = true;
    }

    void FixedUpdate()
    {
        if (destination == null || !isWalking) return;

        float distance = Vector3.Distance(transform.position, destination.position);

        if (distance > stopDistance)
        {
            Vector3 direction = (destination.position - transform.position).normalized;
            Vector3 velocity = direction * moveSpeed;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
            }

            if (animator != null)
                animator.SetBool("isWalking", true);
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);

            if (animator != null)
                animator.SetBool("isWalking", false);

            if (goingToSecondDestination && !reachedSecond)
            {
                reachedSecond = true;
                Debug.Log($"{gameObject.name} reached SECOND destination and will disappear.");
                transform.rotation = Quaternion.Euler(0f, destination.eulerAngles.y, 0f);
                Destroy(gameObject, 1f);
            }
            else if (!goingToSecondDestination)
            {
                Debug.Log($"{gameObject.name} reached FIRST destination. Waiting for signal...");
                isWalking = false;
                transform.rotation = Quaternion.Euler(0f, destination.eulerAngles.y, 0f);
            }
        }
    }

    public void WalkToSecondDestination()
    {
        Debug.Log($"{gameObject.name} was told to walk to second destination.");
        SetDestinationByTag(secondDestinationTag);
        isWalking = true;
        goingToSecondDestination = true;
    }

    void SetDestinationByTag(string tag)
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(tag);
        if (targetObject != null)
        {
            destination = targetObject.transform;
        }
        else
        {
            Debug.LogWarning($"No object with tag '{tag}' found.");
        }
    }
}
