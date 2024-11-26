using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool is_grounded;

    float camera_pitch = 0.0f;
    float accelerating_timer = 0.0f;
    float jump_timer = 0.0f;
    float internal_speed_multiplier = 1.0f;
    float external_speed_multiplier = 1.0f;

    Vector3 velocity;

    CharacterController character_controller;

    MoveAudioState move_audio_state;

    [SerializeField] float sprint_speed_multiplier;
    [SerializeField] float sneak_speed_multiplier;
    [SerializeField] float movement_speed;
    [SerializeField] float rotation_speed;
    [SerializeField] float jump_height;
    [SerializeField] float gravity;
    [SerializeField] float ground_check_radius;

    [SerializeField] Transform ground_check_transform;
    [SerializeField] Transform camera_transform;

    [SerializeField] AudioClip jump_sound;
    [SerializeField] AudioClip jump_pad_sound;
    [SerializeField] AudioClip walk_sound;
    [SerializeField] AudioClip sprint_sound;
    [SerializeField] AudioSource audio_source;

    void Start()
    {
        character_controller = GetComponent<CharacterController>();
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
        //check if grounded
        is_grounded = Physics.CheckSphere(ground_check_transform.position, ground_check_radius, LayerMask.GetMask("Ground"));

        //safety check
        if (character_controller.isGrounded && jump_timer <= 0.0f)
        {
            velocity.y = 0.0f;
        }

        //check if sprinting
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

        //get movement inputs
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");

        //apply movement inputs
        //calculate movement magnitude
        velocity.x = horizontal_input * movement_speed * internal_speed_multiplier * external_speed_multiplier * Time.deltaTime;
        velocity.z = vertical_input * movement_speed * internal_speed_multiplier * external_speed_multiplier * Time.deltaTime;

        //check jump input
        if (Input.GetButtonDown("Jump") && is_grounded)
        {
            jump();
        }

        //apply gravity
        if (!is_grounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        //reset y velocity if grounded
        else if (jump_timer <= 0.0f)
        {
            velocity.y = -3.0f;
        }

        //calculate movement direction
        velocity = transform.TransformDirection(velocity);

        //apply calculated movement
        character_controller.Move(velocity * Time.deltaTime);
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
        transform.rotation = Quaternion.Slerp(transform.rotation, target_player_rotation, Time.deltaTime * rotation_speed);
        
        camera_transform.localRotation = Quaternion.Euler(camera_pitch, 0.0f, 0.0f);
    }

    void updateTimers()
    {
        if (accelerating_timer > 0.0f)
        {
            accelerating_timer -= Time.deltaTime;
        }

        if (jump_timer > 0.0f)
        {
            jump_timer -= Time.deltaTime;
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
        jump_timer = 0.1f;

        //work out y velocity based on target jump height
        velocity.y = Mathf.Sqrt(jump_height * -2.0f * gravity);

        //play jump sound
        audio_source.PlayOneShot(jump_sound);
    }

    public void padJump(float override_jump_height, bool accelerate)
    {
        jump_timer = 0.1f;

        //work out y velocity based on target jump height
        velocity.y = Mathf.Sqrt(override_jump_height * -2.0f * gravity);

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
