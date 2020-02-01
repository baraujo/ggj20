using UnityEngine;
using UnityEngine.AddressableAssets;

public class AsyncLoader : MonoBehaviour
{
    public AssetReference sceneReference;
    private void Start()
    {
        Addressables.LoadSceneAsync(sceneReference);
    }
}
