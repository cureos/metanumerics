using System;
using System.Collections.Generic;
namespace Meta.Numerics {

    /// <summary>
    /// The exception that is thrown when an algorithm fails to converge.
    /// </summary>
    public partial class NonconvergenceException : Exception {

        /// <summary>
        /// Initializes a new nonconvergence exception.
        /// </summary>
        public NonconvergenceException () : base() { }

        /// <summary>
        /// Inititalizes a new nonconvergence exception with the given exception message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public NonconvergenceException (String message) : base(message) { }

        /// <summary>
        /// Initializes a new nonconvergence exception with the given exception message and inner exception.
        /// </summary>
        /// <param name="message">The exeption message.</param>
        /// <param name="innerException">The inner exception.</param>
        public NonconvergenceException (String message, Exception innerException) : base(message, innerException) { }

    }

    /// <summary>
    /// The exception that is thrown when attempting an operation on objects with incompatible dimensions.
    /// </summary>
    public partial class DimensionMismatchException : InvalidOperationException {

        /// <summary>
        /// Initializes a new dimension mismatch exception.
        /// </summary>
        public DimensionMismatchException () : base(Messages.DimensionMismatch) { }

        /// <summary>
        /// Inititalizes a new dimension mismatch exception with the given exception message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public DimensionMismatchException (String message) : base(message) { }

        /// <summary>
        /// Initializes a new dimension mismatch exception with the given exception message and inner exception.
        /// </summary>
        /// <param name="message">The exeption message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DimensionMismatchException (String message, Exception innerException) : base(message, innerException) { }

    }

}
