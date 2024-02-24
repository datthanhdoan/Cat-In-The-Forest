using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    public void OnMouseUpAsButton()
    {
        var effect = GetComponent<IEffect>();
        if (effect != null)
        {
            effect.Effect();
        }
    }
}