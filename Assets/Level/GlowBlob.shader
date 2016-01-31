// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32764,y:32696,varname:node_3138,prsc:2|emission-5712-OUT;n:type:ShaderForge.SFN_NormalVector,id:9751,x:31431,y:32907,prsc:2,pt:False;n:type:ShaderForge.SFN_Fresnel,id:6059,x:31838,y:32899,varname:node_6059,prsc:2|NRM-9751-OUT,EXP-3470-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3470,x:31586,y:33120,ptovrint:False,ptlb:Exp,ptin:_Exp,varname:node_3470,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_OneMinus,id:8794,x:32055,y:32899,varname:node_8794,prsc:2|IN-6059-OUT;n:type:ShaderForge.SFN_Multiply,id:8333,x:32287,y:32899,varname:node_8333,prsc:2|A-8794-OUT,B-1835-OUT;n:type:ShaderForge.SFN_Slider,id:1835,x:31913,y:33136,ptovrint:False,ptlb:Slider,ptin:_Slider,varname:node_1835,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Color,id:4908,x:31908,y:32638,ptovrint:False,ptlb:Glow_Color,ptin:_Glow_Color,varname:node_4908,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9448277,c2:1,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:6399,x:32134,y:32677,varname:node_6399,prsc:2|A-4908-RGB,B-1740-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1740,x:31981,y:32804,ptovrint:False,ptlb:Glow_Int,ptin:_Glow_Int,varname:node_1740,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Lerp,id:5712,x:32486,y:32691,varname:node_5712,prsc:2|A-9407-RGB,B-6399-OUT,T-8333-OUT;n:type:ShaderForge.SFN_Color,id:9407,x:32139,y:32492,ptovrint:False,ptlb:Base,ptin:_Base,varname:node_9407,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:3470-1835-4908-1740-9407;pass:END;sub:END;*/

Shader "Custom/GlowBlob" {
    Properties {
        _Exp ("Exp", Float ) = 1
        _Slider ("Slider", Range(0, 1)) = 1
        _Glow_Color ("Glow_Color", Color) = (0.9448277,1,0,1)
        _Glow_Int ("Glow_Int", Float ) = 3
        _Base ("Base", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _Exp;
            uniform float _Slider;
            uniform float4 _Glow_Color;
            uniform float _Glow_Int;
            uniform float4 _Base;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 emissive = lerp(_Base.rgb,(_Glow_Color.rgb*_Glow_Int),((1.0 - pow(1.0-max(0,dot(i.normalDir, viewDirection)),_Exp))*_Slider));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
