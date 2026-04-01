using UdonSharp;
using UnityEngine;
using VRC.Udon;

public class AnimatorSliderRelay : UdonSharpBehaviour
{
    public NetworkedMultiAnimatorSliderDriver driver;
    public int sliderIndex;

    public void OnSliderValueChanged()
    {
        if (driver == null)
        {
            return;
        }

        driver.HandleSliderChanged(sliderIndex);
    }
}