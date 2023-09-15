using FluentMigrator;

namespace Migrator.Migrations;

[TimestampedMigration(2023, 9, 14, 16, 15)]
public sealed class AddGameTable : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("Game")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("State").AsString(20).NotNullable()
            .WithColumn("Target").AsInt32().NotNullable()
            .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("User", "Id").Indexed();
    }
}