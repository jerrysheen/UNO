Shader "ST/UI/RimLight"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _BorderSize ("Border Size", Range(0.0, 0.01)) = 0.01
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
                half4 main1 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv1);
                half4 main2 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv2);
                half4 main3 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv3);
                half4 main4 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv4);
                //return main1 + main;
                //half totalAlpha = 1.0;
                //totalAlpha = saturate(step(1.0,(main1.a + main2.a + main3.a + main4.a)));
                //return (totalAlpha) * half4(0.0, 1.0, 1.0, 1.0) * main;
                //half4 mask = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, i.uv);
                //return CombinedShapeLightShared(main, mask, i.lightingUV);


                float sum = main1.a + main2.a + main3.a + main4.a;
                float outline = min(sum, 1.0);
                half4 line_color = _Color;
                
                half4 res = lerp(main, line_color, outline - main.a);
                return res;
                if (main.a == 0.0)
		        discard;
                return _Color;
            }
            ENDHLSL
        }
    }

    Fallback "Sprites/Default"
}
