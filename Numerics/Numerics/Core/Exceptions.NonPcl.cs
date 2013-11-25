using System;
using System.Runtime.Serialization;

namespace Meta.Numerics
{
	[Serializable]
	public partial class NonconvergenceException
	{
		/// <summary>
		/// Initalizes a new nonconvergence exception with the given serialization information and streaming context.
		/// </summary>
		/// <param name="info">The serialization information.</param>
		/// <param name="context">The streaming context.</param>
		protected NonconvergenceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}

	[Serializable]
	public partial class DimensionMismatchException
	{
		/// <summary>
		/// Initalizes a new dimension mismatch exception with the given serialization information and streaming context.
		/// </summary>
		/// <param name="info">The serialization information.</param>
		/// <param name="context">The streaming context.</param>
		protected DimensionMismatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}