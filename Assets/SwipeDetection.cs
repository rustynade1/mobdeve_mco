using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minimumDistance = .4f;
    [SerializeField] private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)] private float directionThreshold = .9f;
    [SerializeField] private GameObject trail;
    [SerializeField] private PaperAnim paperAnim; // Reference to PaperAnim script
    [SerializeField] private PlayerAnimLogic playerAnimLogic; // Reference to PlayerAnimLogic script

    private InputManager inputManager;
    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;
    private Coroutine coroutine;

    private void Awake()
    {
        inputManager = InputManager.Instance;

        if (paperAnim == null)
        {
            paperAnim = FindObjectOfType<PaperAnim>();
        }

        if (playerAnimLogic == null)
        {
            playerAnimLogic = FindObjectOfType<PlayerAnimLogic>();
        }

        if (paperAnim == null)
        {
            Debug.LogError("PaperAnim is not assigned or found in the scene.");
        }
        else
        {
            Debug.Log("PaperAnim successfully assigned.");
        }

        if (playerAnimLogic == null)
        {
            Debug.LogError("PlayerAnimLogic is not assigned or found in the scene.");
        }
        else
        {
            Debug.Log("PlayerAnimLogic successfully assigned.");
        }
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
        trail.SetActive(true);
        trail.transform.position = startPosition;
        coroutine = StartCoroutine(Trail());
    }

    private IEnumerator Trail()
    {
        while (true)
        {
            trail.transform.position = inputManager.PrimaryPosition();
            yield return null;
        }
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        trail.SetActive(false);
        StopCoroutine(coroutine);
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            Debug.Log("Swipe up detected");

            // Trigger the state change in PaperAnim
            if (paperAnim != null)
            {
                paperAnim.ChangeState("submit"); // Change state to "submit"
                Debug.Log("paperAnim not null");
            }

            // Trigger the OnButtonClick in PlayerAnimLogic
            if (playerAnimLogic != null)
            {
                playerAnimLogic.OnButtonClick(); // Call the method
            }
        }
    }
}
