// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:True,rprd:True,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.8823529,fgcg:0.9513184,fgcb:1,fgca:1,fgde:0.003,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:5973,x:32719,y:32712,varname:node_5973,prsc:2|diff-7368-OUT,spec-8406-OUT,gloss-2325-OUT,normal-6047-OUT,emission-9249-OUT,clip-5266-OUT;n:type:ShaderForge.SFN_Tex2d,id:5349,x:31087,y:31707,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_236,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9529,x:31809,y:31612,varname:node_9529,prsc:2|A-5390-OUT,B-1966-RGB;n:type:ShaderForge.SFN_Color,id:1966,x:31619,y:31674,ptovrint:False,ptlb:Diffuse Color,ptin:_DiffuseColor,varname:node_3946,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:9012,x:31144,y:32649,ptovrint:False,ptlb:SRM,ptin:_SRM,varname:node_7213,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2417,x:31688,y:33300,ptovrint:False,ptlb:normal,ptin:_normal,varname:node_2746,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Multiply,id:2325,x:31578,y:32604,varname:node_2325,prsc:2|A-9012-G,B-4977-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7304,x:31370,y:32888,ptovrint:False,ptlb:Metallness,ptin:_Metallness,varname:node_1705,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:4977,x:31358,y:32648,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_4207,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Lerp,id:6047,x:32114,y:33382,varname:node_6047,prsc:2|A-5751-OUT,B-2417-RGB,T-4668-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4668,x:31807,y:33542,ptovrint:False,ptlb:normal str,ptin:_normalstr,varname:node_4668,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Vector1,id:5381,x:31154,y:32807,varname:node_5381,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector4,id:5751,x:31539,y:33493,varname:node_5751,prsc:2,v1:0,v2:0,v3:1,v4:0;n:type:ShaderForge.SFN_Multiply,id:8406,x:31578,y:32755,varname:node_8406,prsc:2|A-9012-B,B-7304-OUT;n:type:ShaderForge.SFN_Multiply,id:5266,x:32111,y:32942,varname:node_5266,prsc:2|A-5349-A,B-6358-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6358,x:31929,y:33028,ptovrint:False,ptlb:alpha clip,ptin:_alphaclip,varname:node_6358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Lerp,id:944,x:32095,y:31811,varname:node_944,prsc:2|A-9529-OUT,B-8361-OUT,T-9599-OUT;n:type:ShaderForge.SFN_Desaturate,id:5390,x:31469,y:31611,varname:node_5390,prsc:2|COL-5468-OUT,DES-7715-OUT;n:type:ShaderForge.SFN_Power,id:5468,x:31293,y:31538,varname:node_5468,prsc:2|VAL-5349-RGB,EXP-6811-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7715,x:31293,y:31459,ptovrint:False,ptlb:Diffuse Desaturate,ptin:_DiffuseDesaturate,varname:node_7715,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:6811,x:31087,y:31559,ptovrint:False,ptlb:Diffuse contrast,ptin:_Diffusecontrast,varname:node_6811,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Power,id:6030,x:31293,y:31832,varname:node_6030,prsc:2|VAL-5349-RGB,EXP-2713-OUT;n:type:ShaderForge.SFN_Desaturate,id:6711,x:31469,y:31832,varname:node_6711,prsc:2|COL-6030-OUT,DES-9016-OUT;n:type:ShaderForge.SFN_Multiply,id:8361,x:31809,y:31833,varname:node_8361,prsc:2|A-6711-OUT,B-500-RGB;n:type:ShaderForge.SFN_ValueProperty,id:9016,x:31293,y:32000,ptovrint:False,ptlb:Metall Desaturate,ptin:_MetallDesaturate,varname:node_9016,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Color,id:500,x:31619,y:31902,ptovrint:False,ptlb:Metall Color,ptin:_MetallColor,varname:node_500,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:2713,x:31104,y:31934,ptovrint:False,ptlb:Metall Contrast,ptin:_MetallContrast,varname:node_2713,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_SwitchProperty,id:7368,x:32579,y:32293,ptovrint:False,ptlb:Dirt ADD,ptin:_DirtADD,varname:node_7368,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-944-OUT,B-2677-OUT;n:type:ShaderForge.SFN_Lerp,id:2677,x:32408,y:32306,varname:node_2677,prsc:2|A-944-OUT,B-8385-RGB,T-4421-OUT;n:type:ShaderForge.SFN_Color,id:8385,x:32099,y:32291,ptovrint:False,ptlb:Dirt color,ptin:_Dirtcolor,varname:node_8385,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:4421,x:32248,y:32392,varname:node_4421,prsc:2|A-9012-A,B-8134-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8134,x:32099,y:32461,ptovrint:False,ptlb:Dirt Str,ptin:_DirtStr,varname:node_8134,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_SwitchProperty,id:9249,x:32253,y:33841,ptovrint:False,ptlb:Emission add,ptin:_Emissionadd,varname:node_9249,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-1347-OUT,B-5906-OUT;n:type:ShaderForge.SFN_Tex2d,id:6644,x:31869,y:33915,ptovrint:False,ptlb:emission,ptin:_emission,varname:node_6644,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-1854-OUT;n:type:ShaderForge.SFN_Vector1,id:1347,x:31869,y:33823,varname:node_1347,prsc:2,v1:0;n:type:ShaderForge.SFN_TexCoord,id:2857,x:31348,y:33945,varname:node_2857,prsc:2,uv:2;n:type:ShaderForge.SFN_SwitchProperty,id:1854,x:31603,y:33909,ptovrint:False,ptlb:Emission 3 channel,ptin:_Emission3channel,varname:_Emissionadd_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-2509-UVOUT,B-2857-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2509,x:31348,y:33805,varname:node_2509,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:5906,x:32075,y:33891,varname:node_5906,prsc:2|A-6644-RGB,B-4848-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4848,x:31870,y:34194,ptovrint:False,ptlb:emission str,ptin:_emissionstr,varname:node_4848,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:9599,x:31871,y:32149,varname:node_9599,prsc:2|A-8406-OUT,B-9109-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9109,x:31605,y:32250,ptovrint:False,ptlb:diff/mett color mask str,ptin:_diffmettcolormaskstr,varname:node_9109,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:5349-1966-6811-7715-9109-500-2713-9016-2417-9012-7304-4977-4668-6358-7368-8385-8134-9249-6644-1854-4848;pass:END;sub:END;*/

