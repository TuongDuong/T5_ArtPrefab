// unlit, vertex colour, alpha blended
// cull off

Shader "Custom/BlendVertexColorV3" 
{
	Properties 
	{
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Color ("Main Color", Color) = (1,1,1,1)
		_PatCol ("Pattern Color", Color) = (1,1,1,1)
		_NewColor ("New Color", Color) = (1,1,1,1)
		_Range ("Range", Range (0.0, 2.0)) = 0.01
		_HueRange ("Hue Range", Range (0.0, 4.0)) = 0.1
		_LightIntensity("Light intensity",Range (-2,2.0)) = 1
		_Sharpness("Sharpness", Range (-1.0,1.0)) = 0.25
	}
	
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite Off Lighting Off Cull Off Fog { Mode Off } Blend SrcAlpha OneMinusSrcAlpha
		LOD 110
		
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert_vct
			#pragma fragment frag_mult 
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;

			fixed4 _Color;
			fixed4 _PatCol;
			fixed4 _NewColor;
			half _Range;
			half _HueRange;
			half _Sharpness;
			half _LightIntensity;

			struct vin_vct 
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f_vct
			{
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			v2f_vct vert_vct(vin_vct v)
			{
				v2f_vct o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = v.texcoord;
				return o;
			}

			float3 rgb2hsv(float3 c) {
        float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
        float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
        float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

        float d = q.x - min(q.w, q.y);
        float e = 1.0e-10;
        return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
      }

      float3 hsv2rgb(float3 c) {
        c = float3(c.x, clamp(c.yz, 0.0, 1.0));
        float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
        float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
        return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
      }

			fixed4 frag_mult(v2f_vct i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.texcoord) * i.color;
				half hue = atan2(1.73205 * (c.g - c.b), 2 * c.r - c.g - c.b + 0.001);
				half targetHue = atan2(1.73205 * (_PatCol.g - _PatCol.b), 2 * _PatCol.r - _PatCol.g - _PatCol.b + 0.001);
				c.rgb = lerp(c.rgb,(_NewColor.rgb - _PatCol.rgb + c.rgb),
	                         sqrt(saturate(1 - ((c.r - _PatCol.r)*(c.r - _PatCol.r) + (c.g - _PatCol.g)*(c.g - _PatCol.g) + (c.b - _PatCol.b)*(c.b - _PatCol.b)) / (_Range * _Range))
				                  * saturate(1.0 - min(abs(hue-targetHue),6.28319 - abs(hue-targetHue))/(_HueRange * _HueRange))));
				fixed lum = saturate(Luminance(c.rgb) * _Sharpness);
				c.rgb = lerp(c.rgb, fixed3(lum,lum,lum), _LightIntensity);
				return c;
			}
			
			ENDCG
		} 
	}
}
