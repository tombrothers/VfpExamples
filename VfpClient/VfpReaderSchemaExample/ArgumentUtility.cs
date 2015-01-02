using System;

namespace VfpReaderSchemaExample {
    internal class ArgumentUtility {
        public static T CheckNotNull<T>(string argumentName, T value) where T : class {
            if (value == null) {
                throw new ArgumentNullException(argumentName);
            }

            return value;
        }

        public static string CheckNotNullOrEmpty(string argumentName, string value) {
            if (string.IsNullOrWhiteSpace(value)) {
                throw new ArgumentNullException(argumentName);
            }

            return value;
        }
    }
}