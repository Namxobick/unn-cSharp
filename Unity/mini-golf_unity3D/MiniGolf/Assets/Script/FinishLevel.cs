using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private const string SceneName = "Scene";

    [SerializeField] 
    private CrossingWithBall _planeDeathCrossingWithBall;

    [SerializeField] 
    private CrossingWithBall _holeCrossingWithBall;

    private void OnEnable()
    {
        _planeDeathCrossingWithBall.OnTriggerBall += Restart;
        _holeCrossingWithBall.OnTriggerBall += NextLevel;
    }

    private void OnDisable()
    {
        _planeDeathCrossingWithBall.OnTriggerBall -= Restart;
        _holeCrossingWithBall.OnTriggerBall -= NextLevel;
    }

    private void Restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneName);
    }
    
    private void NextLevel()
    {
        Debug.Log("Level passed");
    }

}
