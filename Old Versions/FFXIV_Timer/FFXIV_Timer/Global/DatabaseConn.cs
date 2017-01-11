using FFXIV_Timer.Data;
using FFXIV_Timer.Interface;
using FFXIV_Timer.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.Text;

namespace FFXIV_Timer.Global
{
    /// <summary>
    /// Holds and controls the Database Elements to connect to the SQLite Database
    /// </summary>
    public class DatabaseConn
    {
        #region Static Properties
        // ReSharper disable once ConvertToAutoProperty
        public static DatabaseConn Instance => _instance;
        #endregion

        #region Members
        // ReSharper disable once InconsistentNaming
        private static readonly DatabaseConn _instance = new DatabaseConn();
        private readonly SQLiteConnection _conn;
        #endregion

        #region Constructors
        /// <summary>
        /// Private Constructor. Handles the initialization of the database.
        /// </summary>
        private DatabaseConn()
        {
            //If File exists, register the database file
            if (System.IO.File.Exists(Sql.Connection.FileName))
            {
                _conn = new SQLiteConnection(Sql.Connection.ConnectionString);
            }
            //Else, create file, register, and initialize the database
            else
            {
                SQLiteConnection.CreateFile(Sql.Connection.FileName);
                _conn = new SQLiteConnection(Sql.Connection.ConnectionString);
                InitializeDatabase();
            }
        }

