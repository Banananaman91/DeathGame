Shader "Unlit/DebugTwoColour"
{
    Properties
    {
        _MainTex ("Albedo Texture (DO NOT TOUCH)", 2D) = "white" {}
        _OriginUvColour ("Origin UV Colour (0, 0)", Color) = (0, 0, 0, 1)
        _MaximumUvColour ("Maximum UV Colour (1, 1)", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags {"RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            fixed4 _OriginUvColour;
            fixed4 _MaximumUvColour;
            float4 _MainTex_ST;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 col = lerp(_OriginUvColour, _MaximumUvColour, (i.uv.x + i.uv.y) / 2.0);
                col.a = 1;
                return col;
            }
            ENDCG
        }
    }
}
