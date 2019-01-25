Shader "Unlit/HouseShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Colour("Colour", Color) = (0.0, 0.0, 0.0, 0.0)
		_BackgroundColour("BackgroundColour", Color) = (0.0, 0.0, 0.0, 0.0)
		//_NearestObjectPosition("NearestObjectPosition", Vector) = (0.0, 0.0, 0.0, 0.0)
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"
				#include "Lighting.cginc"

				struct VS_Input
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					float3 normal: NORMAL;
				};

				struct PS_Input
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					float3 normal : TEXCOORD1;

					float3 nearestPosition : TEXCOORD2;
					float distance : TEXCOORD3;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float4 _Colour;
				float4 _BackgroundColour;
				uniform float4 _NearestObjectPosition;

				PS_Input vert(VS_Input input)
				{
					PS_Input output;
					/*o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					UNITY_TRANSFER_FOG(o,o.vertex);*/

					float amplitude = 1.5;
					float frequency = 3.5;
					float randomWaviness = 125;

					//float displacement = amplitude * sin((frequency * _Time.y) + (input.vertex.x * input.vertex.z * (input.vertex.y* randomWaviness)));
					//input.vertex.y += displacement;
					//input.vertex *= displacement * normal;
					output.vertex = UnityObjectToClipPos(input.vertex);
					output.normal = input.normal;

					output.uv = TRANSFORM_TEX(input.uv, _MainTex);

					output.nearestPosition = float4(_NearestObjectPosition.xyz, 1.0);
					float4 worldPosition = mul(unity_ObjectToWorld, input.vertex);
					output.distance = distance(worldPosition, output.nearestPosition);
					return output;
				}

				fixed4 frag(PS_Input input) : SV_Target
				{
					float density = 0.5f;
					float gradient = 3.0f;

					float visibility = exp(-pow((input.distance * density), -gradient));

					return lerp(tex2D(_MainTex, input.uv), _BackgroundColour, visibility);


					//	// sample the texture
					//	fixed4 col = tex2D(_MainTex, i.uv);
					//// apply fog
					//UNITY_APPLY_FOG(i.fogCoord, col);
					//return col;
				}
				ENDCG
			}
		}
}
