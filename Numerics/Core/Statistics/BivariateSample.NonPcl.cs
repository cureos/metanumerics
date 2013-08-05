using System;
using System.Data;
using System.Globalization;

namespace Meta.Numerics.Statistics
{
	public partial class BivariateSample
	{
		/// <summary>
		/// Loads values from a data reader.
		/// </summary>
		/// <param name="reader">The data reader.</param>
		/// <param name="xIndex">The column number of the x-variable.</param>
		/// <param name="yIndex">The column number of the y-variable.</param>
		public void Load(IDataReader reader, int xIndex, int yIndex)
		{
			if (reader == null) throw new ArgumentNullException("reader");
			if (isReadOnly) throw new InvalidOperationException();
			while (reader.Read())
			{
				if (reader.IsDBNull(xIndex) || reader.IsDBNull(yIndex)) continue;
				object xValue = reader.GetValue(xIndex);
				object yValue = reader.GetValue(yIndex);
				Add(Convert.ToDouble(xValue, CultureInfo.InvariantCulture), Convert.ToDouble(yValue, CultureInfo.InvariantCulture));
			}
		}

	}
}