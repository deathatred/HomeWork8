Shader "Custom/UIOutlineUnlit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineSize ("Outline Size", Float) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _OutlineColor;
            float _OutlineSize;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 col = tex2D(_MainTex, uv) * _Color;

                float alpha = col.a;

                // Sample neighbors for outline
                float outline = 0.0;
                float2 offset = float2(_OutlineSize / _ScreenParams.x, _OutlineSize / _ScreenParams.y);

                outline += tex2D(_MainTex, uv + float2(offset.x, 0)).a;
                outline += tex2D(_MainTex, uv + float2(-offset.x, 0)).a;
                outline += tex2D(_MainTex, uv + float2(0, offset.y)).a;
                outline += tex2D(_MainTex, uv + float2(0, -offset.y)).a;

                fixed4 outlineCol = _OutlineColor;
                outlineCol.a *= step(0.01, outline) * (1 - alpha); // Show only where original is transparent

                return col + outlineCol;
            }
            ENDCG
        }
    }
}