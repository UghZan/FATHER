using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float x_speed = 5f;
    public float y_speed = 5f;
    public float move_speed_damp = 0.1f;

    public float sprintMultiplier = 2f;
    public float jumpForce = 1;

    public float sprintDrain = 1;
    public float jumpDrain = 5;

    public float x_turn_speed = 5f;
    public float y_turn_speed = 5f;

    public float cam_turn_max = 45f;
    public float cam_turn_min = -45f;

    public float breathThreshold = 0.2f;

    private Vector3 movementVector;
    private float x_mov;
    private float y_mov;

    private float x_rot;
    private float y_rot;

    public GameObject playerCamera;
    private Rigidbody _rigidbody;
    private CameraMovement _cameraMovement;

    private float speed;
    private float currentCamAngle;
    private float speedMultiplier;

    private bool isRunning;
    private Player player;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        _rigidbody = GetComponent<Rigidbody>();
        _cameraMovement = GetComponentInChildren<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.instance.cantmove)
        {
            if (isRunning)
                speedMultiplier = sprintMultiplier;
            else
                speedMultiplier = 1;
            x_mov = Mathf.Lerp(x_mov, Input.GetAxisRaw("Horizontal") * x_speed * speedMultiplier, Time.deltaTime * move_speed_damp);
            y_mov = Mathf.Lerp(y_mov, Input.GetAxisRaw("Vertical") * y_speed * speedMultiplier, Time.deltaTime * move_speed_damp);

            movementVector = transform.forward * y_mov + transform.right * x_mov;
            speed = movementVector.magnitude;

            if (Physics.Raycast(transform.position, -transform.up, 3.2f))
                isGrounded = true;
            else
                isGrounded = false;

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                if (player.HasEnoughStamina(10))
                {
                    _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                    player.ChangeStamina(-player.staminaDrainMul * jumpDrain, true);
                }

            if (Input.GetKey(KeyCode.LeftShift) && player.sprintCooldown <= 0)
            {
                if (player.HasEnoughStamina(10))
                {
                    isRunning = true;
                    player.ChangeStamina(-player.staminaDrainMul * Time.deltaTime * sprintDrain, true);
                }
                else
                {
                    player.sprintCooldown = 2;
                }
            }
            else
                isRunning = false;

            x_rot = Input.GetAxisRaw("Mouse X") * x_turn_speed;
            y_rot = Input.GetAxisRaw("Mouse Y") * y_turn_speed;

            Rotate(new Vector3(0, x_rot, 0));
            CamRotate(y_rot);

            if (speed < breathThreshold)
                _cameraMovement.Breath(1);
            else
                _cameraMovement.Sway(1);

        }
    }

    void FixedUpdate()
    {
            Move(movementVector);
    }

    public void Rotate(Vector3 rotate)
    {
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(rotate));
    }

    public void CamRotate(float rotateAngle)
    {
        currentCamAngle -= rotateAngle;
        currentCamAngle = Mathf.Clamp(currentCamAngle, cam_turn_min, cam_turn_max);
        playerCamera.transform.localRotation = Quaternion.Euler(currentCamAngle, 0, 0);
    }

    public void Move(Vector3 move)
    {
        _rigidbody.MovePosition(transform.position + movementVector * Time.deltaTime);
    }
}
