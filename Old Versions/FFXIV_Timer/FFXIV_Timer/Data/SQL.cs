namespace FFXIV_Timer.Data
{
    public struct Sql
    {
        public struct CreateTable
        {
            public const string ItemTableSql = @"CREATE TABLE UnspoiledItem (
                                      UnspoiledItem_Id INTEGER PRIMARY KEY ASC,
                                      SourceClass VARCHAR(10),
                                      Name VARCHAR(50),
                                      ItemLevel INT,
                                      Slot INT,
                                      Location VARCHAR(50),
                                      AvailabilityHours INT)";
            public const string TimeTableSql = @"CREATE TABLE ItemTime (
                                      ItemTime_Id INTEGER PRIMARY KEY ASC,
                                      UnspoiledItem_Id INT REFERENCES UnspoiledItem(UnspoiledItem_Id),
                                      Time INT NOT NULL)";
        }
        public struct Insert
        {
            public const string UnspoiledItemInsert = "INSERT INTO UnspoiledItem (" +
                                                    "SourceClass," +
                                                    "Name," +
                                                    "ItemLevel," +
                                                    "Slot," +
                                                    "Location," +
                                                    "AvailabilityHours)" +
                                                    "VALUES (" +
                                                    "\"{0}\"," +
                                                    "\"{1}\"," +
                                                    "{2}," +
                                                    "{3}," +
                                                    "\"{4}\"," +
                                                    "{5})";
            public const string ItemTimeInsert = @"INSERT INTO ItemTime (
                                                    UnspoiledItem_Id,
                                                    Time)
                                                    VALUES (
                                                    {0},
                                                    {1})";
        }

        public struct Query
        {
            public const string UnspoiledItemQuery = @"SELECT * FROM UnspoiledItem";
            public const string SelectTimeQuery = @"SELECT * FROM ItemTime WHERE UnspoiledItem_Id = {0}";
        }

        public struct Connection
        {
            public const string ConnectionString = "DataSource=XIVTimer.sqlite;Version=3";
            public const string FileName = "XIVTimer.sqlite";
        }
    }
}
