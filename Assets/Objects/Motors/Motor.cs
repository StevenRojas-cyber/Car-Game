using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct motorParts
{
    public GameObject Manija;
    public GameObject Cuerda;
    public GameObject Refrigeracion;
    public GameObject Hamster;
}

public class Motor : MonoBehaviour
{

    public virtual void motorPartDamaged()
    {
    
    }

    public virtual void motorPartRepaired()
    {

    }


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


}
