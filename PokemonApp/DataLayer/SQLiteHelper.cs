using Android.Content;
using Android.Database.Sqlite;
using System;
using System.IO;

namespace PokemonApp.DataAccess
{
    public class SQLiteHelper : SQLiteOpenHelper
    {
        private static string DB_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static string DB_FILENAME = "PokemonDB.sqlite";
        private static int VERSION = 1;
        private Context _appContext = null;

        public SQLiteHelper(Context appContext = null) : base(appContext, DB_FILENAME, null, VERSION)
        {
            _appContext = appContext;
        }

        public void CreateDatabase()
        {
            if (!IsDatabaseCreated())
            {
                try
                {
                    CopyDatabase();
                }

                catch(Exception ex)
                {
                    throw new Exception("Error creating database " + ex.Message);
                }

            }
        }

        public string GetDbPath()
        {
            return Path.Combine(DB_DIRECTORY, DB_FILENAME);
        }

        public void CopyDatabase()
        {
            var inputFile = _appContext.Assets.Open(DB_FILENAME);
            var outputFile = new FileStream(GetDbPath(), FileMode.OpenOrCreate);

            byte[] buffer = new byte[1024];
            int length;

            while ((length = inputFile.Read(buffer, 0, 1024)) > 0)
                outputFile.Write(buffer, 0, length);

            outputFile.Flush();
            outputFile.Close();
            inputFile.Close();
        }

        private bool IsDatabaseCreated()
        {
            SQLiteDatabase sqLiteDatabase = null;
            bool isCreated = false;

            try
            {
                sqLiteDatabase = SQLiteDatabase.OpenDatabase(GetDbPath(), 
                    null, DatabaseOpenFlags.OpenReadonly);
            }

            catch(Exception ex) { }

            if (sqLiteDatabase != null)
            {
                isCreated = true;
                sqLiteDatabase.Close();
            }

            return isCreated;
        }

        #region Not implemented override methods

        public override void OnCreate(SQLiteDatabase db)
        {

        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {

        }

        #endregion
    }
}