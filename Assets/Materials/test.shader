Shader "test" {
	Properties
	{
		_MainColor("_MainColor", Color) = (1,1,1,1)

	}
		SubShader
	{
		Lighting Off
		Fog{ Mode Off }
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha


		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		Pass
	{
		CGPROGRAM

		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"

			sampler2D _MaskTex;
			uniform fixed4 _MainColor;
			uniform float _Scale;

			struct vertOut
			{
				float4 pos : SV_POSITION;
				float3 lpos : TEXCOORD2;
			};


			vertOut vert(appdata_full input)
			{
				vertOut output;
				fixed4 pos = input.vertex;
				//pos.x = sin(_Time.z * pos.y) * sin(_Time.z * pos.z) * 0.1 + pos.x; funny
				/** random noise
				pos.x += (frac(sin(dot(float2(pos.x, _Time.x), float2(12.9898, 78.233))) * 43758.5453)) * 0.2;
				pos.y += (frac(sin(dot(float2(pos.y, _Time.y), float2(12.9898, 78.233))) * 43758.5453)) * 0.2;
				pos.z += (frac(sin(dot(float2(pos.z, _Time.z), float2(12.9898, 78.233))) * 43758.5453)) * 0.2; */
				


				output.lpos = input.vertex;
				output.pos = mul(UNITY_MATRIX_MVP, pos);
				return output;
			}

			fixed4 frag(vertOut input) : COLOR0
			{
				fixed4 output = fixed4(0,0,0,0);
				output.a = 0.5 * (sin(_Time.z) + 1);
				output.rgb = input.lpos * 2;

				return output;
			}

			ENDCG
		}
	}
}