Shader "Custom/Base" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _DiffuseColor ("Diffuse Color", Color) = (1,1,1,1)
        _Diffusecontrast ("Diffuse contrast", Float ) = 1
        _DiffuseDesaturate ("Diffuse Desaturate", Float ) = 0
        _diffmettcolormaskstr ("diff/mett color mask str", Float ) = 1
        _MetallColor ("Metall Color", Color) = (1,1,1,1)
        _MetallContrast ("Metall Contrast", Float ) = 1
        _MetallDesaturate ("Metall Desaturate", Float ) = 0
        _normal ("normal", 2D) = "bump" {}
        _SRM ("SRM", 2D) = "white" {}
        _Metallness ("Metallness", Float ) = 1
        _Gloss ("Gloss", Float ) = 1
        _normalstr ("normal str", Float ) = 0
        _alphaclip ("alpha clip", Float ) = 1
        [MaterialToggle] _DirtADD ("Dirt ADD", Float ) = 0
        _Dirtcolor ("Dirt color", Color) = (0,0,0,1)
        _DirtStr ("Dirt Str", Float ) = 1
        [MaterialToggle] _Emissionadd ("Emission add", Float ) = 0
        _emission ("emission", 2D) = "white" {}
        [MaterialToggle] _Emission3channel ("Emission 3 channel", Float ) = 0
        _emissionstr ("emission str", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _DiffuseColor;
            uniform sampler2D _SRM; uniform float4 _SRM_ST;
            uniform sampler2D _normal; uniform float4 _normal_ST;
            uniform float _Metallness;
            uniform float _Gloss;
            uniform float _normalstr;
            uniform float _alphaclip;
            uniform float _DiffuseDesaturate;
            uniform float _Diffusecontrast;
            uniform float _MetallDesaturate;
            uniform float4 _MetallColor;
            uniform float _MetallContrast;
            uniform fixed _DirtADD;
            uniform float4 _Dirtcolor;
            uniform float _DirtStr;
            uniform fixed _Emissionadd;
            uniform sampler2D _emission; uniform float4 _emission_ST;
            uniform fixed _Emission3channel;
            uniform float _emissionstr;
            uniform float _diffmettcolormaskstr;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float3 tangentDir : TEXCOORD4;
                float3 bitangentDir : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _normal_var = UnpackNormal(tex2D(_normal,TRANSFORM_TEX(i.uv0, _normal)));
                float3 normalLocal = lerp(float4(0,0,1,0),float4(_normal_var.rgb,0.0),_normalstr);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                clip((_Diffuse_var.a*_alphaclip) - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _SRM_var = tex2D(_SRM,TRANSFORM_TEX(i.uv0, _SRM));
                float gloss = (_SRM_var.g*_Gloss);
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                UnityGI gi = UnityGlobalIllumination (d, 1, gloss, normalDirection);
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float node_8406 = (_SRM_var.b*_Metallness);
                float3 node_944 = lerp((lerp(pow(_Diffuse_var.rgb,_Diffusecontrast),dot(pow(_Diffuse_var.rgb,_Diffusecontrast),float3(0.3,0.59,0.11)),_DiffuseDesaturate)*_DiffuseColor.rgb),(lerp(pow(_Diffuse_var.rgb,_MetallContrast),dot(pow(_Diffuse_var.rgb,_MetallContrast),float3(0.3,0.59,0.11)),_MetallDesaturate)*_MetallColor.rgb),(node_8406*_diffmettcolormaskstr));
                float3 diffuseColor = lerp( node_944, lerp(node_944,_Dirtcolor.rgb,(_SRM_var.a*_DirtStr)), _DirtADD ); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, node_8406, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * unity_LightGammaCorrectionConsts_PIDiv4 );
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float2 _Emission3channel_var = lerp( i.uv0, i.uv2, _Emission3channel );
                float4 _emission_var = tex2D(_emission,TRANSFORM_TEX(_Emission3channel_var, _emission));
                float3 emissive = lerp( 0.0, (_emission_var.rgb*_emissionstr), _Emissionadd );
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _DiffuseColor;
            uniform sampler2D _SRM; uniform float4 _SRM_ST;
            uniform sampler2D _normal; uniform float4 _normal_ST;
            uniform float _Metallness;
            uniform float _Gloss;
            uniform float _normalstr;
            uniform float _alphaclip;
            uniform float _DiffuseDesaturate;
            uniform float _Diffusecontrast;
            uniform float _MetallDesaturate;
            uniform float4 _MetallColor;
            uniform float _MetallContrast;
            uniform fixed _DirtADD;
            uniform float4 _Dirtcolor;
            uniform float _DirtStr;
            uniform fixed _Emissionadd;
            uniform sampler2D _emission; uniform float4 _emission_ST;
            uniform fixed _Emission3channel;
            uniform float _emissionstr;
            uniform float _diffmettcolormaskstr;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float3 tangentDir : TEXCOORD4;
                float3 bitangentDir : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _normal_var = UnpackNormal(tex2D(_normal,TRANSFORM_TEX(i.uv0, _normal)));
                float3 normalLocal = lerp(float4(0,0,1,0),float4(_normal_var.rgb,0.0),_normalstr);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                clip((_Diffuse_var.a*_alphaclip) - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _SRM_var = tex2D(_SRM,TRANSFORM_TEX(i.uv0, _SRM));
                float gloss = (_SRM_var.g*_Gloss);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float node_8406 = (_SRM_var.b*_Metallness);
                float3 node_944 = lerp((lerp(pow(_Diffuse_var.rgb,_Diffusecontrast),dot(pow(_Diffuse_var.rgb,_Diffusecontrast),float3(0.3,0.59,0.11)),_DiffuseDesaturate)*_DiffuseColor.rgb),(lerp(pow(_Diffuse_var.rgb,_MetallContrast),dot(pow(_Diffuse_var.rgb,_MetallContrast),float3(0.3,0.59,0.11)),_MetallDesaturate)*_MetallColor.rgb),(node_8406*_diffmettcolormaskstr));
                float3 diffuseColor = lerp( node_944, lerp(node_944,_Dirtcolor.rgb,(_SRM_var.a*_DirtStr)), _DirtADD ); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, node_8406, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * unity_LightGammaCorrectionConsts_PIDiv4 );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _alphaclip;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
/////// Vectors:
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                clip((_Diffuse_var.a*_alphaclip) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
