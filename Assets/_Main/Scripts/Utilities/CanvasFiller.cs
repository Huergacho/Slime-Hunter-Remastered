using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Collections.Generic;

public class CanvasFiller : MonoBehaviour
{
    [SerializeField] protected float timeToShow;
    [SerializeField] protected Image imageToFill;
    [SerializeField] protected bool isPermanent = false;
    protected Coroutine showCanvas;
    protected Coroutine progressiveCanvas;
    [SerializeField] protected bool isProgressive = false;

    private void Start()
    {
        if (isPermanent)
        {
            FillImage();
        }
        else
        {
            ClearImage();
        }
    }

    protected virtual void EmptyImage()
    {
        imageToFill.fillAmount = 0;
    }

    protected virtual void FillImage()
    {
        imageToFill.fillAmount = 1;
    }
    protected virtual void ClearImage()
        {
            showCanvas = null;
            StopAllCoroutines();
            EmptyImage();
        }

    public void Reset()
    {
        FillImage();
    }

    public virtual void OnDestroy()
    {
        StopAllCoroutines();
    }
    //Muestra el canvas por un instante
    protected virtual IEnumerator ShowCanvasForAmountOfTime()
    {
        yield return new WaitForSeconds(timeToShow);
        ClearImage();
        showCanvas = null;
    }
    
    public virtual void UpdateCanvas(float currentValue, float maxValue)
    {
        var valueToFill = currentValue / maxValue;
        InstantCanvasFiller(valueToFill);
        if(isPermanent){return;}
        showCanvas ??= StartCoroutine(ShowCanvasForAmountOfTime());
    }

    protected virtual void InstantCanvasFiller(float currValue)
    {
        imageToFill.fillAmount = currValue;

    }
}