Shader "Self-Illumin/DiffuseWithController" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_Illum ("Illumin (A)", 2D) = "white" {}
	_EmissionLM ("Emission (Lightmapper)", Float) = 0
	//_mousePositionx ("Mouse Position X", Float) = 0
	//_mousePositiony("Mouse Position Y", Float) = 0
 //   _screenWidth("Screen Width", Int) = 0
	//_screenHeight("Screen Height", Int) = 0
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 200
	
CGPROGRAM
#pragma surface surf Lambert

sampler2D _MainTex;
sampler2D _Illum;
fixed4 _Color;
//float _mousePositionx;
//float _mousePositiony;
//float _screenWidth;
//float _screenHeight;

struct Input {
	float2 uv_MainTex;
	float2 uv_Illum;
	float3 viewDir;
	float3 worldNormal;
	float4 screenPos;
	
	//float _mousePositionx;
	//float _mousePositiony;
	//float _screenWidth;
	//float _screenHeight;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 c = tex * _Color;
	//  o.Albedo = c.rgb;
	//	o.Emission = c.rgb * tex2D(_Illum, IN.uv_Illum).a;
    //	o.Alpha = c.a;
	

	//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
	//if (IN.worldNormal.x / IN.viewDir.x == IN.worldNormal.y / IN.viewDir.y && IN.worldNormal.y / IN.viewDir.y == IN.worldNormal.z / IN.viewDir.z)
    //if (normalize(IN.worldNormal.z) == -normalize (IN.viewDir.z) //
	if (IN.screenPos.y > 0 && IN.screenPos.x > 0
		) 
		//	|| 
		//(
		//
		

		//UNITY_MATRIX_IT_MV[2].xyz
		//&& normalize(IN.worldNormal.y) == normalize(IN.viewDir.y)
		//&& normalize(IN.worldNormal.x) == normalize(IN.viewDir.x)
	  
	{
		//o.Albedo = float3(IN.screenPos.x / IN.screenPos.w, IN.screenPos.y / IN.screenPos.w, IN.screenPos.z/ IN.screenPos.w);
		//o.Emission = float3(IN.screenPos.x / IN.screenPos.w, IN.screenPos.y / IN.screenPos.w, IN.screenPos.z / IN.screenPos.w);

		o.Albedo = float3(10, 0, 0);
		o.Emission = float3(10, 0, 0);

	}
	else
	{
		o.Albedo = float3(0, 0, 0);
		o.Emission = float3(0, 0, 0);
		//o.Albedo = float3(255, 255, 255);
		//o.Emission = float3(255, 255, 255);
	}
	
	//o.Albedo = float3(IN.worldNormal.x, IN.worldNormal.y, IN.worldNormal.z);
	//o.Albedo = IN.normal;
	//o.Albedo = IN.viewDir;



//	o.Emission = c.rgb * tex2D(_Illum, IN.uv_Illum).a;
	//o.Emission = float3(IN.worldNormal.x, IN.worldNormal.y, IN.worldNormal.z);
	//o.Emission = IN.viewDir;

	//o.Alpha = c.a;

	o.Alpha = 255;
}
ENDCG
} 
//FallBack "Self-Illumin/VertexLit"
}
