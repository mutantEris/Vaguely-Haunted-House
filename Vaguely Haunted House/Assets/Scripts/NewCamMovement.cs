using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NewCamMovement : MonoBehaviour
{
    public Vector3 cam_offset;
    public Vector3 cam_rot_offset;
    public GameObject player_object;
    public GameObject camera_object;
    public bool enable_zoom;
    public KeyCode zoom_key;
    private bool is_sprinting;
    private bool is_zoomed;
    public bool hold_to_zoom;
    public float zoomStepTime = 5f;
    public float fov = 60f;
    public float zoomFOV = 30f;

    public Camera cam;
    public float bobSpeed = 10f;
    private float timer = 0;
    public float sprintSpeed = 7f;
    public float speedReduction = .5f;
    private bool is_crouched;
    private Vector3 jointOriginalPos;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    public float max_cam_up;
    public float max_cam_down;

    public float cam_x_sensitivity;
    public float cam_y_sensitivity;
    public bool enableHeadBob;

    public PlayerMover player_mover;

    // Start is called before the first frame update
    void Start()
    {
        jointOriginalPos = camera_object.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // set camera position and rotation to player object plus offsets
        camera_object.transform.position = player_object.transform.position + cam_offset;
        float delta_cam_pitch = -Input.GetAxis("Mouse Y") * cam_y_sensitivity; // forward/back
        float delta_cam_yaw = Input.GetAxis("Mouse X") * cam_x_sensitivity; // left/right
        Vector3 cam_now_ang = camera_object.transform.eulerAngles;
        camera_object.transform.eulerAngles = new Vector3(cam_now_ang.x + delta_cam_pitch, cam_now_ang.y + delta_cam_yaw, 0) + cam_rot_offset;
        // camera_object.transform.eulerAngles = new Vector3(math.clamp(cam_now_ang.x + delta_cam_pitch, -max_cam_down, -max_cam_up), cam_now_ang.y + delta_cam_yaw, 0) + cam_rot_offset;
        if(enableHeadBob)
            HeadBob();
        if (enable_zoom)
        {
            // Changes is_zoomed when key is pressed
            // Behavior for toogle zoom
            if(Input.GetKeyDown(zoom_key) && !hold_to_zoom && !is_sprinting)
            {
                if (!is_zoomed)
                {
                    is_zoomed = true;
                }
                else
                {
                    is_zoomed = false;
                }
            }

            // Changes is_zoomed when key is pressed
            // Behavior for hold to zoom
            if(hold_to_zoom && !is_sprinting)
            {
                if(Input.GetKeyDown(zoom_key))
                {
                    is_zoomed = true;
                }
                else if(Input.GetKeyUp(zoom_key))
                {
                    is_zoomed = false;
                }
            }

            // Lerps camera.fieldOfView to allow for a smooth transistion
            if(is_zoomed)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomFOV, zoomStepTime * Time.deltaTime);
            }
            else if(!is_zoomed && !is_sprinting)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, zoomStepTime * Time.deltaTime);
            }
        }
    }

    private void HeadBob()
    {
        if(player_mover.is_moving)
        {
            // Calculates HeadBob speed during sprint
            if(is_sprinting)
            {
                timer += Time.deltaTime * (bobSpeed + sprintSpeed);
            }
            // Calculates HeadBob speed during crouched movement
            else if (is_crouched)
            {
                timer += Time.deltaTime * (bobSpeed * speedReduction);
            }
            // Calculates HeadBob speed during walking
            else
            {
                timer += Time.deltaTime * bobSpeed;
            }
            // Applies HeadBob movement
            camera_object.transform.position = new Vector3(camera_object.transform.position.x + Mathf.Sin(timer) * bobAmount.x, camera_object.transform.position.y + Mathf.Sin(timer) * bobAmount.y, camera_object.transform.position.z + Mathf.Sin(timer) * bobAmount.z);
        }
        else
        {
            // Resets when play stops moving
            timer = 0;
            camera_object.transform.position = new Vector3(Mathf.Lerp(camera_object.transform.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(camera_object.transform.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(camera_object.transform.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed));
        }
    }
}
