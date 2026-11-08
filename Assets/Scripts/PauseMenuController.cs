using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button firstPauseButton;

    private InputAction pauseAction;
    private InputAction moveAction;
    private InputAction interactAction;
    private bool isPaused;

    private void Awake()
    {
        if (inputActions != null)
        {
            pauseAction = inputActions.FindAction("Gameplay/Pause");
            moveAction = inputActions.FindAction("Gameplay/Move");
            interactAction = inputActions.FindAction("Gameplay/Interact");
        }
    }

    private void OnEnable()
    {
        pauseAction?.Enable();
    }

    private void OnDisable()
    {
        pauseAction?.Disable();
        moveAction?.Enable();
        interactAction?.Enable();
    }

    private void Start()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (pauseAction != null && pauseAction.WasPressedThisFrame())
        {
            if (isPaused)
            {
                Resume();
                return;
            }

            Pause();
        }
    }

    private void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        moveAction?.Disable();
        interactAction?.Disable();
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(firstPauseButton.gameObject);
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        moveAction?.Enable();
        interactAction?.Enable();
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scene_MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}