using System.ComponentModel;
using System.Globalization;

namespace System
{
    /// <summary>
    /// Represents a whole date, having a year, month and day component.
    /// All values are in the proleptic Gregorian (ISO 8601) calendar system unless otherwise specified.
    /// </summary>
    [TypeConverter(typeof(DateTypeConverter))]
    public readonly struct Date : IEquatable<Date>, IComparable<Date>, IComparable
    {
        private DateTime Value { get; }

        public Date(int year, int month, int day) => Value = new DateTime(year, month, day);

        public Date(DateTime date) => Value = date.Date;

        public static implicit operator DateTime(Date date) => date.Value;

        /// <summary>
        /// Casts a <see cref="DateTime"/> object to a <see cref="Date"/> by returning a new
        /// <see cref="Date"/> object that has the equivalent year, month, and day components. This is useful when
        /// using APIs that express a calendar date as a <see cref="DateTime"/> with time set at midnight (00:00:00).
        /// This operator enables these values to be assigned to a variable having a <see cref="Date"/> type.
        /// However, since <see cref="DateTime"/> values containing a time are not allowed, the cast is required to
        /// be applied explicitly. Note that the <see cref="DateTime.Kind"/> property is ignored for this operation.
        /// </summary>
        /// <param name="dateTime">A <see cref="DateTime"/> value whose date portion will be used to construct a new
        /// <see cref="Date"/> object, whose time portion must be set to midnight (00:00:00), and whose kind will be ignored.</param>
        /// <returns>A newly constructed <see cref="Date"/> object with an equivalent date value.</returns>
        /// <exception cref="InvalidCastException">
        /// <paramref name="dateTime"/> has some non-zero time value, and thus cannot be cast to a <see cref="Date"/>.
        /// </exception>
        /// <remarks>
        /// Fundamentally, a date-only value and a date-time value are two different concepts.  In previous versions
        /// of the .NET framework, the <see cref="Date"/> type did not exist, and thus several date-only values were
        /// represented by using a <see cref="DateTime"/> with the time set to midnight (00:00:00).  For example, the
        /// <see cref="DateTime.Today"/> and <see cref="DateTime.Date"/> properties exhibit this behavior.
        /// This cast operator allows those APIs to be used with <see cref="Date"/>, when explicitly cast.
        /// <para>
        /// Also note that when evaluated as a full date-time, the input <paramref name="dateTime"/> might not actually
        /// exist, since some time zones (ex: Brazil) will spring-forward directly from 23:59 to 01:00, skipping over
        /// midnight.  Using a <see cref="Date"/> object avoids this particular edge case, and several others.
        /// </para>
        /// </remarks>
        public static explicit operator Date(DateTime dateTime) => new (dateTime);

        /// <summary>
        /// Gets a <see cref="Date"/> object that is set to the current date,
        /// expressed in this computer's local time zone.
        /// </summary>
        /// <value>An object whose value is the current local date.</value>
        public static Date Today => new (DateTime.Today);

        /// <summary>
        /// Converts the value of the current <see cref="Date"/> object to its equivalent ISO standard string
        /// representation (ISO-8601), which has the format: <c>yyyy-MM-dd</c>.
        /// </summary>
        /// <returns>
        /// A string that contains the ISO standard string representation of the current <see cref="Date"/> object.
        /// </returns>
        /// <remarks>
        /// Because the ISO-8601 standard uses the proleptic Gregorian calendar, this method always uses the calendar
        /// of the <see cref="CultureInfo.InvariantCulture"/>, despite the setting of the current culture.
        /// </remarks>
        public override string ToString() => Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        /// <summary>
        /// Converts the specified string representation of a date to its <see cref="Date"/> equivalent
        /// The format of the string representation must match the (ISO-8601), which has the format: <c>yyyy-MM-dd</c> exactly or an exception is thrown.
        /// </summary>
        /// <param name="s">A string that contains a date to convert.</param>
        public static Date Parse(string s) => new (DateTime.ParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture));

