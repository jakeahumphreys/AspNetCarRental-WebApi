namespace EIRLSS_Data_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ConfigurationRecords", "DvlaImportPath", c => c.String(nullable: false));
            AlterColumn("dbo.ConfigurationRecords", "AbiImportPath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ConfigurationRecords", "AbiImportPath", c => c.String());
            AlterColumn("dbo.ConfigurationRecords", "DvlaImportPath", c => c.String());
        }
    }
}
