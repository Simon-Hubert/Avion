using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


[Serializable]
[PostProcess(typeof(PixelEffectRenderer), PostProcessEvent.AfterStack, "Custom/Pixel")]
public sealed class PixelEffect : PostProcessEffectSettings
{
    [Range(0,50)]
    public FloatParameter factor = new FloatParameter { value = 0f };
}

public sealed class PixelEffectRenderer : PostProcessEffectRenderer<PixelEffect>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Pixel"));
        sheet.properties.SetFloat("_Factor", settings.factor);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}