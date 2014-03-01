using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalanche.Models;

namespace Avalanche.Repository
{
    public class AvalancheRepository
    {
        //TODO:
        //- Auto-detect and create catalog
        //-- pull catalog id
        //- bind pictures to vaults
        //-- autodetect vaults
        //-- pull vaultid


        protected readonly string _savePath;
        protected readonly string _catalogName;

        public AvalancheRepository(string savePath, string catalogName)
        {
            _savePath = savePath;
            _catalogName = catalogName;

            AssertDatabaseExists();
        }

        #region Setup

        protected DbConnection GetConnection()
        {
            return new SQLiteConnection(string.Format("DataSource={0};Version=3;", _savePath));
        }

        protected void AssertDatabaseExists()
        {
            if (File.Exists(_savePath))
            {
                return;
            }

            SQLiteConnection.CreateFile(_savePath);

            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
CREATE TABLE Catalogs
(
    CatalogId INTEGER NOT NULL
        CONSTRAINT PK_Catalogs
            PRIMARY KEY 
            AUTOINCREMENT,
    UniqueId NVARCHAR(200)
        CONSTRAINT IX_Catalogs_UniqueId
            UNIQUE
            ON CONFLICT ROLLBACK,
    FileName NVARCHAR(200) NOT NULL
)";

                command.ExecuteNonQuery();

                command.CommandText = @"
CREATE TABLE Pictures
(
    PictureId INTEGER NOT NULL
        CONSTRAINT PK_Pictures
            PRIMARY KEY
            AUTOINCREMENT,
    CatalogId INTEGER NOT NULL
        CONSTRAINT FK_Pictures_Catalogs
            REFERENCES Catalogs(CatalogId),
    FileAbsolutePath TEXT NOT NULL,
    FileCatalogPath TEXT NULL,
    FileName TEXT NOT NULL,
    FileId UNIQUEIDENTIFIER NOT NULL,
    ImageId UNIQUEIDENTIFIER NOT NULL,
    GlacierArchiveId TEXT NULL,
    GlacierHttpStatusCode INT NULL,
    GlacierLocation TEXT NULL,
    GlacierMetadata TEXT NULL,
    GlacierTimestamp DATETIME NULL
)";
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Read

        public bool FileIsArchived(Guid fileId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var query = connection.CreateCommand();
                query.CommandText = @"
SELECT
    COUNT(*)
FROM
    Pictures p
WHERE
    p.FileId = $fileId
";
                query.Parameters.Add(new SQLiteParameter("$fileId", fileId));
                var result = query.ExecuteScalar();
                return (long)result > 0;
            }
        }

        #endregion

        #region Write

        public void MarkFileAsArchived(ArchivedPictureModel model)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var insert = connection.CreateCommand();
                insert.CommandText = @"
INSERT INTO Pictures
    (
        CatalogId,
        FileAbsolutePath, 
        FileCatalogPath,
        FileName,
        FileId,
        ImageId,
        GlacierArchiveId,
        GlacierHttpStatusCode,
        GlacierLocation,
        GlacierMetadata,
        GlacierTimestamp
    )
SELECT
    $catalogId,
    $fileAbsolutePath,
    $fileCatalogPath,
    $fileName,
    $fileId,
    $imageId,
    $glacierArchiveId,
    $glacierHttpStatusCode,
    $glacierLocation,
    $glacierMetadata,
    $glacierTimestamp
";
                //insert.Parameters.Add(new SQLiteParameter("$catalogId", 0));
                insert.Parameters.Add(new SQLiteParameter("$fileAbsolutePath", model.Picture.AbsolutePath));
                insert.Parameters.Add(new SQLiteParameter("$fileCatalogPath", model.Picture.CatalogRelativePath));
                insert.Parameters.Add(new SQLiteParameter("$fileName", model.Picture.FileName));
                insert.Parameters.Add(new SQLiteParameter("$fileId", model.Picture.FileId));
                insert.Parameters.Add(new SQLiteParameter("$imageId", model.Picture.ImageId));
                insert.Parameters.Add(new SQLiteParameter("$glacierArchiveId", model.Archive.ArchiveId));
                insert.Parameters.Add(new SQLiteParameter("$glacierHttpStatusCode", (int)model.Archive.Status));
                insert.Parameters.Add(new SQLiteParameter("$glacierLocation", model.Archive.Location));
                insert.Parameters.Add(new SQLiteParameter("$glacierMetadata", model.Archive.Metadata));
                insert.Parameters.Add(new SQLiteParameter("$glacierTimestamp", model.Archive.PostedTimestamp));

                insert.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
