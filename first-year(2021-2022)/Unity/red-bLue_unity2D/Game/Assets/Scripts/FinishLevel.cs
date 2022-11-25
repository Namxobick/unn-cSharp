using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private const string SampleSceneName = "SampleScene";

    [SerializeField] private List<DeathPlatform> _platforms;
    [SerializeField] private PortalPool _portalPool;

    private void OnEnable()
    {
        _portalPool.OnAllPortalsAreFull += Finish;

        foreach (var platform in _platforms)
        {
            platform.OnPlayerTrigger += Finish;
        }
    }

    private void OnDisable()
    {
        _portalPool.OnAllPortalsAreFull -= Finish;

        foreach (var platform in _platforms)
        {
            platform.OnPlayerTrigger -= Finish;
        }
    }

    private void Finish()
    {
        SceneManager.LoadScene(SampleSceneName);
    }
}
