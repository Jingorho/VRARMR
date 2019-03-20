﻿Shader "Custom/sample" {
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
#pragma surface surf Standard 
#pragma target 3.0

		struct Input {
		float3 worldPos;
	};

	void surf(Input IN, inout SurfaceOutputStandard o) {
		float dist = distance(fixed3(0,0,0), IN.worldPos);
		float radius = 2;
		if (radius < dist && dist < radius + 0.2) {
			o.Albedo = fixed4(1,1,1,1);
		}
		else {
			o.Albedo = fixed4(110 / 255.0, 87 / 255.0, 139 / 255.0, 1);
		}
	}
	ENDCG
	}
		FallBack "Diffuse"
}