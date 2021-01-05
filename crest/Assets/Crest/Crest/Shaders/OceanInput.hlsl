// Crest Ocean System

// This file is subject to the MIT License as seen in the root of this folder structure (LICENSE)

#ifndef CREST_OCEAN_INPUT_INCLUDED
#define CREST_OCEAN_INPUT_INCLUDED

#include "OceanConstants.hlsl"

/////////////////////////////
// Samplers

UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthTexture);
UNITY_DECLARE_SCREENSPACE_TEXTURE(_BackgroundTexture);

sampler2D _Normals;
sampler2D _ReflectionTex;
samplerCUBE _ReflectionCubemapOverride;
sampler2D _FoamTexture;
sampler2D _CausticsTexture;

/////////////////////////////
// Constant buffer: CrestPerMaterial

CBUFFER_START(CrestInputsPerMaterial)
half3 _Diffuse;
half3 _DiffuseGrazing;

half _RefractionStrength;
half4 _DepthFogDensity;

half3 _SubSurfaceColour;
half _SubSurfaceBase;
half _SubSurfaceSun;
half _SubSurfaceSunFallOff;

half _SubSurfaceDepthMax;
half _SubSurfaceDepthPower;
half3 _SubSurfaceShallowCol;
half3 _SubSurfaceShallowColShadow;

half _CausticsTextureScale;
half _CausticsTextureAverage;
half _CausticsStrength;
half _CausticsFocalDepth;
half _CausticsDepthOfField;
half _CausticsDistortionScale;
half _CausticsDistortionStrength;

half3 _DiffuseShadow;

half _NormalsStrength;
half _NormalsScale;

half3 _SkyBase, _SkyAwayFromSun, _SkyTowardsSun;
half _SkyDirectionality;

half _Specular;
half _Roughness;
half _DirectionalLightFallOff;
half _DirectionalLightFarDistance;
half _DirectionalLightFallOffFar;
half _DirectionalLightBoost;
half _FresnelPower;
float  _RefractiveIndexOfAir;
float  _RefractiveIndexOfWater;
half _PlanarReflectionNormalsStrength;
half _PlanarReflectionIntensity;

half _FoamScale;
float4 _FoamTexture_TexelSize;
half4 _FoamWhiteColor;
half4 _FoamBubbleColor;
half _FoamBubbleParallax;
half _ShorelineFoamMinDepth;
half _WaveFoamFeather;
half _WaveFoamBubblesCoverage;
half _WaveFoamNormalStrength;
half _WaveFoamSpecularFallOff;
half _WaveFoamSpecularBoost;
half _WaveFoamLightScale;

// Hack - due to SV_IsFrontFace occasionally coming through as true for backfaces,
// add a param here that forces ocean to be in undrwater state. I think the root
// cause here might be imprecision or numerical issues at ocean tile boundaries, although
// i'm not sure why cracks are not visible in this case.
float _ForceUnderwater;

float _HeightOffset;

CBUFFER_END

#endif // CREST_OCEAN_INPUT_INCLUDED
