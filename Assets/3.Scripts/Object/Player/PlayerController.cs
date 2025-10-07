using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private PlayerInput input;
    private PlayerBuilding build;

    private void Awake()
    {
        var controller = GetComponent<CharacterController>();
        build = GetComponent<PlayerBuilding>();
        input = GetComponent<PlayerInput>();

        _inputReader = new NewInputReader(input.actions);
        _movement = new PlayerMovement(controller, moveSpeed, gravity, jumpHeight);
        _look = new FPSLook(Camera.main.transform, transform, mouseSensitivity);
        _tool = new Pickaxe();

        var manager = Managers.Instance;
        Managers.Data.LoadAll(); // Temp
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

            if (ui.hotbar.slots[ui.hotbar.currentIndex].itemData != null && ui.hotbar.slots[ui.hotbar.currentIndex].itemData.itemType == ItemType.Build)
            {
                if (_inputReader.ReadLeftClick())
                {
                    build.Validate(build.nowMode);
                }
                if (_inputReader.ReadBuild())
                {
                    build.Build();
                }
                if (_inputReader.ReadEdit())
                {
                    build.Edit();
                }
                if (_inputReader.ReadDelete())
                {
                    build.Delete();
                }
                if (_inputReader.ReadRotate())
                {
                    build.Rotate();
                }
            }
            else
            {
                DetectInteractable();

                if (_inputReader.ReadLeftClick() && !_isMining)
                    TryMine();

                if (_inputReader.ReadInteract())
                    TryInteract();
            }

            if (Managers.UI.hotbar != null)
                Managers.UI.hotbar.WheelSlot(input.actions);
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

        if (_inputReader.ReadEscape())
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            ui.setting.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Attempts to mine a minable object
    /// </summary>
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

    /// <summary>
    /// Attempts to interact an interactable object
    /// </summary>
    private void TryInteract()
    {
        var camera = Camera.main;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, range))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(this);
                Debug.Log($"Interacted with {hit.collider.name}");
            }
        }
    }

    /// <summary>
    /// Detech to interact if interactable object is not null
    /// </summary>
    private void DetectInteractable()
    {
        var camera = Camera.main;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, range))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // Show text in ui
                Managers.UI.interactionText.SetText(interactable.GetInteractionText());
                Managers.UI.interactionText.gameObject.SetActive(true);
                return;
            }
        }

        Managers.UI.interactionText.gameObject.SetActive(false);
    }

    private IEnumerator MineRoutine(IMinable target)
    {
        _isMining = true;
        _tool.Use(target);
        yield return new WaitForSeconds(_tool.MiningTime);
        _isMining = false;
    }

    /// <summary>
    /// Visualizes the interact range in the editor
    /// </summary>
    private void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        Gizmos.color = Color.red;
        Vector3 camPos = Camera.main.transform.position;
        Vector3 camForward = Camera.main.transform.forward;

        Gizmos.DrawLine(camPos, camPos + camForward * range);
        Gizmos.DrawSphere(camPos + camForward * range, 0.1f);
    }
}