        /// <summary>
        /// Static Constructor. Not used.
        /// </summary>
        static DatabaseConn() { }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the Database.
        /// Creates tables, populated data, establishes constraints.
        /// </summary>
        private void InitializeDatabase()
        {
            //Create commands to create tables.
            var cmd = new SQLiteCommand(Sql.CreateTable.ItemTableSql, _conn);
            var timeCmd = new SQLiteCommand(Sql.CreateTable.TimeTableSql, _conn);
            try
            {
                //Open DB and create Tables
                _conn.Open();
                cmd.ExecuteNonQuery();
                timeCmd.ExecuteNonQuery();

                //Fill DataTable with Data to insert into DB
                var unspoiledTable = GetTableOfType(new UnspoiledItem_Mock());

                //Populate temporary data
                foreach (var staticRow in AppData.MinerNodes)
                {
                    unspoiledTable.Rows.Add(staticRow);
                }

                //Insert data
                foreach (DataRow item in unspoiledTable.Rows)
                {
                    //Replace parameters in string
                    var insertCmd = new SQLiteCommand(string.Format(Sql.Insert.UnspoiledItemInsert, 
                                                                    item["SourceClass"], 
                                                                    item["Name"], 
                                                                    item["ItemLevel"], 
                                                                    item["Slot"], 
                                                                    item["Location"], 
                                                                    item["AvailabilityHours"]),
                                                                 _conn);
                    //Retrieve PK for newly inserted row
                    insertCmd.ExecuteNonQuery();
                    var returnedId = Convert.ToInt32(_conn.LastInsertRowId);

                    //Create Time record for item.
                    var insertTimeCmd = new SQLiteCommand(string.Format(Sql.Insert.ItemTimeInsert,
                                                                    new object[]
                                                                    {
                                                                        returnedId,
                                                                        Convert.ToDateTime(item["Time"]).Hour
                                                                    }),
                                                                _conn);
                    insertTimeCmd.ExecuteNonQuery();

                    //If item is Bi-Daily, insert the second time.
                    if (!Convert.ToBoolean(item["BiDaily"])) continue;
                    var insertSecondTimeCmd = new SQLiteCommand(string.Format(Sql.Insert.ItemTimeInsert,
                        new object[]
                        {
                            returnedId,
                            Convert.ToDateTime(item["Time"]).AddHours(12).Hour
                        }),
                        _conn);
                    insertSecondTimeCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //@TODO: Logging
            }
            finally
            {
                //Close the connection
                _conn.Close();
            }
        }

        /// <summary>
        /// Creates a DataTable of an object type.
        /// </summary>
        /// <param name="model">The Type used to model the DataTable.</param>
        /// <returns>DataTable with properties and types mapped to columns.</returns>
        private static DataTable GetTableOfType(object model)
        {
            //Create DataTable
            var returnTable = new DataTable();

            //Begin iterating through properties of the Type
            foreach (var pInfo in model.GetType().GetProperties())
            {
                returnTable.Columns.Add(new DataColumn(pInfo.Name, pInfo.PropertyType));
            }

            //Return table
            return returnTable;
        }

        /// <summary>
        /// Helper method to TryParse an object.
        /// Makes use of safe conversion, if object is dbnull or cast fails, null value is returned.
        /// </summary>
        /// <param name="obj">Object to Convert</param>
        /// <param name="t">Type to Convert to</param>
        /// <returns>Converted value or null</returns>
        private static object DbNullConversion(object obj, Type t)
        {
            try
            {
                return obj != DBNull.Value ? Convert.ChangeType(obj, t) : null;
            }
            catch(InvalidCastException ice)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the cleaned up string used to display spawn times for UnspoiledItems
        /// </summary>
        /// <param name="hour">Zero-based hour the item spawns</param>
        /// <param name="firstVal">Flag for concatenated times</param>
        /// <returns>ToString value of the datetime object expressed in Hour AM/PM</returns>
        private static string GetDisplayString(int hour, bool firstVal)
        {
            return (firstVal ? string.Empty : ", ") + new DateTime(1, 1, 1, hour, 0, 0).ToString("h tt");
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Builds query for DB based on passed object.
        /// Object properties are used to construct WHERE clause
        /// NOTE: This function only supports empty objects for now.
        /// </summary>
        /// <param name="model">Object used to construct WHERE clause</param>
        /// <returns>IEnumerable of results</returns>
        public IEnumerable<IUnspoiledItem> Get(IUnspoiledItem model)
        {
            var results = new List<IUnspoiledItem>();
            //Begin iterating through model object properties to see what isn't null
            var changeList = new List<PropertyInfo>();
            var blankObject = true;
            foreach (var pi in model.GetType().GetProperties())
            {
                var val = pi.GetValue(model, null);

                //Conditionals because switching on Type is more trouble than its worth
                if (pi.PropertyType == typeof(string))
                {
                    if (val == null) continue;
                    changeList.Add(pi);
                    blankObject = false;
                }
                else if (pi.PropertyType == typeof(int))
                {
                    if (Convert.ToInt32(val) <= 0) continue;
                    changeList.Add(pi);
                    blankObject = false;
                }
                else
                {
                    //Something went horribly wrong.
                    //@TODO: ADD LOGGING
                }
            }
            //Return blank result set if model object isn't blank.
            if (!blankObject) return results;
            //Begin queries
            _conn.Open();
            var selectCmd = new SQLiteCommand(Sql.Query.UnspoiledItemQuery, _conn);
            var returnVals = selectCmd.ExecuteReader();
            while (returnVals.Read())
            {
                //Subquery for time values
                var timeCmd = new SQLiteCommand(string.Format(Sql.Query.SelectTimeQuery, returnVals["UnspoiledItem_Id"]),
                    _conn);
                var returnTimeVals = timeCmd.ExecuteReader();
                //Variables to hold retrieved data
                var timeList = new List<IItemTime>();
                var firstVal = true;
                var displayString = new StringBuilder();
                //Iterate through returned Times
                while (returnTimeVals.Read())
                {
                    timeList.Add(new ItemTime()
                    {
                        ItemTime_Id = Convert.ToInt32(DbNullConversion(returnTimeVals["ItemTime_Id"], typeof(int))),
                        Time = Convert.ToInt32(DbNullConversion(returnTimeVals["Time"], typeof(int))),
                        UnspoiledItem_Id = Convert.ToInt32(DbNullConversion(returnTimeVals["UnspoiledItem_Id"], typeof(int)))
                    });

                    if (firstVal)
                    {
                        displayString.Append(value: GetDisplayString(timeList[timeList.Count - 1].Time, true));
                        firstVal = false;
                    }
                    else
                    {
                        displayString.Append(GetDisplayString(timeList[timeList.Count - 1].Time, false));
                    }
                }
                //Build UnspoiledItem object
                var tempObj = new UnspoiledItem
                {
                    UnspoiledItem_Id =
                        Convert.ToInt32(DbNullConversion(returnVals["UnspoiledItem_Id"], typeof(int))),
                    SourceClass = DbNullConversion(returnVals["SourceClass"].ToString(), typeof(string)).ToString(),
                    Name = DbNullConversion(returnVals["Name"].ToString(), typeof(string)).ToString(),
                    ItemLevel = Convert.ToInt32(DbNullConversion(returnVals["ItemLevel"], typeof(int))),
                    DisplayTime = displayString.ToString(),
                    Slot = Convert.ToInt32(DbNullConversion(returnVals["Slot"], typeof(int))),
                    Location = DbNullConversion(returnVals["Location"].ToString(), typeof(string)).ToString(),
                    NextSpawn =
                    {
                        AvailabilityHours =
                            Convert.ToInt32(DbNullConversion(returnVals["AvailabilityHours"], typeof (int))),
                        Time = timeList
                    }
                };
                //Add object to results list
                results.Add(tempObj);
            }
            //Return results
            return results;
        }
        #endregion
    }
}
