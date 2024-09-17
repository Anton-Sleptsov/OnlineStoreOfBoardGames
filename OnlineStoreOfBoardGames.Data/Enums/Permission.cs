namespace OnlineStoreOfBoardGames.Data.Enums
{
    [Flags]
    public enum Permission
    {
        None = 0,
        CanViewAlerts = 1,
        CanCreateAndDeleteAlerts = 2,
        CanViewPremission = 4,
        CanEditPremission = 8,
        CanCreateAndUpdateBoardGames = 256,
        CanDeleteBoardGames = 512,
        CanModerateReviewsOfBoardGames = 1024
    }
}