        /// <summary>
        /// Returns a value indicating whether two <see cref="Date"/> instances have the same date value.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the two values are equal; otherwise, <c>false</c>.</returns>
        public static bool Equals(Date left, Date right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value indicating whether the value of this instance is equal to the value of the specified
        /// <see cref="Date"/> instance.
        /// </summary>
        /// <param name="value">The other <see cref="Date"/> object to compare against this instance.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="value"/> parameter equals the value of this instance;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Date value) => Value == value.Value;

        /// <summary>
        /// Returns a value indicating whether this instance is equal to the specified object.
        /// </summary>
        /// <param name="value">The object to compare to this instance.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="value"/> is an instance of <see cref="Date"/>
        /// and equals the value of this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? value)
        {
            if (value is null)
            {
                return false;
            }

            return value is Date date && Equals(date);
        }

        /// <summary>
        /// Returns the hash code of this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Compares two instances of <see cref="Date"/> and returns an integer that indicates whether the first
        /// instance is earlier than, the same as, or later than the second instance.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of <paramref name="left"/> and <paramref name="right"/>.
        /// <list type="table">
        /// <listheader><term>Value</term><term>Description</term></listheader>
        /// <item>
        ///   <term>Less than zero</term>
        ///   <term><paramref name="left"/> is earlier than <paramref name="right"/>.</term>
        /// </item>
        /// <item>
        ///   <term>Zero</term>
        ///   <term><paramref name="left"/> is the same as <paramref name="right"/>.</term>
        /// </item>
        /// <item>
        ///   <term>Greater than zero</term>
        ///   <term><paramref name="left"/> is later than <paramref name="right"/>.</term>
        /// </item>
        /// </list>
        /// </returns>
        public static int Compare(Date left, Date right) => left.Value.CompareTo(right.Value);

        /// <summary>
        /// Compares the value of this instance to a specified <see cref="Date"/> value and returns an integer that
        /// indicates whether this instance is earlier than, the same as, or later than the specified
        /// <see cref="Date"/> value.
        /// </summary>
        /// <param name="value">The object to compare to the current instance.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and the <paramref name="value"/> parameter.
        /// <list type="table">
        /// <listheader><term>Value</term><term>Description</term></listheader>
        /// <item>
        ///   <term>Less than zero</term>
        ///   <term>This instance is earlier than <paramref name="value"/>.</term>
        /// </item>
        /// <item>
        ///   <term>Zero</term>
        ///   <term>This instance is the same as <paramref name="value"/>.</term>
        /// </item>
        /// <item>
        ///   <term>Greater than zero</term>
        ///   <term>This instance is later than <paramref name="value"/>.</term>
        /// </item>
        /// </list>
        /// </returns>
        public int CompareTo(Date value) => Compare(this, value);

        /// <summary>
        /// Compares the value of this instance to a specified object that contains a <see cref="Date"/> value and
        /// returns an integer that indicates whether this instance is earlier than, the same as, or later than the
        /// specified <see cref="Date"/> value.
        /// </summary>
        /// <param name="value">The object to compare to the current instance.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and the <paramref name="value"/> parameter.
        /// <list type="table">
        /// <listheader><term>Value</term><term>Description</term></listheader>
        /// <item>
        ///   <term>Less than zero</term>
        ///   <term>This instance is earlier than <paramref name="value"/>.</term>
        /// </item>
        /// <item>
        ///   <term>Zero</term>
        ///   <term>This instance is earlier than <paramref name="value"/>.</term>
        /// </item>
        /// <item>
        ///   <term>Greater than zero</term>
        ///   <term>
        ///     This instance is later than <paramref name="value"/>,
        ///     or <paramref name="value"/> is <c>null</c>.
        ///   </term>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> is not a <see cref="Date"/>.
        /// </exception>
        public int CompareTo(object? value)
        {
            if (value is null)
            {
                return 1;
            }

            if (value is not Date)
            {
                throw new ArgumentException("Argument Must Be Date", nameof(value));
            }

            return Compare(this, (Date)value);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Date"/> are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left"/> and <paramref name="right"/> represent the same date;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(Date left, Date right) => left.Equals(right);

        /// <summary>
        /// Determines whether two specified instances of <see cref="Date"/> are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left"/> and <paramref name="right"/> do not represent the same date;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(Date left, Date right) => !left.Equals(right);

        /// <summary>
        /// Determines whether one specified <see cref="Date"/> is later than another specified <see cref="Date"/>.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left"/> is later than <paramref name="right"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(Date left, Date right) => left.Value > right.Value;

        /// <summary>
        /// Determines whether one specified <see cref="Date"/> is equal to or later than another specified <see cref="Date"/>.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left"/> is equal to or later than <paramref name="right"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(Date left, Date right) => left.Value >= right.Value;

        /// <summary>
        /// Determines whether one specified <see cref="Date"/> is earlier than another specified <see cref="Date"/>.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left"/> is earlier than <paramref name="right"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(Date left, Date right) => left.Value < right.Value;

        /// <summary>
        /// Determines whether one specified <see cref="Date"/> is equal to or earlier than another specified <see cref="Date"/>.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left"/> is equal to or earlier than <paramref name="right"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(Date left, Date right) => left.Value <= right.Value;

        /// <summary>
        /// Gets a <see cref="Date"/> object whose value is ahead or behind the value of this instance by the specified
        /// number of days. Positive values will move the date forward; negative values will move the date backwards.
        /// </summary>
        /// <param name="days">The number of days to adjust by. The value can be negative or positive.</param>
        /// <returns>
        /// A new <see cref="Date"/> object which is the result of adjusting this instance by the
        /// <paramref name="days"/> specified.
        /// </returns>
        public Date AddDays(int days) => new (Value.AddDays(days));
    }
}