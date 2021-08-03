using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NibrsModels.Extensions
{
    public static class StringExtensions
    {
        #region Search

        public static bool MatchAll(this string input, params string[] words)
        {
            return words.All(word => input == word);
        }

        public static bool Matches(this string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        public static bool Matches(this string input, Regex pattern)
        {
            return pattern.IsMatch(input);
        }

        public static bool MatchOne(this string input, params string[] words)
        {
            return words.Any(word => input == word);
        }

        public static bool MatchOne(this string input, DataRowCollection rowCollection, DataColumn column)
        {
            return rowCollection.Cast<DataRow>().Any(row => input == row[column].ToString());
        }

        public static bool MatchOne(this string input, DataRowCollection rowCollection, string column)
        {
            return rowCollection.Cast<DataRow>().Any(row => input == row[column].ToString());
        }

        public static bool MatchOne<TEnumerationType>(this string valueToCompare)
        {
            //Enumerates all enum values and compares their description attribute value with *valueToCompare*. If no matches, returns false, otherwise true.
            return
                Enum.GetValues(typeof(TEnumerationType))
                    .Cast<Enum>()
                    .Where(value => value.GetDescription() == valueToCompare)
                    .ToList()
                    .Count > 0;
        }

        public static bool MatchOne<TEnumerationType, TDescriptionAttributeType>(this string valueToCompare,
            Func<TDescriptionAttributeType, string> descriptionAttributeValueParser)
        {
            //Enumerates all enum values and compares their description attribute value with *valueToCompare*. If no matches, returns false, otherwise true.
            return
                Enum.GetValues(typeof(TEnumerationType))
                    .Cast<Enum>()
                    .Where(
                        value =>
                            descriptionAttributeValueParser(
                                value.GetDescriptionForAttributeType<TDescriptionAttributeType>()) == valueToCompare)
                    .ToList()
                    .Count > 0;
        }

        #endregion

        #region Value

        public static bool IsNullBlankOrEmpty(this string input, bool trim = true)
        {
            //Blank means is full of whitespace
            return input != null && trim ? input.Trim() == "" : string.IsNullOrEmpty(input);
        }

        public static bool IsNullBlankOrEmpty(this char input)
        {
            //Blank means is full of whitespace
            return input == '\0' || input == ' ';
        }

        public static bool HasValue(this string input, bool trim = false)
        {
            //Blank means is full of whitespace
            return !IsNullBlankOrEmpty(input, trim);
        }

        #endregion

        #region Transform

        public static Stream ToStream(this string input)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(input);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string ToTitleCase(this string input)
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            var titleCaseRepresentation = textInfo.ToTitleCase(input);
            return titleCaseRepresentation;
        }

        #endregion
        
        public static List<string> SplitCsv(this string csvList, bool nullOrWhitespaceInputReturnsNull = false)
        {
            if (string.IsNullOrWhiteSpace(csvList))
                return nullOrWhitespaceInputReturnsNull ? null : new List<string>();

            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable<string>()
                .Select(s => s.Trim())
                .ToList();
        }
    }
}