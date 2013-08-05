using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Meta.Numerics.Statistics
{
	public partial class MultivariateSample
	{
		/// <summary>
		/// Loads values from a data reader.
		/// </summary>
		/// <param name="reader">The data reader.</param>
		/// <param name="dbIndexes">The database column indexes of the sample columns.</param>
		public void Load(IDataReader reader, IList<int> dbIndexes)
		{
			if (reader == null) throw new ArgumentNullException("reader");
			if (dbIndexes == null) throw new ArgumentNullException("dbIndexes");
			if (dbIndexes.Count != Dimension) throw new DimensionMismatchException();
			if (isReadOnly) throw new InvalidOperationException();

			// create an array to store values, which we will re-use as we move through the data
			double[] entry = new double[Dimension];
			// move through the data
			while (reader.Read())
			{
				// check each entry and, if value, add it to the sample
				if (ReadValues(reader, dbIndexes, entry)) Add(entry);
			}

		}

		private bool ReadValues(IDataReader reader, IList<int> dbIndexes, double[] entry)
		{
			for (int c = 0; c < Dimension; c++)
			{
				int i = dbIndexes[c];
				if (reader.IsDBNull(i))
				{
					return (false);
				}
				else
				{
					entry[c] = Convert.ToDouble(reader.GetValue(i), CultureInfo.InvariantCulture);
				}
			}
			return (true);
		}

		/// <summary>
		/// Loads values from a data reader.
		/// </summary>
		/// <param name="reader">The data reader.</param>
		/// <param name="dbIndexes">The database column indexes of the sample columns.</param>
		public void Load(IDataReader reader, params int[] dbIndexes)
		{
			Load(reader, (IList<int>)dbIndexes);
		}

	}
}