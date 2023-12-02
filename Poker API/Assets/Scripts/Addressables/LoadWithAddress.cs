using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadWithAddress : MonoBehaviour
{
    [SerializeField]private string _address;
    private AsyncOperationHandle<Material> _handle;

    private void OnComplete(AsyncOperationHandle<Material> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            this.GetComponent<MeshRenderer>().material = handle.Result;
        }
        else
        {
            Debug.LogError($"{this._address}");
        }
    }

    void Start()
    {
        this._handle = Addressables.LoadAssetAsync<Material>(this._address);
        _handle.Completed += OnComplete;
    }

    void OnDestroy()
    {
        Addressables.Release(this._handle);
    }
}
