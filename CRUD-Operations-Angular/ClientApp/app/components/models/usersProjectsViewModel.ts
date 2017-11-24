export class UsersProjectsViewModel
{
    public userId: number;
    public projectId: number;

    public static GetUserProjectModel(userId: number, projectId: number): UsersProjectsViewModel
    {
        let result: UsersProjectsViewModel = new UsersProjectsViewModel();
        result.projectId = projectId;
        result.userId = userId;
        return result;
    }
}