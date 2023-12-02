using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadWithLabels : MonoBehaviour
{
    [SerializeField] private List<string> _labels;
    private AsyncOperationHandle<IList<Material>> _handle;
    private int _index = 0;


    private void OnLoad(Material asset)
    {
        Debug.Log($"STUff: {asset.name}");

        switch (asset.name)
        {
            case "Card Back B":
                this.GetComponent<MeshRenderer>().material = asset;
                break;
        }
        this._index++;

    }

    private void OnComplete(AsyncOperationHandle<IList<Material>> _handle)
    {

    }

    void Start()
    {
        this._handle = Addressables.LoadAssetsAsync<Material>(this._labels, this.OnLoad, Addressables.MergeMode.Union, false);
        _handle.Completed += OnComplete;
    }

    void OnDestroy()
    {
        Addressables.Release(this._handle);
    }
}
