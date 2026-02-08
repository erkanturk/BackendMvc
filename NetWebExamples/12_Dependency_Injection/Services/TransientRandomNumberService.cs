namespace _12_Dependency_Injection.Services
{
    public class TransientRandomNumberService
    {
        private readonly int _randomNumber;
        public TransientRandomNumberService()
        {
            _randomNumber= new Random().Next(1, 1001);
        }
        public int GetRandomNumber()
        {
            return _randomNumber;
        }
    }
}
