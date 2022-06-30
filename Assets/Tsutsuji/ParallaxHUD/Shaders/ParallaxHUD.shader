Shader "Tsutsuji/ParallaxHUD" {
    Properties {
        [Enum(OFF,0,FRONT,1,BACK,2)] _Culling("Culling", Int) = 2
        _Tex1 ("Tex1", 2D) = "black" {}
        _Tex2 ("Tex2", 2D) = "black" {}
        _Tex3 ("Tex3", 2D) = "black" {}
        _Tex4 ("Tex4", 2D) = "black" {}
        [HDR]_Color1 ("Color1", Color) = (1,1,1,1)
        [HDR]_Color2 ("Color2", Color) = (1,1,1,1)
        [HDR]_Color3 ("Color3", Color) = (1,1,1,1)
        [HDR]_Color4 ("Color4", Color) = (1,1,1,1)
        [Space]
        _Height1 ("Height1", Range(0, 0.5)) = 0
        _Height2 ("Height2", Range(0, 0.5)) = 0
        _Height3 ("Height3", Range(0, 0.5)) = 0
        _Height4 ("Height4", Range(0, 0.5)) = 0
        [Space]
        _RotateSpeed1 ("RotateSpeed1", Range(-10, 10)) = 0
        _RotateSpeed2 ("RotateSpeed2", Range(-10, 10)) = 0
        _RotateSpeed3 ("RotateSpeed3", Range(-10, 10)) = 0
        _RotateSpeed4 ("RotateSpeed4", Range(-10, 10)) = 0
        [Space]
        _Displacement ("Displacement", Range(0, 5)) = 0
        _DissolveSpeed ("DissolveSpeed", Range(0, 5)) = 1
        _DissolveRotation ("DissolveRotation", Range(-1, 1)) = 0.2
        [HDR]_Background ("Background", Color) = (0,0,0,1)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull [_Culling]
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles metal
            #pragma target 3.0
            uniform sampler2D _Tex1; uniform float4 _Tex1_ST;
            uniform float _Height1;
            uniform float4 _Color1;
            uniform float4 _Color2;
            uniform sampler2D _Tex2; uniform float4 _Tex2_ST;
            uniform float _Height2;
            uniform float4 _Color3;
            uniform sampler2D _Tex3; uniform float4 _Tex3_ST;
            uniform float _Height3;
            uniform float _Displacement;
            uniform float _RotateSpeed3;
            uniform float _RotateSpeed2;
            uniform float _RotateSpeed1;
            uniform float _DissolveSpeed;
            float easeOut( float t ){
            t=t-1;
            return t*t*t+1;
            }
            
            float easeIn( float t ){
            return t*t*t;
            }
            
            uniform float _DissolveRotation;
            uniform float4 _Color4;
            uniform sampler2D _Tex4; uniform float4 _Tex4_ST;
            uniform float _Height4;
            uniform float _RotateSpeed4;
            uniform float4 _Background;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 node_476 = _Time;
                float node_2304_ang = (node_476.g*_RotateSpeed1);
                float node_2304_spd = 1.0;
                float node_2304_cos = cos(node_2304_spd*node_2304_ang);
                float node_2304_sin = sin(node_2304_spd*node_2304_ang);
                float2 node_2304_piv = float2(0.5,0.5);
                float2 node_2304 = (mul((_Displacement*(_Height1 - 0.5)*mul(tangentTransform, viewDirection).xy + i.uv0).rg-node_2304_piv,float2x2( node_2304_cos, -node_2304_sin, node_2304_sin, node_2304_cos))+node_2304_piv);
                float2 node_8652 = saturate(node_2304);
                float4 _Tex1_var = tex2D(_Tex1,TRANSFORM_TEX(node_8652, _Tex1));
                float node_3351_ang = (node_476.g*_RotateSpeed2);
                float node_3351_spd = 1.0;
                float node_3351_cos = cos(node_3351_spd*node_3351_ang);
                float node_3351_sin = sin(node_3351_spd*node_3351_ang);
                float2 node_3351_piv = float2(0.5,0.5);
                float2 node_3351 = (mul((_Displacement*(_Height2 - 0.5)*mul(tangentTransform, viewDirection).xy + i.uv0).rg-node_3351_piv,float2x2( node_3351_cos, -node_3351_sin, node_3351_sin, node_3351_cos))+node_3351_piv);
                float2 node_9491 = saturate(node_3351);
                float4 _Tex2_var = tex2D(_Tex2,TRANSFORM_TEX(node_9491, _Tex2));
                float node_3380_ang = (node_476.g*_RotateSpeed3);
                float node_3380_spd = 1.0;
                float node_3380_cos = cos(node_3380_spd*node_3380_ang);
                float node_3380_sin = sin(node_3380_spd*node_3380_ang);
                float2 node_3380_piv = float2(0.5,0.5);
                float2 node_3380 = (mul((_Displacement*(_Height3 - 0.5)*mul(tangentTransform, viewDirection).xy + i.uv0).rg-node_3380_piv,float2x2( node_3380_cos, -node_3380_sin, node_3380_sin, node_3380_cos))+node_3380_piv);
                float2 node_9976 = saturate(node_3380);
                float4 _Tex3_var = tex2D(_Tex3,TRANSFORM_TEX(node_9976, _Tex3));
                float node_7512_ang = (node_476.g*_RotateSpeed4);
                float node_7512_spd = 1.0;
                float node_7512_cos = cos(node_7512_spd*node_7512_ang);
                float node_7512_sin = sin(node_7512_spd*node_7512_ang);
                float2 node_7512_piv = float2(0.5,0.5);
                float2 node_6935 = (_Displacement*(_Height4 - 0.5)*mul(tangentTransform, viewDirection).xy + i.uv0);
                float2 node_7512 = (mul(node_6935.rg-node_7512_piv,float2x2( node_7512_cos, -node_7512_sin, node_7512_sin, node_7512_cos))+node_7512_piv);
                float2 node_3553 = saturate(node_7512);
                float4 _Tex4_var = tex2D(_Tex4,TRANSFORM_TEX(node_3553, _Tex4));
                float2 node_4779 = (saturate(node_6935.rg)*2.0+-1.0).rg;
                float4 node_9066 = _Time;
                float node_6113 = frac((((atan2(node_4779.g,node_4779.r)/6.28318530718)+0.5)-(node_9066.g*_DissolveRotation)));
                float node_9533 = (node_9066.g*_DissolveSpeed);
                float node_5918 = fmod(node_9533,2.0);
                float node_7948 = floor(node_5918);
                float3 emissive = ((_Tex1_var.rgb*_Color1.rgb)+(_Tex2_var.rgb*_Color2.rgb)+(_Tex3_var.rgb*_Color3.rgb)+((_Tex4_var.rgb*_Color4.rgb)*(step(node_6113,easeOut( max(frac(node_9533),node_7948) ))-step(node_6113,easeIn( (node_7948*(node_5918-1.0)) ))))+_Background.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
