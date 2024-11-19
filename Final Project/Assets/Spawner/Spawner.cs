using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //Serialized Fields
    [SerializeField]
    [Tooltip("The waves that will instantiate from this spwan point")]
    WaveSO[] waves;

    //Cahse References
    MeshRenderer spawnPointVisaul;

    //Attributes
    bool isSpawningWave = false;

    private void Awake()
    {
        //Get the visual & hide it
        spawnPointVisaul = GetComponent<MeshRenderer>();
        spawnPointVisaul.enabled = false;
    }

    void Start()
    {
        //This is where I will subscribe to the OnWaveStart event

        //TESTING
        PlayWave(0);
    }

    void PlayWave(int index)
    {
        //Set index range that will be allowed
        if (index >= 0 && index < waves.Length)
        {
            //Spawn the wave
            StartCoroutine(SpawnWave(waves[index]));
        }
    }

    IEnumerator SpawnWave(WaveSO wave)
    {
        //Wait the initial start delay
        yield return new WaitForSeconds(wave.startDelay);

        //Instantiate each enemy at the spawn point poision
        foreach (GameObject enemy in wave.wave)
        {
            GameObject newObj = Instantiate(enemy, transform.position, Quaternion.identity);
            //Set parent to the spawner object
            newObj.transform.SetParent(transform, true);
            //Wait to spawn the next enemy
            yield return new WaitForSeconds(wave.delayBetweenEnemySpawn);
        }
    }
}
