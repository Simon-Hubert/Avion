Shader "Hidden/Custom/Pixel"
{
    HLSLINCLUDE
    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    float _Factor;

    float4 frag(VaryingsDefault i) : SV_TARGET
    {
        float ratio = 1 / _Factor;
        float2 j = floor(i.texcoord / ratio) * ratio;
        float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, j);
        return col;
    }
    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            HLSLPROGRAM
            #pragma vertex VertDefault
            #pragma fragment frag
            ENDHLSL
        }
    }
}
