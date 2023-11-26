using System;

namespace RabbitOM.Net.Sdp.Validation
{
	/// <summary>
	/// Represent session descriptor validator which validate ALL fields presents in the descriptor
	/// </summary>
	public sealed class FullSessionDescriptorValidator : SessionDescriptorValidator
	{
		/// <summary>
		/// Initialiaze a new instance of validator
		/// </summary>
		internal FullSessionDescriptorValidator()
		{
		}

		/// <summary>
		/// Validate
		/// </summary>
		/// <param name="descriptor">the descriptor</param>
		/// <exception cref="ArgumentNullException"/>
		public override void Validate(SessionDescriptor descriptor)
		{
			if (descriptor == null)
			{
				throw new ArgumentNullException( nameof( descriptor ) );
			}

			foreach ( var field in SessionDescriptor.ListAllFields( descriptor ) )
			{
				field.Validate();
			}
		}

		/// <summary>
		/// Validate
		/// </summary>
		/// <param name="descriptor">the descriptor</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public override bool TryValidate(SessionDescriptor descriptor)
		{
			if (descriptor == null)
			{
				return false;
			}

			foreach (var field in SessionDescriptor.ListAllFields( descriptor ) )
			{
				if ( ! field.TryValidate() )
				{
					return false;
				}
			}

			return true;
		}

	}
}
