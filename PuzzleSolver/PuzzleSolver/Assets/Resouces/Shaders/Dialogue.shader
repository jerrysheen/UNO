Shader "ST/UI/Dialogue"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _ReadSpeed ("_ReadSpeed Size", Range(0.0, 1.0)) = 0.01
        _DiscardAlphaFrom ("_DiscardAlphaFrom Size", Range(0.0, 1.0)) = 0.2
        _ShowAlpha ("_ShowAlpha Size", Range(0.0, 1.0)) = 0.9
        _ShowControlValue ("_ShowControlValue", Range(0.0, 10)) = 5.5
        _MainTex("Diffuse", 2D) = "white" {}
        //_MaskTex("Mask", 2D) = "white" {}
        //_NormalMap("Normal Map", 2D) = "bump" {}

        // Legacy properties. They're here so that materials using this shader can gracefully fallback to the legacy sprite shader.
        [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
        [HideInInspector] _AlphaTex("External Alpha", 2D) = "white" {}
        [HideInInspector] _EnableExternalAlpha("Enable External Alpha", Float) = 0
    }

    HLSLINCLUDE
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    ENDHLSL

    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Tags { "LightMode" = "Universal2D" }
            HLSLPROGRAM
            #pragma vertex CombinedShapeLightVertex
            #pragma fragment CombinedShapeLightFragment
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_0 __
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_1 __
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_2 __
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_3 __

            struct Attributes
            {
                float3 positionOS   : POSITION;
                float4 color        : COLOR;
                float2  uv           : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4  positionCS  : SV_POSITION;
                half4   color       : COLOR;
                float2	uv          : TEXCOORD0;
                float2	uv1          : TEXCOORD2;
                float2	uv2          : TEXCOORD3;
                float2	uv3          : TEXCOORD4;
                float2	uv4          : TEXCOORD5;
                half2	lightingUV  : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_MaskTex);
            SAMPLER(sampler_MaskTex);
            TEXTURE2D(_NormalMap);
            SAMPLER(sampler_NormalMap);
            half4 _MainTex_ST;
            half4 _NormalMap_ST;
            half4 _Color;
            half _BorderSize;
            half _DiscardAlphaFrom;
            half _ShowAlpha;
            half _ShowControlValue;
            half _ReadSpeed;
            #if USE_SHAPE_LIGHT_TYPE_0
            SHAPE_LIGHT(0)
            #endif

            #if USE_SHAPE_LIGHT_TYPE_1
            SHAPE_LIGHT(1)
            #endif

            #if USE_SHAPE_LIGHT_TYPE_2
            SHAPE_LIGHT(2)
            #endif

            #if USE_SHAPE_LIGHT_TYPE_3
            SHAPE_LIGHT(3)
            #endif

            Varyings CombinedShapeLightVertex(Attributes v)
            {
                Varyings o = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.positionCS = TransformObjectToHClip(v.positionOS);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv1 = v.uv * _MainTex_ST.xy + _MainTex_ST.zw + half2(_BorderSize, 0);
                o.uv2 = v.uv * _MainTex_ST.xy + _MainTex_ST.zw + half2(-_BorderSize, 0);
                o.uv3 = v.uv * _MainTex_ST.xy + _MainTex_ST.zw + half2(0, -_BorderSize);
                o.uv4 = v.uv * _MainTex_ST.xy + _MainTex_ST.zw + half2(0, _BorderSize);
                float4 clipVertex = o.positionCS / o.positionCS.w;
                o.lightingUV = ComputeScreenPos(clipVertex).xy;
                o.color = v.color;
                return o;
            }

            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

            half4 CombinedShapeLightFragment(Varyings i) : SV_Target
            {
                half4 main = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                if(main.a < _DiscardAlphaFrom)
                {
                    discard;
                }
                float progress = - _ShowControlValue * i.uv.x + _ReadSpeed;
                progress = max(0, progress);
                return half4(main.xyz, min(progress, _ShowAlpha));

            }
            ENDHLSL
        }
    }

    Fallback "Sprites/Default"
}
