using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float range = 3f;

    private IMovement _movement;
    private IInputReader _inputReader;
    private ILook _look;
    private IMiningTool _tool;

    private bool _isMining = false;

    private InputSystem_Actions input;

    private void Awake()
    {
        var controller = GetComponent<CharacterController>();
        input = new InputSystem_Actions();

        _inputReader = new NewInputReader(input);
        _movement = new PlayerMovement(controller, moveSpeed, gravity, jumpHeight);
        _look = new FPSLook(Camera.main.transform, transform, mouseSensitivity);
        _tool = new Pickaxe();

        var manager = Managers.Instance;
    }

    private void Update()
    {
        var ui = Managers.UI;

        Vector3 inputDir = _inputReader.ReadMovement();
        Vector2 inputRotation = _inputReader.ReadRotation();

        Vector3 moveDir = Camera.main.transform.TransformDirection(inputDir);
        moveDir.y = 0;

        if (!ui.activeInven)
        {
            _look.Look(inputRotation);
            if (_inputReader.ReadLeftClick() && !_isMining)
                TryMine();
        }
        _movement.Move(moveDir);

        if (_inputReader.ReadJump())
            _movement.Jump();

        if (_inputReader.ReadTab())
        {
            ui.activeInven = !ui.activeInven;
            ui.inven.gameObject.SetActive(ui.activeInven);
            if (ui.activeInven)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }

        if (Managers.UI.hotbar != null)
            Managers.UI.hotbar.WheelSlot(input);
    }

    private void TryMine()
    {
        var camera = Camera.main;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, range))
        {
            var mineable = hit.collider.GetComponent<IMinable>();
            if (mineable != null)
                StartCoroutine(MineRoutine(mineable));
        }
    }

    private IEnumerator MineRoutine(IMinable target)
    {
        _isMining = true;
        yield return new WaitForSeconds(_tool.MiningTime);
        _tool.Use(target);
        _isMining = false;
    }
}
