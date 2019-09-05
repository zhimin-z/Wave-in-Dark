Shader "Custom/JellyEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DisplacementX("DisplacementX", range(0,1)) = 0
		_DisplacementY("DisplacementY", range(0,1)) = 0
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

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			sampler2D _MainTex;
			float _DisplacementX;
			float _DisplacementY;
			
			v2f vert (appdata_base v)
			{
				v2f o;
				v.vertex.x += sign(v.vertex.x) * sin(_Time.w) / 5 * _DisplacementX;
				v.vertex.y += sign(v.vertex.y) * cos(_Time.w) / 5 * _DisplacementY;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord;
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				return col;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
