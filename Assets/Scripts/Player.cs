using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Joystick moveJoystick;
    [SerializeField] Joystick aimJoystick;
    [SerializeField] CharacterController characterController;
    [SerializeField] int moveSpeed;
    [SerializeField] int turnSpeed;
    private Animator _animator;
    Camera _mainCamera;
    
    [Header("Inventory")]
    [SerializeField] InventoryComponent inventoryComponent;
    
    
    Vector2 _moveInput;
    Vector2 _aimInput;
    private float _animatorTurnSpeed;
    CameraController _cameraController; 
    void Start()
    {
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
        aimJoystick.OnStickInputValueUpdatedEvent += AimInputUpdated;
        //aimJoystick.OnStickTapedEvent += SwitchWeapon;
        moveJoystick.OnStickInputValueUpdatedEvent += MoveInputUpdated;
        _cameraController = FindObjectOfType<CameraController>();
    }

    private void SwitchWeapon()
    {
        inventoryComponent.NextWeapon();
    }
    //attack
    public void AttackPoint()
    {
        inventoryComponent.GetCurrentWeapon().Attack();
    }
    void MoveInputUpdated(Vector2 inputValue)
    {
        _moveInput = inputValue;
    }
    void AimInputUpdated(Vector2 inputValue)
    {
        _aimInput= inputValue;
        if(_aimInput.magnitude!=0)
        {
            _animator.SetBool("Attacking", true);
        }
        else
        {
            _animator.SetBool("Attacking", false);
        }
    }
    Vector3 StickInputToWorldDirection(Vector2 stickInput)
    {
        Vector3 rightDir = _mainCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return rightDir * stickInput.x + upDir * stickInput.y;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchWeapon();
        }
        Vector3 moveDir = StickInputToWorldDirection(_moveInput);
        Vector3 aimDir= StickInputToWorldDirection(_aimInput);
        float forward= Vector3.Dot(moveDir, transform.forward);
        float right= Vector3.Dot(moveDir, transform.right);
        _animator.SetFloat("ForwardSpeed", forward);
        _animator.SetFloat("RightSpeed", right);
        characterController.Move( moveSpeed * Time.deltaTime * moveDir);
        RotateTowards(aimDir, moveDir);
        //UpdateCamera();
        //UpdateCamera();
    }
    private void UpdateCamera()
    {
        if (_moveInput.magnitude != 0 && _cameraController.transform != null && _aimInput.magnitude==0)
        {
            _cameraController.AddYawInput(_moveInput.x);
        }
    }
    private void RotateTowards(Vector3 aimDir, Vector3 moveDir)
    {
        float currentTurnSpeed = 0;
        if (aimDir.magnitude != 0)
        {
            Quaternion prevRotation = transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up),
                Time.deltaTime * turnSpeed);
            Quaternion newRotation = transform.rotation;
            float direction = Vector3.Dot(transform.right, aimDir) >0? 1:-1;
            float angle = Quaternion.Angle(prevRotation, newRotation)*direction;
             currentTurnSpeed = angle / Time.deltaTime;
            
        }
        else if (moveDir.magnitude != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir, Vector3.up),
                Time.deltaTime * turnSpeed);
        }
        _animatorTurnSpeed= Mathf.Lerp(_animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * 10);
        _animator.SetFloat("TurnSpeed", _animatorTurnSpeed);
    }
}
