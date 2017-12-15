using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
	public static class Constants {

		static float cameraMovementSpeed = 10.0f;
		static float cameraMovementSideThickness = 0.01f;


		static float visionArcAngle = 2.0f;
		static float hearRadiusCoefficient = 0.01f; // hearRadius = hearRadiusCoefficient * LOS


		public static float CameraMovementSpeed {
			get {
				return cameraMovementSpeed;
			}
		}

		public static float CameraMovementSideThickness {
			get {
				return cameraMovementSideThickness;
			}
		}

		public static float VisionArcAngle {
			get {
				return visionArcAngle;
			}
		}

		public static float HearRadiusCoefficient {
			get {
				return hearRadiusCoefficient;
			}
		}
	}
}