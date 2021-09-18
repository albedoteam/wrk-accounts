namespace Accounts.Business.Models.Migrations
{
    // using AlbedoTeam.Sdk.DataLayerAccess.Migrations.Engine.Database;
    // using MongoDB.Driver;
    //
    // public class DBM001_AddNewAccount : DatabaseMigration
    // {
    //     public DBM001_AddNewAccount()
    //         : base("1.0.0")
    //     {
    //     }
    //
    //     public override void Up(IMongoDatabase db)
    //     {
    //         var collection = db.GetCollection<Account>("Accounts");
    //         collection.InsertOne(new Account
    //         {
    //             Name = "AddedInMigration",
    //             DisplayName = "Added In migrations",
    //             Enabled = false,
    //             IdentificationNumber = "123123123222"
    //         });
    //     }
    //
    //     public override void Down(IMongoDatabase db)
    //     {
    //         var collection = db.GetCollection<Account>("Accounts");
    //         collection.DeleteOne(Builders<Account>.Filter.Eq(c => c.Name, "AddedInMigration"));
    //     }
    // }
}