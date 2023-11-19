using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapReceiver : MonoBehaviour
{
/*    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private GameObject _spawnObject2;*/

    [SerializeField] private List<GameObject> _spawnObjects;
    int _nSpawnCount = 0;

    private void Spawn(Vector3 spawnPosition)
    {
        GameObject spawnRef;
        
        spawnRef = Instantiate(_spawnObjects[_nSpawnCount % _spawnObjects.Count], spawnPosition, Quaternion.identity, this.transform);

        /*if (_nSpawnCount % 2 == 0)
        {
            spawnRef = Instantiate(_spawnObject, spawnPosition, Quaternion.identity);
        }
        else
        {
            spawnRef = Instantiate(_spawnObject2, spawnPosition, Quaternion.identity);
        }*/

        _nSpawnCount++;
        spawnRef.SetActive(true);
        Debug.Log("Spawn");
    }

    public void OnTap(object sender, TapEventArgs tapEventArgs)
    {
        if(tapEventArgs.HitObject == null)
        {
            Debug.Log("[TapReceiver]: Spawn");
            Ray ray = Camera.main.ScreenPointToRay(tapEventArgs.Position);
            this.Spawn(ray.GetPoint(10));
        }
    }


    void Start()
    {
        GestureManager.Instance.OnTap += this.OnTap;
    }

    void OnDisable()
    {
        GestureManager.Instance.OnTap -= this.OnTap;
    }

}
