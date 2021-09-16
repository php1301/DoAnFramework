using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Transactions;

namespace ConsoleApp2
{
    class SqlStrategy
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        private const string DateTimeOffsetFormat = "yyyy-MM-dd HH:mm:ss.fffffff zzz";
        private const string TimeSpanFormat = "HH:mm:ss.fffffff";
        protected static readonly Type DateTimeType = typeof(DateTime);
        protected static readonly Type TimeSpanType = typeof(TimeSpan);
        private static readonly Type DateTimeOffsetType = typeof(DateTimeOffset);
        private const int DefaultTimeLimit = 1000;
        protected static readonly Type DecimalType = typeof(decimal);
        protected static readonly Type DoubleType = typeof(double);
        protected static readonly Type FloatType = typeof(float);
        protected static readonly Type ByteArrayType = typeof(byte[]);

        private readonly string masterDbConnectionString;
        private readonly string restrictedUserId;
        private readonly string restrictedUserPassword;
        private readonly string databaseNameForSubmissionProcessor;

        private TransactionScope transactionScope;

        public SqlStrategy(
            string masterDbConnectionString,
            string restrictedUserId ="69",
            string restrictedUserPassword="69",
            string submissionProcessorIdentifier="3000")
        {
            if (string.IsNullOrWhiteSpace(masterDbConnectionString))
            {
                throw new ArgumentException("Invalid master DB connection string!", nameof(masterDbConnectionString));
            }

            if (string.IsNullOrWhiteSpace(restrictedUserId))
            {
                throw new ArgumentException("Invalid restricted user ID!", nameof(restrictedUserId));
            }

            if (string.IsNullOrWhiteSpace(restrictedUserPassword))
            {
                throw new ArgumentException("Invalid restricted user password!", nameof(restrictedUserPassword));
            }

            this.masterDbConnectionString = masterDbConnectionString;
            this.restrictedUserId = restrictedUserId;
            this.restrictedUserPassword = restrictedUserPassword;
            this.databaseNameForSubmissionProcessor = $"worker_{submissionProcessorIdentifier}_DO_NOT_DELETE";
        }

        public string WorkerDbConnectionString { get; set; }

        public string RestrictedUserId => $"{this.GetDatabaseName()}_{this.restrictedUserId}";

        public IDbConnection GetOpenConnection(string databaseName)
        {
            this.EnsureDatabaseIsSetup();

            this.transactionScope = new TransactionScope();
            var createdDbConnection = new SqlConnection(this.WorkerDbConnectionString);
            createdDbConnection.Open();

            return createdDbConnection;
        }

        protected bool ExecuteNonQuery(IDbConnection connection, string commandText, int timeLimit = DefaultTimeLimit)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = this.FixCommandText(commandText);

