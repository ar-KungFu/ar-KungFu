Shader "Coco/Skybox" {

    Properties {
        _TopColor ("Top Color", Color) = (0, 0, 0, 0)
        _BottomColor ("Bottom Color", Color) = (0, 0, 0, 0)

        [Space]
        _Yaw ("Yaw", Range (0, 180)) = 0
        _Pitch ("Pitch", Range (0, 180)) = 0

        [Space]
        _Intensity ("Intensity", Range (0, 2)) = 1.0
        _Exponent ("Exponent", Range (0, 3)) = 1.0
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    struct appdata {
        float4 position : POSITION;
        float3 texcoord : TEXCOORD0;
        float3 direction : TEXCOORD1;
    };
    
    struct v2f {
        float4 position : SV_POSITION;
        float3 texcoord : TEXCOORD0;
        float3 direction : TEXCOORD1;
    };
    
    half4 _TopColor;
    half4 _BottomColor;
    half _Intensity;
    half _Exponent;
    half _Yaw;
    half _Pitch;
    
    v2f vert (appdata v) {
        v2f o;
        o.position = UnityObjectToClipPos(v.position);
        o.texcoord = v.texcoord;

        half pitchRad = _Pitch / 360 * 2 * 3.141593;
        half yawRad = _Yaw / 360 * 2 * 3.141593;
        half3 direction = half3(sin(yawRad), cos(pitchRad), sin(pitchRad) * cos(yawRad));
        o.direction = direction;
        return o;
    }
    
    fixed4 frag (v2f i) : COLOR {
        const half d = dot(normalize(i.texcoord), i.direction) * 0.5f + 0.5f;
        return lerp (_TopColor, _BottomColor, pow(d, _Exponent)) * _Intensity;
    }

    ENDCG

    SubShader {
        Tags { "RenderType"="Background" "Queue"="Background" }

        Pass {
            ZWrite Off
            Cull Off
            Fog { Mode Off }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
}
