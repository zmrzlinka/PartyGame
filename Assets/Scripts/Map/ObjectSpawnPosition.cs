using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnPosition : MonoBehaviour
{
    public bool free { get; private set; }
    ScreenBounds screenBounds;
    ObjectSpawner spawner;
    private void Awake()
    {
        free = true;
    }
    public void Spawn(ThrowableObject objekt)
    {
        free = false;
        ThrowableObject spawned =  Instantiate(objekt);
        spawned.spawnerPos = spawner.objectParent;
        spawned.transform.SetParent(spawner.objectParent);
        spawned.transform.position = transform.position;
        spawned.Init(this,screenBounds,spawner);
        spawned.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void Release()
    {
        free = true;
    }
    public void Init(ScreenBounds bounds, ObjectSpawner spawener)
    {
        screenBounds = bounds;
        spawner = spawener;
    }
}
