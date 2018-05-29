 /// <summary>
 /// Post shader that creates a fullscreen vertical gradient using two colours.
 /// Gradients are generated using a top and bottom colour.
 /// </summary>

Shader "Post/VerticalGradient"
{
	Properties
	{
		_BlueNoise ("Blue Noise", 2D) = "white" {}
	    _TopColour("Top Colour", Color) = (1,0,0,1)
	    _BottomColour("Bottom Colour", Color) = (0,1,0,1)
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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
            
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _BlueNoise;

			float4 _TopColour;
			float4 _BottomColour;

			// Use blue noise to remove visible banding when changing gradients
			// Camera is fixed for the gradient so the noise does not need to be animated.
			// Screen Space Gradient Shader with Dithering in Unity : https://startupfreakgame.com/2017/01/15/screen-space-gradient-shader-with-dithering-in-unity/
			// Low Complexity, High Fidelity: The Rendering of INSIDE : https://www.youtube.com/watch?v=RdN06E6Xn9E
			float3 getNoise(float2 uv)
			{
				float3 noise = tex2D(_BlueNoise, uv);
				noise = noise * 2.0  - 0.5;
				return noise/255;
			}

			float4 frag (v2f i) : SV_Target
			{	
				float4 colour = lerp(_BottomColour, _TopColour, i.uv.y);
				return float4(colour.rgb + getNoise(i.uv), colour.a);
			}
			ENDCG
		}
	}
}
