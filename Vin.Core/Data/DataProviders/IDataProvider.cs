﻿using System.Data.Common;

namespace Vin.Core.Data.DataProviders
{
    /// <summary>
    /// Data provider interface
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Initialize connection factory
        /// </summary>
        void InitConnectionFactory();

        /// <summary>
        /// Set database initializer
        /// </summary>
        void SetDatabaseInitializer();

        /// <summary>
        /// Initialize database
        /// </summary>
        void InitDatabase();

        /// <summary>
        /// A value indicating whether this data provider supports stored procedures
        /// </summary>
        bool StoredProceduresSupported { get; }

        /// <summary>
        /// Gets a support database parameter object (used by stored procedures)
        /// </summary>
        /// <returns>Parameter</returns>
        DbParameter GetParameter();
    }
}