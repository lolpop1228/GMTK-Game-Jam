using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 8f;
    public float sprintSpeed = 16f;
    public float acceleration = 10f;
    public float gravity = -35f;
    public float jumpForce = 16f;

    [Header("Head Bobbing")]
    public Transform cameraHolder;
    public float bobFrequency = 8f;
    public float bobAmplitude = 0.05f;
    public float sprintBobMultiplier = 1.5f;
    public float bobLerpSpeed = 10f;

    private Vector3 camStartLocalPos;
    private float bobTimer = 0f;
    private float previousBobOffset = 0f;

    [Header("Footstep Sound")]
    public AudioSource footstepAudioSource;
    public AudioClip footstepClip;

    private CharacterController controller;
    private Vector3 currentVelocity = Vector3.zero;
    private float yVelocity = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (cameraHolder != null)
            camStartLocalPos = cameraHolder.localPosition;
        if (footstepAudioSource == null)
            footstepAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (controller == null) return;

        bool grounded = controller.isGrounded;

        // Input
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        bool isMoving = input.magnitude > 0.1f;
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        float targetSpeed = isSprinting ? sprintSpeed : walkSpeed;
        Vector3 desiredDirection = transform.TransformDirection(input) * targetSpeed;

        // Smooth movement
        Vector3 horizontalVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
        horizontalVelocity = Vector3.Lerp(horizontalVelocity, desiredDirection, acceleration * Time.deltaTime);
        currentVelocity = new Vector3(horizontalVelocity.x, yVelocity, horizontalVelocity.z);

        // Jump & gravity
        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
                yVelocity = jumpForce;
            else if (yVelocity < 0f)
                yVelocity = -1f;
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }

        currentVelocity.y = yVelocity;
        controller.Move(currentVelocity * Time.deltaTime);

        // Head bobbing + footstep sound
        if (cameraHolder != null)
        {
            if (isMoving && grounded)
            {
                float speedMultiplier = isSprinting ? sprintBobMultiplier : 1f;
                bobTimer += Time.deltaTime * bobFrequency * speedMultiplier;

                float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude * speedMultiplier;
                Vector3 targetPos = camStartLocalPos + new Vector3(0, bobOffset, 0);
                cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, targetPos, bobLerpSpeed * Time.deltaTime);

                // Play footstep at peak (from downward to upward movement)
                if (previousBobOffset <= 0f && bobOffset > 0f)
                {
                    if (footstepAudioSource && footstepClip)
                        footstepAudioSource.PlayOneShot(footstepClip);
                }

                previousBobOffset = bobOffset;
            }
            else
            {
                // Reset bob
                cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, camStartLocalPos, bobLerpSpeed * Time.deltaTime);
                bobTimer = 0f;
                previousBobOffset = 0f;
            }
        }
    }
}
