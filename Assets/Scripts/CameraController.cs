using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _minPitch;
    [SerializeField] 
    private float _maxPitch;
    [SerializeField]
    private float _minYaw;
    [SerializeField]
    private float _maxYaw;

    [SerializeField]
    private Player _player;

    private float _mouseX;
    private float _mouseY;

    private float _currentPitch = 0;
    private float _currentYaw = 0;
    private float _calculatedYaw;

    void Update()
    {
        AdjustCameraOrientation();
    }

    private void AdjustCameraOrientation()
    {
        // Pitch
        _mouseY = Input.GetAxis("Mouse Y");
        _currentPitch = Mathf.Clamp(_currentPitch - _mouseY, _minPitch, _maxPitch);

        // Yaw
        _mouseX = Input.GetAxis("Mouse X");
        _currentYaw = Mathf.Clamp(_currentYaw - _mouseX, _minYaw, _maxYaw);
        // Add in player's rotation
        _calculatedYaw = _player.transform.eulerAngles.y + _currentYaw;

        // Rotate Camera
        this.transform.eulerAngles = new Vector3(_currentPitch, _calculatedYaw, 0.0f);
    }
}
