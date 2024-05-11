using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
    public VariableJoystick _joystick;
    public CharacterController _controler;
    public Canvas _inputCanvas;
    public bool _isJoystick;
    [SerializeField] float _movementSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] Animator _playerAnimator;

    private void Start()
    {
        EnablaJoystickInput();
    }

    public void EnablaJoystickInput()
    {
        _isJoystick = true;
        _inputCanvas.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_isJoystick)
        {
            var movementDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
            _controler.SimpleMove(movementDirection * _movementSpeed);

            if (movementDirection.sqrMagnitude <= 0)
            {
                _playerAnimator.SetBool("run", false);
                return;
            }

            _playerAnimator.SetBool("run", true);
            var targetDirection = Vector3.RotateTowards(_controler.transform.forward, movementDirection, _rotationSpeed * Time.deltaTime, 0f);
            _controler.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }
}
