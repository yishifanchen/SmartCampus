Shader "Custom/Gradient" {

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert  

		#pragma target 3.0

	    float4 _Colors[100];//监测点颜色
		float _Points[100];//监测点位置
		int _Num;//监测点数量
		float value;

		struct Input {
			float2 uv_MainTex;
			float3 vertex;
		};

		void vert(inout appdata_full v, out Input IN) {
			UNITY_INITIALIZE_OUTPUT(Input, IN);
			IN.vertex = (v.vertex);  //顶点坐标转换成3d世界坐标    mul(unity_ObjectToWorld, v.vertex)
		}

		void surf (Input IN, inout SurfaceOutput o) {
			o.Alpha = 1;
			for(int i=1;i<_Num;i++){
				float length=_Points[i]-_Points[i-1];
				if(IN.vertex.x>=_Points[i-1]&&IN.vertex.x<=_Points[i]&&IN.vertex.y>-10){
					value=(IN.vertex.x-_Points[i-1])/length;
					o.Albedo=lerp(_Colors[i-1],_Colors[i],value);
				}
			}
			//o.Albedo=fixed3(1,0,1);
		}
		ENDCG
	}
	FallBack "Diffuse"
}