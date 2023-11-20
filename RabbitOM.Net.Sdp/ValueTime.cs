using System;
using System.Linq;

namespace RabbitOM.Net.Sdp
{
	/// <summary>
	/// Represent a value object
	/// </summary>
	public struct ValueTime
	{
		private readonly long _startTime;

		private readonly long _stopTime;



		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="startTime">the start time</param>
		/// <param name="stopTime">the stop time</param>
		public ValueTime(long startTime, long stopTime)
		{
			_startTime = startTime;
			_stopTime = stopTime;
		}



		/// <summary>
		/// Represent a zero value
		/// </summary>
		public readonly static ValueTime Zero = new ValueTime(0, 0);



		/// <summary>
		/// Gets the start time
		/// </summary>
		public long StartTime
		{
			get => _startTime;
		}

		/// <summary>
		/// Gets the stop time
		/// </summary>
		public long StopTime
		{
			get => _stopTime;
		}



		/// <summary>
		/// Validate
		/// </summary>
		/// <returns>returns true for a success, otherwise false</returns>
		public bool Validate()
		{
			return 0 <= _startTime && _startTime <= _stopTime;
		}

		/// <summary>
		/// Format to string
		/// </summary>
		/// <returns>returns a string value</returns>
		public override string ToString()
		{
			return string.Format("{0} {1}", _startTime, _stopTime);
		}



		/// <summary>
		/// Parse
		/// </summary>
		/// <param name="value">the input value</param>
		/// <returns>returns an instance</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="FormatException"/>
		public static ValueTime Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(nameof(value));
			}

			return TryParse(value, out ValueTime result) ? result : throw new FormatException();
		}

		/// <summary>
		/// Try to parse
		/// </summary>
		/// <param name="value">the input value</param>
		/// <param name="result">the output result</param>
		/// <returns>returns true for a success, otherwise false</returns>
		public static bool TryParse(string value, out ValueTime result)
		{
			result = ValueTime.Zero;

			if (string.IsNullOrWhiteSpace(value))
			{
				return false;
			}

			var tokens = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			if (tokens.Length <= 1)
			{
				return false;
			}

			result = new ValueTime(SessionDescriptorDataConverter.ConvertToLong(tokens.ElementAtOrDefault(0)), SessionDescriptorDataConverter.ConvertToLong(tokens.ElementAtOrDefault(1)));

			return true;
		}
	}
}
