using Unity.VisualScripting;
using UnityEngine;

public struct motorParts
{
    public Refrigerante Refrigerante;
}

public class Motor : MonoBehaviour
{
    [Header("Motor Parts")]
    [SerializeField] public GameObject Refrigerante;


    public motorParts PlayerMotorParts;


    void Start()
    {
        PlayerMotorParts.Refrigerante = Refrigerante.GetComponent<Refrigerante>();
    }

    
    void Update()
    {
        
    }


}
