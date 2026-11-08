using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class PlantPickup : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private TMP_Text messageLabel;

    public static bool HasBeenCollected { get; private set; }

    private InputAction interactAction;
    private SpriteRenderer spriteRenderer;
    private Collider2D plantCollider;
    private bool playerNearby;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        plantCollider = GetComponent<Collider2D>();

        if (inputActions != null)
            interactAction = inputActions.FindAction("Gameplay/Interact");
    }

    private void OnEnable()
    {
        interactAction?.Enable();
    }

    private void OnDisable()
    {
        interactAction?.Disable();
    }

    private void Start()
    {
        if (HasBeenCollected)
        {
            spriteRenderer.enabled = false;
            return;
        }

        messageLabel.text = "";
        messageLabel.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerNearby &&
            interactAction != null &&
            interactAction.WasPressedThisFrame())
        {
            CollectPlant();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HasBeenCollected ||
            other.GetComponent<PlayerMovement>() == null)
        {
            ShowMessage("Sacred plant collected");
            return;
        }

        playerNearby = true;
        ShowMessage("Press E to collect");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() == null)
            return;

        playerNearby = false;

        messageLabel.text = "";
        messageLabel.transform.parent.gameObject.SetActive(false);
    }

    private void CollectPlant()
    {
        HasBeenCollected = true;
        spriteRenderer.enabled = false;
        playerNearby = false;

        ShowMessage("Sacred plant collected");
    }

    private void ShowMessage(string message)
    {
        messageLabel.text = message;
        messageLabel.transform.parent.gameObject.SetActive(true);
    }

    public static void ResetForNewGame()
    {
        HasBeenCollected = false;
    }
}