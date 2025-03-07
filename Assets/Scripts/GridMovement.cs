using System.Collections;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private Vector2 gridSize = new Vector2(1, 1);
    public Vector3 playerDestination;
    public Vector3 startPosition;
    public Vector3 raycastDistance = new Vector3(1f,1.5f,0);
    public bool isMoving = false;
    public bool isWaitingForInput = false;
    private StatusMessageUI statusUI;


    void Start()
    {
        Camera.main.orthographicSize = (Screen.height / (32 * 2));
        statusUI = FindFirstObjectByType<StatusMessageUI>(); // Store reference once
    }

    void Update()
    {
        if (isMoving || isWaitingForInput) return; // Prevent movement spam
        
        Vector3 movementInput = Vector3.zero;
        bool keyPressed = false;

        // Check for first input
        if (Input.GetKeyDown(KeyCode.W)) { movementInput += new Vector3(0, gridSize.y, 0); keyPressed = true; }
        if (Input.GetKeyDown(KeyCode.S)) { movementInput += new Vector3(0, -gridSize.y, 0); keyPressed = true; }
        if (Input.GetKeyDown(KeyCode.A)) { movementInput += new Vector3(-gridSize.x, 0, 0); keyPressed = true; }
        if (Input.GetKeyDown(KeyCode.D)) { movementInput += new Vector3(gridSize.x, 0, 0); keyPressed = true; }

        if (keyPressed)
        {
            StartCoroutine(WaitForSecondInput(movementInput));
        }
    }

    IEnumerator WaitForSecondInput(Vector3 initialInput)
    {
        isWaitingForInput = true;
        Vector3 finalMovement = initialInput;
        bool secondInputDetected = false;

        float waitTime = 0.08f;
        while (waitTime > 0)
        {
            waitTime -= Time.deltaTime;

            if (!secondInputDetected) // Only allow 1 additional input
            {
                if (Input.GetKeyDown(KeyCode.W) && initialInput.y == 0) { finalMovement += new Vector3(0, gridSize.y, 0); secondInputDetected = true; }
                if (Input.GetKeyDown(KeyCode.S) && initialInput.y == 0) { finalMovement += new Vector3(0, -gridSize.y, 0); secondInputDetected = true; }
                if (Input.GetKeyDown(KeyCode.A) && initialInput.x == 0) { finalMovement += new Vector3(-gridSize.x, 0, 0); secondInputDetected = true; }
                if (Input.GetKeyDown(KeyCode.D) && initialInput.x == 0) { finalMovement += new Vector3(gridSize.x, 0, 0); secondInputDetected = true; }
            }

            yield return null;
        }

        // Move with final input
        playerDestination = transform.position + finalMovement;
        startPosition = transform.position;
        isWaitingForInput = false;
        StartCoroutine(MoveThePlayer());
    }

    IEnumerator MoveThePlayer()
    {
        isMoving = true;
        float moveSpeed = 2.5f;
        float t = 0f;

        RaycastHit2D hit = Physics2D.Raycast(startPosition, (playerDestination - startPosition).normalized, gridSize.x, LayerMask.GetMask("SolidObjects"));
        Debug.DrawRay(startPosition, (playerDestination - startPosition).normalized * gridSize.x, Color.red, 1f);

        if (hit.collider != null)
        {
            Debug.Log("Collision detected! Can't move.");
            isMoving = false;
            StartCoroutine(statusUI.ShowStatusMessage("Blocked")); // ✅ Corrected
            yield break;
        }

        else
        {
            // No collision, movement is allowed
            Debug.Log("Path is clear! Moving...");
        }
        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPosition, playerDestination, t);
            yield return null;
        }

        // **Snap to Grid to Fix Floating Point Issues**
        transform.position = new Vector3(
            Mathf.Round(transform.position.x / gridSize.x) * gridSize.x,
            Mathf.Round(transform.position.y / gridSize.y) * gridSize.y,
            transform.position.z
        );

        playerDestination = transform.position; // Ensure proper alignment
        isMoving = false;
        
    }
}
