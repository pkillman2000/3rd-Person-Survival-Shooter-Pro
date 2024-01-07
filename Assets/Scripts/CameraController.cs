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

        // Rotate Camera
        this.transform.eulerAngles = new Vector3(_currentPitch, _player.transform.eulerAngles.y, 0.0f);
    }
}
