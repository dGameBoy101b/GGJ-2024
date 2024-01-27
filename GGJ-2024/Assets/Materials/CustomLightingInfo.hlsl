#ifndef CUSTOM_LIGHTING_INFO
#define CUSTOM_LIGHTING_INFO

void LightInfo_float(float3 WorldPos, out float3 Direction, out float Shadows)
{
    #ifdef SHADERGRAPH_PREVIEW
    Direction = normalize(float3(1,1,-0.4));
    Shadows = 1;
    #else

    float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
    
    Light mainLight = GetMainLight(shadowCoord);
    Direction = -mainLight.direction;

  
    Shadows = mainLight.shadowAttenuation;
    #endif
    
}


#endif