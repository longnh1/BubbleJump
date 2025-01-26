Shader "Unlit/BubbleShader3D"
{
    Properties
    {
        _ThinFilmTex ("Thin-Film Texture", 2D) = "red" {}
        _DissolveTex ("Dissolve Texture", 2D) = "blue" {}
        _BubbleColor ("Bubble Color", Color) = (0.5, 0.5, 1.0, 1.0) // Local bubble color
        _ViewDir ("View Direction", Vector) = (0, 0, -1, 0) // Vector pointing straight ahead into the 2D scene
        _LightDirection ("Light Direction", Vector) = (0, 0, -1, 0) // The light source direction for specular reflections
        _LightColor ("Light Color", Color) = (1.0, 1.0, 1.0, 1.0) // Color of light used for specular reflections
        _Acceleration ("Acceleration", Vector) = (0.0, 0.0, 0.0) // Change this value in script to increase bubble wiggliness when accelerating
        _WiggleAmount ("Wiggle Amount", Float) = 1.0 // Change the amount of wiggle
        _WiggleAccelerationStrenght ("Wiggle Acceleration Strength", Float) = 1.0 // Amount of bubble wiggliness when accelerating
        _DissolveTextureOffset ("Dissolve UV offset", Vector) = (0.1, 0.0, 0.0) // Change this if the voronoi pattern looks wonky
        _PopAnimation ("Pop Animation", Float) = 0.0
        _BubblePopScale ("Bubble Pop Scale", Float) = 1.2
        [Toggle] _Debug ("Debug", Float) = 0.0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                //float3 worldNormal : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //float3 worldNormal : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float3 normal : NORMAL;
                float4 vertex : SV_POSITION;
            };

            uniform sampler2D _ThinFilmTex;
            uniform sampler2D _DissolveTex;
            uniform float4 _MainTex_ST;
            uniform float4 _BubbleColor;
            uniform float4 _ViewDir;
            uniform float4 _LightDirection;
            uniform float4 _LightColor;
            uniform float3 _Acceleration;
            uniform float2 _DissolveTextureOffset;
            uniform float _WiggleAmount;
            uniform float _WiggleAccelerationStrenght;
            uniform float _PopAnimation;
            uniform float _BubblePopScale;
            uniform float _Debug;

            static const float PI = 3.14159265;

            v2f vert (appdata v, float3 normal : NORMAL)
            {
                v2f o;
                // add squash and stretch in axis of acceleration   

                // add wiggle 
                float totalWiggleStrength = length(_Acceleration) * _WiggleAccelerationStrenght + _WiggleAmount;
                v.vertex.x += sin(_Time * 20.0 + v.vertex.x) * 0.025 * totalWiggleStrength;
                v.vertex.y += sin(_Time * 10.0 + v.vertex.y) * 0.025 * totalWiggleStrength + sin(_Time * 40.0 + v.vertex.x) * 0.0125 * totalWiggleStrength;

                // add scale-rebound when merging

                // add slight scaling before popping
                // [0, _BubblePopScale]
                // [1, _BubblePopScale + 1]
                v.vertex.xyz *= 1.0 + (_BubblePopScale - 1.0) * _PopAnimation;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = normal;

                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o, o.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = _BubbleColor;
                    
                // caluclate fresnel
                float fresnel = clamp(dot(i.normal, _ViewDir), 0.0, 1.0);
                col.a *= 1.0 - pow(fresnel, 0.2);

                // thin-film effect
                float2 UV_thinFilm;

                // use angle of each fragment normal to sample second dimension of thin-film texture
                float normalAngle = atan2(i.normal.x, i.normal.y) + PI; // [0, 2*PI]
                normalAngle /= 2 * PI; // [0, 1]

                UV_thinFilm.x = normalAngle;
                UV_thinFilm.y = fresnel;

                float4 thinFilmColor = tex2D(_ThinFilmTex, UV_thinFilm);
                col.rgb += thinFilmColor.rgb;

                // add voronoi texture for visual interest
                float4 dissolveColor = tex2D(_DissolveTex, i.uv + _DissolveTextureOffset * sin(_Time) * 0.1);
                col.a += dissolveColor.r * 0.2 * pow(fresnel, 2.0);
                


                // calculate specular reflections (Blinn-Phong)
                // calculate half-vector between view direction and light direction
                float3 halfVector = normalize(_ViewDir + _LightDirection);

                // compare half-vector with surface normal
                col.rgba += pow(max(dot(halfVector, i.normal), 0.0) * _LightColor, 50.0);
                col.rgba = clamp(col.rgba, 0.0, 1.0);

                // use voronoi texture for bubble popping animation
                col.a = lerp(col.a, dissolveColor.r, _PopAnimation) * (1.25 - _PopAnimation);


                // Color overwrite for debugging
                if (_Debug > 0.0) {

                    //col.rgb = i.normal * 0.5 + 0.5;
                    //col = thinFilmColor;
                    
                    //float4 dissolveColor = tex2D(_DissolveTex, i.uv + _DissolveTextureOffset * sin(_Time) * 0.1);
                    //col.a += dissolveColor.r * 0.2 * pow(fresnel, 2.0);

                    // use voronoi texture for bubble popping animation

                    col.rgb = thinFilmColor;
                    col.a = lerp(thinFilmColor.a, dissolveColor.r, _PopAnimation) * (1.25 - _PopAnimation);
                    
                }   

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }
            ENDCG
        }
    }
}
