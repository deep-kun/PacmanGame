
namespace PacManLibrary.Interfaces
{
    public interface ICharacter : IMoveble
    {
        float Score { get; set; }
        bool IsLive { get; set; }
        int Life { get; set; }
        Direction Direction { get; }

    }
}
