namespace EIRLSS_Data_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConfigRecords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConfigurationRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecordCreated = c.DateTime(nullable: false),
                        DvlaImportPath = c.String(),
                        AbiImportPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ConfigurationRecords");
        }
    }
}
