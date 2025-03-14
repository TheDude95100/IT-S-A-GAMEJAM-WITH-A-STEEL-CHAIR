Shader "Unlit/OutlineShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0.001, 0.1)) = 0.01
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100

        Pass
        {
            Name "OutlinePass"
            Tags { "LightMode"="UniversalForward" }

            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineThickness;

            // Vertex Shader : on ne touche pas aux vertices cette fois-ci
            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = v.uv;
                return o;
            }

            // Fonction pour vérifier l'alpha autour du pixel actuel
            float GetAlpha(sampler2D tex, float2 uv, float2 offset)
            {
                return tex2D(tex, uv + offset).a;
            }

            half4 frag (Varyings i) : SV_Target
            {
                float alpha = tex2D(_MainTex, i.uv).a;

                // Vérification des pixels autour (haut, bas, gauche, droite)
                float edge = 0.0;
                edge += GetAlpha(_MainTex, i.uv, float2(_OutlineThickness, 0));
                edge += GetAlpha(_MainTex, i.uv, float2(-_OutlineThickness, 0));
                edge += GetAlpha(_MainTex, i.uv, float2(0, _OutlineThickness));
                edge += GetAlpha(_MainTex, i.uv, float2(0, -_OutlineThickness));

                // Si on détecte une transition entre un pixel opaque et transparent, on affiche l'outline
                if (alpha < 0.1 && edge > 0.1)
                {
                    return _OutlineColor;
                }
                else
                {
                    return half4(0, 0, 0, 0); // Transparent sinon
                }
            }
            ENDHLSL
        }

        // Deuxième pass pour afficher le sprite normalement
        Pass
        {
            Name "MainPass"
            Tags { "LightMode"="UniversalForward" }

            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = v.uv;
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDHLSL
        }
    }
}
