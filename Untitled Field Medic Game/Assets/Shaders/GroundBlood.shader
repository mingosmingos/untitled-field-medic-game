Shader "Custom/GroundBlood"
{
    Properties
    {
        _MainTex ("Ground", 2D) = "white" {}
        _BloodTex ("Blood Layer", 2D) = "black" {}
        _BloodIntensity ("Blood Opacity", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex, _BloodTex;
            float _BloodIntensity;

            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float2 uv : TEXCOORD0; float4 vertex : SV_POSITION; };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 ground = tex2D(_MainTex, i.uv);
                fixed4 blood = tex2D(_BloodTex, i.uv);
                // Blend: darker ground + red tint where blood is
                ground.rgb = lerp(ground.rgb, ground.rgb * fixed3(0.4, 0.1, 0.1), blood.r * _BloodIntensity);
                return ground;
            }
            ENDCG
        }
    }
}