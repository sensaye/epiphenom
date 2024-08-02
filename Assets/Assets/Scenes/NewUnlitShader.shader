Shader "Custom/SpotlightShaderWithGradient" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _SpotlightPos ("Spotlight Position", Vector) = (0,0,0,0)
        _SpotlightRadius ("Spotlight Radius", Float) = 0.2
        _GradientColor ("Gradient Color", Color) = (1, 1, 1, 1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _SpotlightPos;
            float _SpotlightRadius;
            float4 _GradientColor;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target {
                half4 texColor = tex2D(_MainTex, i.uv);

                float2 screenPos = i.uv * 2.0 - 1.0; // Convert from [0,1] to [-1,1]
                float distance = length(screenPos - _SpotlightPos.xy);

                float gradientFactor = smoothstep(_SpotlightRadius, 0, distance);
                half4 gradientColor = lerp(texColor, _GradientColor, gradientFactor);

                if (distance < _SpotlightRadius) {
                    return gradientColor;
                } else {
                    return half4(0, 0, 0, 1); // Karartýlmýþ alan
                }
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
