using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [Header("Spawner Attributes")]
    public BoxCollider2D spawnArea;

    // CAMBIO CLAVE: Ahora es un Array (nota los corchetes []).
    // Esto te permitirá arrastrar varios obstáculos distintos desde el Inspector.
    public GameObject[] obstaclePrefabs;

    public float metersFrecuency;
    public float tiempoEntreSpawns = 2f;
    private float temporizador;

    void Start()
    {
        temporizador = tiempoEntreSpawns;
    }

    void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0f)
        {
            GenerarObstaculo();
            temporizador = tiempoEntreSpawns;
        }
    }

    void GenerarObstaculo()
    {
        // Medida de seguridad: verificamos que el array tenga al menos 1 obstáculo
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0)
        {
            Debug.LogWarning("¡Falta asignar los Obstacle Prefabs en el Inspector!");
            return;
        }
        if (spawnArea == null)
        {
            Debug.LogWarning("¡Falta asignar el Spawn Area en el Inspector!");
            return;
        }

        // --- LÓGICA DE VARIACIÓN ---
        // Generamos un número entero aleatorio desde el 0 hasta el tamaño de tu array.
        // Ojo: Random.Range con enteros (int) excluye el número máximo, así que si tienes 
        // 2 obstáculos (tamaño 2), esto devolverá 0 o 1 (los índices correctos).
        int indiceAleatorio = Random.Range(0, obstaclePrefabs.Length);

        // Seleccionamos el prefab de la lista usando ese número
        GameObject obstaculoElegido = obstaclePrefabs[indiceAleatorio];


        // --- LÓGICA DE POSICIÓN (Igual que antes) ---
        float limiteIzquierdo = spawnArea.bounds.min.x;
        float limiteDerecho = spawnArea.bounds.max.x;

        float posicionXAleatoria = Random.Range(limiteIzquierdo, limiteDerecho);
        Vector3 posicionDeAparicion = new Vector3(posicionXAleatoria, transform.position.y, transform.position.z);

        // Instanciamos el obstáculo que ganò el sorteo
        Instantiate(obstaculoElegido, posicionDeAparicion, Quaternion.identity);
    }
}