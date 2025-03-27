namespace _Project.CodeBase.Runtime.Factories.Interfaces
{
    public interface IFactory<T>
    {
        public T Create();
    }
    
    public interface IFactory<T, TParam>
    {
        public T Create(TParam param);
    }
    
    public interface IFactory<T, TParam1, TParam2>
    {
        public T Create(TParam1 param1, TParam2 param2);
    }
    
    public interface IFactory<T, TParam1, TParam2, TParam3>
    {
        public T Create(TParam1 param1, TParam2 param2, TParam3 param3);
    }
    
    public interface IFactory<T, TParam1, TParam2, TParam3, TParam4>
    {
        public T Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4);
    }
    
    public interface IFactory<T, TParam1, TParam2, TParam3, TParam4, TParam5>
    {
        public T Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5);
    }
    
    public interface IFactory<T, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>
    {
        public T Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6);
    }
    
    public interface IFactory<T, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>
    {
        public T Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7);
    }
}