﻿#region Copyright Information
// Sentience Lab Unity Framework
// (C) Sentience Lab (sentiencelab@aut.ac.nz), Auckland University of Technology, Auckland, New Zealand 
#endregion Copyright Information

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace SentienceLab
{
	/// <summary>
	/// Component to trigger Unity events on input actions.
	/// </summary>
	/// 
	[AddComponentMenu("SentienceLab/Events/InputAction Event")]
	public class InputActionEvent : MonoBehaviour
	{
		[Tooltip("InputAction that fires the events")]
		[FormerlySerializedAs("action")]
		public InputActionProperty Action;

		[System.Serializable]
		public struct Events
		{
			[Tooltip("Event fired when action is performed")]
			public UnityEvent OnActionPerformed;

			[Tooltip("Event fired when action is started")]
			public UnityEvent OnActionStarted;

			[Tooltip("Event fired when action is canceled")]
			public UnityEvent OnActionCanceled;
		}
		public Events events;


		public void Start()
		{
			// nothing to do here, just to have the "enable" flag
		}


		public void OnEnable()
		{
			if ((Action != null) && (Action.action != null))
			{
				Action.action.started   += OnActionStarted;
				Action.action.performed += OnActionPerformed;
				Action.action.canceled  += OnActionCanceled;
				Action.action.Enable();
			}
		}


		public void OnDisable()
		{
			if ((Action != null) && (Action.action != null))
			{
				Action.action.started   -= OnActionStarted;
				Action.action.performed -= OnActionPerformed;
				Action.action.canceled  -= OnActionCanceled;
			}
		}


		protected void OnActionStarted(InputAction.CallbackContext obj)
		{
			if (this.isActiveAndEnabled) events.OnActionStarted.Invoke();
		}


		protected void OnActionPerformed(InputAction.CallbackContext obj)
		{
			if (this.isActiveAndEnabled) PerformAction();
		}


		protected void OnActionCanceled(InputAction.CallbackContext obj)
		{
			if (this.isActiveAndEnabled) events.OnActionCanceled.Invoke();
		}


		public void PerformAction()
		{
			events.OnActionPerformed.Invoke();
		}
	}
}
