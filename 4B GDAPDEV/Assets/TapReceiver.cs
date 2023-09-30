using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapReceiver : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;

    private void Spawn(Vector3 spawnPosition)
    {
        GameObject spawnRef = Instantiate(_spawnObject, spawnPosition, Quaternion.identity);
        spawnRef.SetActive(true);
        Debug.Log("Spawn");
    }

    public void OnTap(object sender, TapEventArgs tapEventArgs)
    {
        Ray ray = Camera.main.ScreenPointToRay(tapEventArgs.Position);
        this.Spawn(ray.GetPoint(10));
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
