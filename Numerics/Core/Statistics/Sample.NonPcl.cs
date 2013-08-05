using System;
using System.Data;
using System.Globalization;

namespace Meta.Numerics.Statistics
{
	public partial class Sample
	{
		/// <summary>
		/// Loads values from a data reader.
		/// </summary>
		/// <param name="reader">The data reader.</param>
		/// <param name="dbIndex">The column number.</param>
		public void Load(IDataReader reader, int dbIndex)
		{
			if (reader == null) throw new ArgumentNullException("reader");
			if (isReadOnly) throw new InvalidOperationException();
			while (reader.Read())
			{
				if (reader.IsDBNull(dbIndex)) continue;
				object value = reader.GetValue(dbIndex);
				Add(Convert.ToDouble(value, CultureInfo.InvariantCulture));
			}
		}
	}
}