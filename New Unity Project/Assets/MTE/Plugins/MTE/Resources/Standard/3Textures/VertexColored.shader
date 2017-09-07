﻿Shader "MTE/Standard/3 Textures/VertexColored"
{
	Properties
	{
		_Control ("Control (RGBA)", 2D) = "red" {}
		_Splat0 ("Layer 1", 2D) = "white" {}
		_Splat1 ("Layer 2", 2D) = "white" {}
		_Splat2 ("Layer 3", 2D) = "white" {}

		[Gamma] _Metallic0("Metallic 0", Range(0.0, 1.0)) = 0.0
		[Gamma] _Metallic1("Metallic 1", Range(0.0, 1.0)) = 0.0
		[Gamma] _Metallic2("Metallic 2", Range(0.0, 1.0)) = 0.0
		_Smoothness0("Smoothness 0", Range(0.0, 1.0)) = 1.0
		_Smoothness1("Smoothness 1", Range(0.0, 1.0)) = 1.0
		_Smoothness2("Smoothness 2", Range(0.0, 1.0)) = 1.0
	}

	CGINCLUDE
		#pragma surface surf Standard vertex:MTE_SplatmapVert finalcolor:MTE_SplatmapFinalColor finalprepass:MTE_SplatmapFinalPrepass finalgbuffer:MTE_SplatmapFinalGBuffer
		#pragma multi_compile_fog

		struct Input
		{
			float4 tc_ControlSplat0 : TEXCOORD0;
			float4 tc_Splat12 : TEXCOORD1;
			UNITY_FOG_COORDS(2)
			float3 color : COLOR;
		};

		sampler2D _Control;
		float4 _Control_ST;
		sampler2D _Splat0,_Splat1,_Splat2;
		float4 _Splat0_ST,_Splat1_ST,_Splat2_ST;

		#define MTE_STANDARD_SHADER
		#include "UnityPBSLighting.cginc"
		#include "../../MTE Common.cginc"

		half _Metallic0;
		half _Metallic1;
		half _Metallic2;
		half _Smoothness0;
		half _Smoothness1;
		half _Smoothness2;

		void MTE_SplatmapVert(inout appdata_full v, out Input data)
		{
			UNITY_INITIALIZE_OUTPUT(Input, data);
			data.tc_ControlSplat0.xy = TRANSFORM_TEX(v.texcoord, _Control);
			data.tc_ControlSplat0.zw = TRANSFORM_TEX(v.texcoord, _Splat0);
			data.tc_Splat12.xy = TRANSFORM_TEX(v.texcoord, _Splat1);
			data.tc_Splat12.zw = TRANSFORM_TEX(v.texcoord, _Splat2);
			float4 pos = UnityObjectToClipPos (v.vertex);
			UNITY_TRANSFER_FOG(data, pos);
		}

		fixed4 MTE_SplatmapMix(Input IN, out half4 splat_control, half4 defaultAlpha)
		{
			splat_control = tex2D (_Control, IN.tc_ControlSplat0.xy);
			fixed4 mixedDiffuse = 0.0f;
			mixedDiffuse += splat_control.r * tex2D(_Splat0, IN.tc_ControlSplat0.zw) * half4(1.0, 1.0, 1.0, defaultAlpha.r);
			mixedDiffuse += splat_control.g * tex2D(_Splat1, IN.tc_Splat12.xy) * half4(1.0, 1.0, 1.0, defaultAlpha.g);
			mixedDiffuse += splat_control.b * tex2D(_Splat2, IN.tc_Splat12.zw) * half4(1.0, 1.0, 1.0, defaultAlpha.b);
			return mixedDiffuse;
		}

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			half4 splat_control;
			half4 defaultSmoothness = half4(_Smoothness0, _Smoothness1, _Smoothness2, 0);
			fixed4 mixedDiffuse = MTE_SplatmapMix(IN, splat_control, defaultSmoothness);
			o.Albedo = mixedDiffuse.rgb * IN.color.rgb;
			o.Alpha = 1;
			o.Smoothness = mixedDiffuse.a;
			o.Metallic = dot(splat_control, half4(_Metallic0, _Metallic1, _Metallic2, 0));
		}

	ENDCG

	Category
	{
		Tags
		{
			"Queue" = "Geometry-99"
			"RenderType" = "Opaque"
		}
		SubShader//for target 3.0+
		{
			CGPROGRAM
				#pragma target 3.0
			ENDCG
		}
		SubShader//for target 2.0
		{
			CGPROGRAM
			ENDCG
		}
	}

	Fallback "MTE/Standard/3 Textures/Diffuse"
	CustomEditor "MTE.MTEShaderGUI"
}
