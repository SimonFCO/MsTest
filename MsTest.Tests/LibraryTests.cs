using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsTest;

namespace MsTest.Tests;
[TestClass]
public class LibraryTests
{
    private LibrarySystem _libSys;

    public LibraryTests()
    {
        _libSys = new LibrarySystem();
    }

    [TestMethod]
    public void AddBook_SeeIfBookHasISBN_ReturnFalse()
    {
        // This will create a new book object without a isbn :)
        var book = new Book("TITLE", "AUTHOR", "", 404);
        // This will create a Result variable from the function Create BOOKK :D
        var Result = _libSys.AddBook(book);
        // This will hopefully fail as creating a book should FAIL :)
        Assert.IsFalse(Result);
    }
}