                return CodeHelpers.ExecuteWithTimeLimit(
                    TimeSpan.FromMilliseconds(timeLimit),
                    () => command.ExecuteNonQuery());
            }
        }
        protected virtual string FixCommandText(string commandText)
            => commandText;
        public void DropDatabase(string databaseName)
            => this.transactionScope?.Dispose();

        public string ToHexString(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var hexChars = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

            var bytesCount = bytes.Length;
            var resultChars = new char[(bytesCount * 2) + 2];

            resultChars[0] = '0';
            resultChars[1] = 'x';

            var bytesIndex = 0;
            var resultCharsIndex = 2;
            while (bytesIndex < bytesCount)
            {
                var @byte = bytes[bytesIndex++];
                resultChars[resultCharsIndex++] = hexChars[@byte / 0x10];
                resultChars[resultCharsIndex++] = hexChars[@byte % 0x10];
            }

            return new string(resultChars, 0, resultChars.Length);
        }

        public string GetDatabaseName() => this.databaseNameForSubmissionProcessor;
        protected string GetDataRecordFieldValueExtra(IDataRecord dataRecord, int index)
        {
            string result;

            if (dataRecord.IsDBNull(index))
            {
                result = null;
            }
            else
            {
                var fieldType = dataRecord.GetFieldType(index);

                // Using CultureInfo.InvariantCulture to have consistent decimal separator.
                if (fieldType == DecimalType)
                {
                    result = dataRecord.GetDecimal(index).ToString(CultureInfo.InvariantCulture);
                }
                else if (fieldType == DoubleType)
                {
                    result = dataRecord.GetDouble(index).ToString(CultureInfo.InvariantCulture);
                }
                else if (fieldType == FloatType)
                {
                    result = dataRecord.GetFloat(index).ToString(CultureInfo.InvariantCulture);
                }
                else if (fieldType == ByteArrayType)
                {
                    var bytes = (byte[])dataRecord.GetValue(index);
                    result = ToHexString(bytes);
                }
                else
                {
                    result = dataRecord.GetValue(index).ToString();
                }
            }

            return result;
        }
        protected string GetDataRecordFieldValue(IDataRecord dataRecord, int index)
        {
            if (!dataRecord.IsDBNull(index))
            {
                var fieldType = dataRecord.GetFieldType(index);

                if (fieldType == DateTimeType)
                {
                    return dataRecord.GetDateTime(index).ToString(DateTimeFormat, CultureInfo.InvariantCulture);
                }

                if (fieldType == DateTimeOffsetType)
                {
                    return ((SqlDataReader)dataRecord)
                        .GetDateTimeOffset(index)
                        .ToString(DateTimeOffsetFormat, CultureInfo.InvariantCulture);
                }

                if (fieldType == TimeSpanType)
                {
                    return ((SqlDataReader)dataRecord)
                        .GetTimeSpan(index)
                        .ToString(TimeSpanFormat, CultureInfo.InvariantCulture);
                }
            }

            return this.GetDataRecordFieldValueExtra(dataRecord, index);
        }
       

        private void EnsureDatabaseIsSetup()
        {
            var databaseName = this.GetDatabaseName();

            using (var connection = new SqlConnection(this.masterDbConnectionString))
            {
                connection.Open();

                var setupDatabaseQuery =
                    $@"IF DB_ID('{databaseName}') IS NULL
                    BEGIN
                    CREATE DATABASE [{databaseName}];
                    CREATE LOGIN [{this.RestrictedUserId}] WITH PASSWORD=N'{this.restrictedUserPassword}',
                    DEFAULT_DATABASE=[master],
                    DEFAULT_LANGUAGE=[us_english],
                    CHECK_EXPIRATION=OFF,
                    CHECK_POLICY=ON;
                    END";

                var setupUserAsOwnerQuery = $@"
                    USE [{databaseName}];
                    IF IS_ROLEMEMBER('db_owner', '{this.RestrictedUserId}') = 0 OR IS_ROLEMEMBER('db_owner', '{this.RestrictedUserId}') is NULL
                    BEGIN
                    CREATE USER [{this.RestrictedUserId}] FOR LOGIN [{this.RestrictedUserId}];
                    ALTER ROLE [db_owner] ADD MEMBER [{this.RestrictedUserId}];
                    END";

                this.ExecuteNonQuery(connection, setupDatabaseQuery);
                this.ExecuteNonQuery(connection, setupUserAsOwnerQuery);
            }

            this.WorkerDbConnectionString = this.BuildWorkerDbConnectionString(databaseName);
        }

        private string BuildWorkerDbConnectionString(string databaseName)
        {
            var userIdRegex = new Regex("User Id=.*?;");
            var passwordRegex = new Regex("Password=.*?;");

            var workerDbConnectionString = this.masterDbConnectionString;

            workerDbConnectionString =
                userIdRegex.Replace(workerDbConnectionString, $"User Id={this.RestrictedUserId};");

            workerDbConnectionString =
                passwordRegex.Replace(workerDbConnectionString, $"Password={this.restrictedUserPassword}");

            workerDbConnectionString += $";Database={databaseName};Pooling=False;";

            return workerDbConnectionString;
        }
    }
}
