using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComoponent : MonoBehaviour
{


    [Header("Player Atributes")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float moveSpeed;

    private float moveDirection;

    private Camera mainCamera;


    void Awake()
    {
        mainCamera = Camera.main;
    }


    void Start()
    {
           moveAction.action.Enable();
    }


    void Update()
    {
        if (moveAction == null) return;

        moveDirection = moveAction.action.ReadValue<float>();

        Vector3 movement = new Vector3(moveDirection * moveSpeed * Time.deltaTime, 0, 0);

        transform.Translate(movement);
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (rayHit.collider == null) return;
        
            
        Debug.Log("Clicked on: " + rayHit.collider.name);
        
    }
}
