
using UnityEngine;
using UnityEngine.Animations;

public class LookAtCam : MonoBehaviour
{

    private LookAtConstraint lookAt;


    private void Awake()
    {
        lookAt = GetComponent<LookAtConstraint>();
    }

    private void OnEnable()
    {
        if (lookAt.sourceCount > 0) return;

        ConstraintSource s = new ConstraintSource
        {
            sourceTransform = Camera.main.transform,
            weight = 1f
        };

        lookAt.AddSource(s);

        lookAt.constraintActive = true;
    }

    private void OnDisable()
    {
        lookAt.constraintActive = false;

        lookAt.RemoveSource(0);
    }
}


