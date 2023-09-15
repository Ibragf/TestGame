using FluentMigrator;

namespace Migrator.Migrations;

[TimestampedMigration(2023, 9, 14, 16, 8)]
public sealed class AddUserTable : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("User")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(75).NotNullable().Unique()
            .WithColumn("Password").AsString(256).NotNullable();
    }
}