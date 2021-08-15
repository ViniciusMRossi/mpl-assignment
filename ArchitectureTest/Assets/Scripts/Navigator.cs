using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour
{
    [SerializeField] private GameObject parentCanvas;
    
    private Animator _transitionAnimator;
    
    private static readonly int AnimateIn = Animator.StringToHash("AnimateIn");
    private static readonly int AnimateOut = Animator.StringToHash("AnimateOut");

    private string _callerScene;
    private string _targetScene;

    private bool _isLoadingScene = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(parentCanvas);
        _transitionAnimator = GetComponent<Animator>();
    }

    public void Navigate(string sceneName)
    {
        if (_isLoadingScene)
        {
            return;
        }
        
        _isLoadingScene = true;
        _callerScene = SceneManager.GetActiveScene().name;
        _targetScene = sceneName;
        _transitionAnimator.SetTrigger(AnimateIn);
    }

    public void OnAnimationInFinish()
    {
        StartCoroutine(LoadSceneAsync());
    }
    
    private IEnumerator LoadSceneAsync()
    {
        var async = SceneManager.LoadSceneAsync(_targetScene, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            yield return null;
        }
        
        SceneManager.UnloadSceneAsync(_callerScene);
        _transitionAnimator.SetTrigger(AnimateOut);

        _callerScene = "";
        _targetScene = "";
        
        _isLoadingScene = false;
    }

    public static Navigator Instance { get; private set; }
}
