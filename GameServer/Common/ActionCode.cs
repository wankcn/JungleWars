namespace Common
{
    public enum ActionCode
    {
        // 枚举类型是值类型 每一个值代表一个数字
        None,
        // 登录请求
        Login,
        Register,
        ListRoom,
        CreateRoom,
        JoinRoom,
        UpdateRoom, //更新房间
        QuitRoom,
        StartGame,
        ShowTimer,
        StartPlay,
        Move,
        Shoot,
        Attack,
        GameOver,
        UpdateResult,
        QuitBattle
    }
}