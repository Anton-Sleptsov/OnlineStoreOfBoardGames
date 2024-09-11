namespace OnlineStoreOfBoardGames.Data.Enums
{
    [Flags]
    public enum Permission
    {
        None = 0,
        CanCreateAndUpdateBoardGames = 256,
        CanDeleteBoardGames = 512,
        CanModerateReviewsOfBoardGames = 1024
    }
}