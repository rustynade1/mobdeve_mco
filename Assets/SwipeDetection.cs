using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = .4f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = .9f;
    [SerializeField]
    private GameObject trail;

    private InputManager inputManager;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private Coroutine coroutine;

    private void Awake()
    {
        inputManager = InputManager.Instance;
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

    private void SwipeStart(Vector2 position, float time) { 
        startPosition = position;
        startTime = time;
        trail.SetActive(true);
        trail.transform.position = startPosition;
        coroutine= StartCoroutine(Trail());
    }

    private IEnumerator Trail() {
        while (true) {
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

    private void DetectSwipe() {
        float endMinusStart = endTime - startTime;

        Debug.Log("distance"+ Vector3.Distance(startPosition, endPosition));
        Debug.Log("endTime-startTime= " + endMinusStart);
        
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime) { 
            Debug.DrawLine(startPosition, endPosition, Color.black, 5f);
            Debug.Log("That was indeed a swipe");
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D= new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);

        }
    }

    private void SwipeDirection(Vector2 direction) {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold) {
            Debug.Log("Swipe up");
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Debug.Log("Swipe Down");
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Debug.Log("Swipe left");
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Debug.Log("Swipe right");
        }
    }
}
