using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadWithReference : MonoBehaviour
{
    [SerializeField] private AssetReference _reference;

    private void OnComplete(AsyncOperationHandle handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            this.GetComponent<MeshRenderer>().material = (Material)handle.Result;
        }
        else
        {
            Debug.LogError($"{_reference.RuntimeKey}");
        }
    }

    void Start()
    {
        AsyncOperationHandle handle = this._reference.LoadAssetAsync<Material>();
        handle.Completed += OnComplete;
    }

    void OnDestroy()
    {
        this._reference.ReleaseAsset();
    }
}
