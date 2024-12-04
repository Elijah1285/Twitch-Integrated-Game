using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool is_grounded;

    float camera_pitch = 0.0f;
    float accelerating_timer = 0.0f;
    float internal_speed_multiplier = 1.0f;
    float external_speed_multiplier = 1.0f;
    float air_speed_multiplier = 1.0f;

    Vector3 move_force;

    Rigidbody rb;

    MoveAudioState move_audio_state;

    [SerializeField] float start_external_speed_multiplier;
    [SerializeField] float sprint_speed_multiplier;
    [SerializeField] float sneak_speed_multiplier;
    [SerializeField] float movement_force;
    [SerializeField] float rotation_speed;
    [SerializeField] float jump_force;
    [SerializeField] float ground_check_radius;
    [SerializeField] float ground_drag;
    [SerializeField] float air_drag;
    [SerializeField] float air_speed_multiplier_air_value;
    [SerializeField] float max_speed;

    [SerializeField] Transform ground_check_transform;
    [SerializeField] Transform camera_transform;

    [SerializeField] AudioClip jump_sound;
    [SerializeField] AudioClip jump_pad_sound;
    [SerializeField] AudioClip walk_sound;
    [SerializeField] AudioClip sprint_sound;
    [SerializeField] AudioSource audio_source;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GetComponent<PlayerHealth>().setCurrentCheckpointExternalSpeedMultiplier(start_external_speed_multiplier);
        external_speed_multiplier = start_external_speed_multiplier;
    }

    void Update()
    {
        movePlayer();
        updateMoveAudio();
        rotatePlayerAndCamera();
        updateTimers();
        checkRespawn();
    }

    void movePlayer()
    {
        //get movement inputs
        //axis
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");

        //sprint/sneak
        if (Input.GetButton("Sprint") && is_grounded)
        {
            internal_speed_multiplier = sprint_speed_multiplier;
        }
        else if (Input.GetButton("Sneak") && is_grounded)
        {
            internal_speed_multiplier = sneak_speed_multiplier;
        }
        else if (!Input.GetButton("Sprint") && !Input.GetButton("Sneak") && is_grounded && accelerating_timer <= 0.0f)
        {
            internal_speed_multiplier = 1.0f;
        }

        //check if grounded before checking jump input
        is_grounded = Physics.CheckSphere(ground_check_transform.position, ground_check_radius, LayerMask.GetMask("Ground"));

        if (is_grounded)
        {
            rb.drag = ground_drag;
            air_speed_multiplier = 1.0f;
        }
        else
        {
            rb.drag = air_drag;
            air_speed_multiplier = air_speed_multiplier_air_value;
        }

        //jump
        if (Input.GetButtonDown("Jump") && is_grounded)
        {
            jump();
        }

        //apply movement inputs
        //calculate movement magnitude
        move_force.x = horizontal_input * movement_force * internal_speed_multiplier * external_speed_multiplier * air_speed_multiplier * Time.deltaTime;
        move_force.z = vertical_input * movement_force * internal_speed_multiplier * external_speed_multiplier * air_speed_multiplier * Time.deltaTime;

        //calculate movement direction
        move_force = transform.TransformDirection(move_force);

        rb.AddForce(move_force, ForceMode.Force);

        //check speed hasn't exceeded maximum
        Vector3 flat_velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        if (flat_velocity.magnitude > max_speed)
        {
            Vector3 limited_velocity = flat_velocity.normalized * max_speed;
            rb.velocity = new Vector3(limited_velocity.x, rb.velocity.y, limited_velocity.z);
        }

        Debug.Log(flat_velocity.magnitude);
    }

    void updateMoveAudio()
    {
        if (is_grounded && (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f) && internal_speed_multiplier == 1.0f)
        {
            move_audio_state = MoveAudioState.WALKING;
        }
        else if (is_grounded && (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f) && internal_speed_multiplier > 1.0f)
        {
            move_audio_state = MoveAudioState.SPRINTING;
        }
        else
        {
            move_audio_state = MoveAudioState.NO_AUDIO;
        }

        switch(move_audio_state)
        {
            case MoveAudioState.WALKING:
                {
                    audio_source.clip = walk_sound;
                    break;
                }

            case MoveAudioState.SPRINTING:
                {
                    audio_source.clip = sprint_sound;
                    break;
                }

            case MoveAudioState.NO_AUDIO:
                {
                    audio_source.clip = null;
                    return;
                }
        }

        if (!audio_source.isPlaying)
        {
            audio_source.Play();
        }
    }

    void rotatePlayerAndCamera()
    {
        //get rotation input
        float x_rotation_input = Input.GetAxis("Mouse X");
        float y_rotation_input = Input.GetAxis("Mouse Y");

        //apply rotation input
        Quaternion target_player_rotation = transform.rotation * Quaternion.Euler(0.0f, x_rotation_input * rotation_speed * Time.deltaTime, 0.0f);

        float target_camera_pitch = camera_pitch + (-y_rotation_input * rotation_speed * Time.deltaTime);
        camera_pitch = Mathf.Lerp(camera_pitch, target_camera_pitch, Time.deltaTime * rotation_speed);

        if (camera_pitch > 90.0f)
        {
            camera_pitch = 90.0f;
        }
        else if (camera_pitch < -90.0f)
        {
            camera_pitch = -90.0f;
        }

        //apply calculated rotation
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, target_player_rotation, Time.deltaTime * rotation_speed));
        
        camera_transform.localRotation = Quaternion.Euler(camera_pitch, 0.0f, 0.0f);
    }

    void updateTimers()
    {
        if (accelerating_timer > 0.0f)
        {
            accelerating_timer -= Time.deltaTime;
        }
    }

    void checkRespawn()
    {
        if (Input.GetButtonDown("Respawn"))
        {
            GetComponent<PlayerHealth>().respawn();
        }
    }

    void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);

        //play jump sound
        audio_source.PlayOneShot(jump_sound);
    }

    public void padJump(float override_jump_force, bool accelerate)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        rb.AddForce(Vector3.up * override_jump_force, ForceMode.Impulse);

        if (accelerate)
        {
            internal_speed_multiplier = sprint_speed_multiplier;
            accelerating_timer = 0.5f;
        }

        //play jump pad sound
        audio_source.PlayOneShot(jump_pad_sound);
    }

    public void setExternalSpeedMultiplier(float new_slow_down_multiplier)
    {
        external_speed_multiplier = new_slow_down_multiplier;
    }

    enum MoveAudioState
    {
        NO_AUDIO = 1,
        WALKING = 2,
        SPRINTING = 3
    }
}
