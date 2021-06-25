﻿using Sqo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDatabase
{
    class SiaqodbFactory
    {
        public static string siaoqodbPath;
        private static Siaqodb instance;
        ///<summary>
        /// Set the path where the database file will reside
        ///</summary>
        public static void SetPath(string path)
        {
            siaoqodbPath = path;
        }
        ///<summary>
        /// Acquire an instance of the database engine
        ///</summary>
        public static Siaqodb GetInstance()
        {
            if (instance == null)
            {
                instance = new Siaqodb(siaoqodbPath);
            }
            return instance;
        }
        ///<summary>
        /// Close the database
        ///</summary>
        public static void CloseDatabase()
        {
            if (instance != null)
            {
                instance.Close();
                instance = null;
            }
        }
    }
}
