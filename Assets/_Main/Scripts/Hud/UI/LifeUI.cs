using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : CanvasFiller
{
    [SerializeField] private LifeController owner;
    protected override void Start()
    {
        base.Start();
        Initialize(owner);
    }

    public void Initialize(LifeController controller)
    {
        SuscribeEvents();
        UpdateCanvas(controller.CurrentLife,controller.MaxLife);
        if(isPermanent){return;}
        ClearImage();
    }

    private void SuscribeEvents()
    {
        owner.OnModifyHealth += UpdateCanvas;
        owner.OnDie += ClearImage;
    }
}
