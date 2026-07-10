namespace FuzzyGraph.Runtime
{
    public enum WriteBackOperation
    {
        Set,        //Replace or create a value
        Add,        //Increase an int or float value
        Subtract,   //Decrease an int or float value
        Toggle      //Reverse a bool value
    }
}