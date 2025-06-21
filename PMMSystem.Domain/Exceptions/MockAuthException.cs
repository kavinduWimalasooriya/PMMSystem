namespace PMMSystem.Domain.Exceptions
{
  public class MockAuthException : Exception
  {
    public MockAuthException() :base("Authorization required: You must be logged in as an Admin to perform Status Update.")
    {
      
    }
  }
}
