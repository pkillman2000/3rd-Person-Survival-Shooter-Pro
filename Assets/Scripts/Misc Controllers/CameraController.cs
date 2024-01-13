using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * This script does not rotate the player from 
 * side to side.  That is handled in the Player
 * script because the player is physically
 * rotating and thus taking the camera along
 * for the ride.
 * 
 * Pitching the camera up/down does not affect
 * the player movement.
*/
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _minPitch;
    [SerializeField] 
    private float _maxPitch;
    [SerializeField]
    private float _cameraSpeed;


    [SerializeField]
    private Player _player;

    private float _mouseY;

    private float _currentPitch = 0;

    void Update()
    {
        AdjustCameraPitch();
    }

    private void AdjustCameraPitch()
    {
        // Pitch
        _mouseY = Input.GetAxis("Mouse Y") * _cameraSpeed;
        _currentPitch = Mathf.Clamp(_currentPitch - _mouseY, _minPitch, _maxPitch);

        // Rotate Camera on Y axis
        this.transform.eulerAngles = new Vector3(_currentPitch, _player.transform.eulerAngles.y, 0.0f);
    }
}
