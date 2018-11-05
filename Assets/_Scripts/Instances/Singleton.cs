using UnityEngine;

public abstract class Singleton<T> : BaseSingleton where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    
    public override void AssignInstance()
    {
        Instance = GetComponent<T>();
    }

    protected virtual void Awake()
    {
        if(Instance && Instance != GetComponent<T>())
        {
            Destroy(this);
            return;
        }
        Init();
    }

    protected virtual void Init() { }
}