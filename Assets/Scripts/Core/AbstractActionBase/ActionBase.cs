using UnityEngine;

public abstract class ActionBase : MonoBehaviour
{
    [SerializeField] protected bool _executeOnStart;
    [SerializeField] protected bool _executeOnlyOnce;

    protected bool IsExecutedOnce;

    private void Start()
    {
        if (_executeOnStart)
        {
            Execute();
        }
    }
    public void Execute()
    {
        if (_executeOnlyOnce && IsExecutedOnce)
        {
            return;
        }
        IsExecutedOnce = true;
        ExecuteInternal();
    }
    protected abstract void ExecuteInternal();


}
