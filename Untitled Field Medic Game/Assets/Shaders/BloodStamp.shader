Shader "Hidden/BloodStamp"
{
    Properties { _MainTex ("Source", 2D) = "black" {} _StampPos ("Pos", Vector) = (0.5,0.5,0,0) _StampScale ("Scale", Float) = 0.02 }
    SubShader
    {
        Tags { "Queue"="Overlay" }
        Pass
        {
            Blend One One // Additive stamping
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            float4 _StampPos;
            float _StampScale;

            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float2 uv : TEXCOORD0; float4 vertex : SV_POSITION; };

            v2f vert (appdata v) {
                v2f o; o.vertex = UnityObjectToClipPos(v.vertex); o.uv = v.uv; return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float2 dist = abs(i.uv - _StampPos.xy);
                float mask = smoothstep(_StampScale, _StampScale * 0.3, dist.x + dist.y);
                // Soft circular splat, red channel only
                return fixed4(mask * 0.3, 0, 0, 1);
            }
            ENDCG
        }
    }
}