using Android.Content;
using Android.Database.Sqlite;
using PokemonApp.Utilities;
using System;
using System.IO;

namespace PokemonApp.AndroidExtensions
{
    public class SQLiteHelper : SQLiteOpenHelper
    {
        private static int VERSION = 1;
        private Context _appContext = null;

        public SQLiteHelper(Context appContext = null) : base(appContext, Constants.DB_FILENAME, null, VERSION)
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

        private void CopyDatabase()
        {
            var inputFile = _appContext.Assets.Open(Constants.DB_FILENAME);
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
            bool isCreated = false;

            var sqLiteDatabase = SQLiteDatabase.OpenDatabase(GetDbPath(), null, DatabaseOpenFlags.OpenReadonly);

            if (sqLiteDatabase != null)
            {
                isCreated = true;
                sqLiteDatabase.Close();
            }

            return isCreated;
        }

        private string GetDbPath()
        {
            return Path.Combine(Constants.DB_DIRECTORY, Constants.DB_FILENAME);
        }

        #region Not implemented override methods

        public override void OnCreate(SQLiteDatabase db) { }
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) { }

        #endregion
    }
}