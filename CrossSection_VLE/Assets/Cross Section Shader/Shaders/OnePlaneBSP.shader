Shader "CrossSection/OnePlaneBSP" {

	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_CrossColor("Cross Section Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_PlaneNormal("PlaneNormal",Vector) = (0,1,0,0)
		_PlanePosition("PlanePosition",Vector) = (0,0,0,1)
		_StencilMask("Stencil Mask", Range(0, 255)) = 255
	}


    SubShader {
    
		Tags { 
            "RenderType"="Opaque"
            //"Queue" = "Transparent"  
        }
		//LOD 200
		Stencil{
			Ref [_StencilMask]
			CompBack Always
			PassBack Replace

			CompFront Always
			PassFront Zero
		}

        //表面を描画する: 視点と反対側のポリゴンをレンダリングしない. デフォルト
		Cull Back
        CGPROGRAM

        // Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows //alpha:fade 

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;         
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _CrossColor;
		fixed3 _PlaneNormal;
		fixed3 _PlanePosition;
        
		bool checkVisability(fixed3 worldPos){
			float dotProd1 = dot(worldPos - _PlanePosition, _PlaneNormal);
			return dotProd1 > 0  ;
		}
        
		void surf(Input IN, inout SurfaceOutputStandard o) {

           

            //Planeより上のオブジェクトの処理
            if (checkVisability(IN.worldPos)){
                discard;
                //o.Alpha = 0.7;
            }

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;

		}
		ENDCG




        ///*
        //裏面を描画する: 視点と同じ側のポリゴンをレンダリングしない。オブジェクトを反転するのに使用
        Cull Front
		CGPROGRAM
        
#pragma surface surf NoLighting noambient
//#pragma surface surf Lambert alpha

        sampler2D _MainTex;

        struct Input {
			half2 uv_MainTex;
			float3 worldPos;         
		};
        
		fixed4 _Color;
		fixed4 _CrossColor;
		fixed3 _PlaneNormal;
		fixed3 _PlanePosition;

		bool checkVisability(fixed3 worldPos){
			float dotProd1 = dot(worldPos - _PlanePosition, _PlaneNormal);
			return dotProd1 >0 ;
		}
        
		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten){
			fixed4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}

		void surf(Input IN, inout SurfaceOutput o){
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            o.Alpha = c.a;


            //Planeより上だったらの処理
			if (checkVisability(IN.worldPos)){
                discard;
                //o.Alpha = 0.3;
            }
			//o.Albedo = _CrossColor;

		}
        
		ENDCG
        //*/
		
	}
	//FallBack "Diffuse"
}
