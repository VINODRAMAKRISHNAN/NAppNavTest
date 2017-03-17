//-----------------------------------------------------------------------
// <copyright file="TestContentDatabase.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Android.Content;
using Android.Database.Sqlite;
using Android.Database;

namespace IntuneMAMSampleAndroid
{
    public class TestContentDatabase : SQLiteOpenHelper
    {
        public const string TABLE_NAME = "t1";
        private const string DB_NAME = "testDB";
        private const int DB_VER = 1;

        public TestContentDatabase(Context context)
            : base(context, DB_NAME, null, DB_VER)
        {
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            try
            {
                db.ExecSQL("DROP TABLE IF EXISTS " + TABLE_NAME + ";");
                db.ExecSQL("CREATE TABLE " + TABLE_NAME + " (" + TestContentProvider.FIELD1 + " VARCHAR, " + TestContentProvider.FIELD2 + " VARCHAR);");
            }
            catch (SQLException e)
            {
                e.PrintStackTrace();
            }
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            throw new NotImplementedException();
        }
    }
}