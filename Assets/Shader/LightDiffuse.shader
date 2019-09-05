Shader "Custom/Lighting/Emission"
{
	Properties
	{
		[Header(Diffuse)]
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Diffuse("Diffuse value", Range(0, 1)) = 1.0
		[Header(Emission)]
		_MainTex("Emissive Map", 2D) = "white" {}
		[HDR] _EmissionColor("Emission Color", Color) = (0,0,0)
		_Threshold("Threshold", Range(0., 1.)) = 1.
		[Dynamic]
		_Grid("Grid", range(1,50.)) = 30.
		_SpeedMax("Speed Max", range(0,30.)) = 20.
		_SpeedMin("Speed Min", range(0,10.)) = 2.
		_Density("Density", range(0,30.)) = 5.
	}
		SubShader
		{
			Tags{ "LightMode" = "ForwardBase" "RenderType"="Opaque"}

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				float noise(float x)
				{
					return frac(sin(x) * 43758.5453);
					return frac(sin(x) * 4100);
				}

				float noise(float2 vect)
				{
					// frac - returns the fractional portion of a scalar or each vector component
					return frac(sin(dot(vect, float2(5372.156, 8452.751))) * 1643.268);
				}

				float texelValue(float2 ipos, float n) {
					for (float i = 0.; i < 5.; i++) {
						for (float j = 0.; j < 3.; j++)
						{
							if (i == ipos.y && j == ipos.x) {
								// fmod returns the remainder of x/y with the same sign as x
								return step(1., fmod(n, 2.)); 
							}

							n = ceil(n / 2.);
						}
					}
					return 0.;
				}

				float _Density;

				float char(float2 st, float n) {
					st.x = st.x * 2. - .5;
					st.y = st.y * 1.2 - .1;

					float2 ipos = floor(st * float2(3., 5.));

					n = floor(fmod(n, 20. + _Density));

					float digit = 0.0;

					if (n < 1.) { digit = 9712.; }
					else if (n < 2.) { digit = 21158.0; }
					else if (n < 3.) { digit = 25231.0; }
					else if (n < 4.) { digit = 23187.0; }
					else if (n < 5.) { digit = 23498.0; }
					else if (n < 6.) { digit = 31702.0; }
					else if (n < 7.) { digit = 25202.0; }
					else if (n < 8.) { digit = 30163.0; }
					else if (n < 9.) { digit = 18928.0; }
					else if (n < 10.) { digit = 23531.0; }
					else if (n < 11.) { digit = 29128.0; }
					else if (n < 12.) { digit = 17493.0; }
					else if (n < 13.) { digit = 7774.0; }
					else if (n < 14.) { digit = 31141.0; }
					else if (n < 15.) { digit = 29264.0; }
					else if (n < 16.) { digit = 3641.0; }
					else if (n < 17.) { digit = 31315.0; }
					else if (n < 18.) { digit = 31406.0; }
					else if (n < 19.) { digit = 30864.0; }
					else if (n < 20.) { digit = 31208.0; }
					else { digit = 1.0; }

					float tex = texelValue(ipos, digit);

					float2 borders = float2(1., 1.);
					borders *= step(0., st) * step(0., 1. - st);

					return step(.1, 1. - tex) * borders.x * borders.y;
				}

				float _Grid;
				float _SpeedMax;
				float _SpeedMin;

				struct v2f {
					float4 pos : SV_POSITION;
					fixed4 col : COLOR0;
					float2 uv : TEXCOORD0;
				};

				fixed4 _Color;
				fixed4 _LightColor0;
				float _Diffuse;

				sampler2D _MainTex;
				float4 _MainTex_ST;

				v2f vert(appdata_base v) {
					v2f o;
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
					float3 worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
					float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
					float NdotL = max(0.0, dot(worldNormal, lightDir));
					fixed4 diff = _Color * NdotL * _LightColor0 * _Diffuse;
					o.col = diff;
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					return o;
				}

				float4 _EmissionColor;
				float _Threshold;

				fixed4 frag(v2f i) : SV_Target{
					float2 ipos = floor(i.uv * _Grid);
					float2 fpos = frac(i.uv * _Grid);

					ipos.y += floor(_Time.y * max(_SpeedMin, _SpeedMax * noise(ipos.x)));
					float charNum = noise(ipos);
					float val = char(fpos, (20. + _Density) * charNum);
					return fixed4(0, val, 0, 1.0);

					// fixed3 emi = tex2D(_MainTex, i.uv).r * _EmissionColor.rgb * _Threshold;
					// i.col.rgb += emi;
					// return i.col;
				}

					ENDCG
				}
	}
}
