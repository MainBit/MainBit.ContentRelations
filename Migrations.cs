using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data.Migration;

namespace MainBit.ContentRelations
{
    public class Migrations : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable("ContentRelationItemRecord",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<int>("ContentRelationRecord_Id")
                    .Column<int>("ContentItemRecord1_Id")
                    .Column<int>("ContentItemRecord2_Id")
                );

            return 1;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.CreateTable("ContentRelationRecord",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("Title")
                );

            return 2;
        }
    }
}