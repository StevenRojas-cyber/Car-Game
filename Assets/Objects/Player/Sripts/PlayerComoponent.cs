using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerComoponent : MonoBehaviour
{

    [Header("Player Atributes")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Motor playerMotor;


    [Header("Player Input Actions")]
    [SerializeField] private InputActionReference moveAction;


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


}
