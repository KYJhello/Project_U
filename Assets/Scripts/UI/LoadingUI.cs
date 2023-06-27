using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text text;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
    }
    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }
    public void SetProgress(float progress)
    {
        slider.value = progress;
        text.text = progress.ToString() + " / 100";
    }
}
