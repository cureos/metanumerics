using System;
using System.Runtime.Serialization;

namespace Meta.Numerics.Statistics
{
	[Serializable]
	public partial class InsufficientDataException
	{
		/// <summary>
		/// Initalizes a new insufficient data exception with the given serialization information and streaming context.
		/// </summary>
		/// <param name="info">The serialization information.</param>
		/// <param name="context">The streaming context.</param>
		protected InsufficientDataException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}