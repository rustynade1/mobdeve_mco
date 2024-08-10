using UnityEngine;

public class PaperAnim : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeState(string stateName)
    {
        if (animator != null)
        {
            if (animator.HasParameterOfType(stateName, AnimatorControllerParameterType.Trigger))
            {
                animator.SetTrigger(stateName);
                Debug.Log($"State changed to: {stateName}");
            }
            else
            {
                Debug.LogError($"Trigger parameter '{stateName}' does not exist in the Animator.");
            }
        }
    }
}

public static class AnimatorExtensions
{
    public static bool HasParameterOfType(this Animator self, string name, AnimatorControllerParameterType type)
    {
        foreach (AnimatorControllerParameter param in self.parameters)
        {
            if (param.type == type && param.name == name)
            {
                return true;
            }
        }
        return false;
    }
}
