using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObject/Wave")]
public class WaveSO : ScriptableObject
{
    //This scriptable object will accept an array of objects and have a delay between enemy spawning
    public GameObject[] wave;

    [Min(0)]
    public float delayBetweenEnemySpawn;

    [Min(0)]
    public float startDelay;
}
