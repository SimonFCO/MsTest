using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsTest;

namespace MsTest.Tests;
[TestClass]
public class LibraryTests
{
    private LibrarySystem _libSys;

    [TestInitialize]
    public void Setup()
    {
        _libSys = new LibrarySystem();
    }


    [TestMethod]
    public void AddBook_CreateBookWithoutISBN_ReturnFalse()
    {
        var book = new Book("TITLE", "AUTHOR", "", 404);
        var Result = _libSys.AddBook(book);
        Assert.IsFalse(Result);
    }

    [TestMethod]
    public void Add2Books_SeeIf2SameISBNWorks_ReturnFalse()
    {
        var book1 = new Book("TITLE", "AUTHOR", " 978-91-47-15045-8", 404);
        _libSys.AddBook(book1);
        var book2 = new Book("TITLE", "AUTHOR", " 978-91-47-15045-8", 404);
        var Result = _libSys.AddBook(book2);
        Assert.IsFalse(Result);
    }

    [TestMethod]
    public void RemoveBook_SeeIfPossibleToRemoveThyBook_ReturnTrue()
    {
        var bookToRemove = new Book("TITLE", "AUTHOR", "9191", 404);
        _libSys.AddBook(bookToRemove);
        var result = _libSys.RemoveBook("9191");
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void RemoveBorrowedBook_BorrowedBooksShouldNotBeAchiveblyRemoveableBySystematicalMeans_ReturnTrue()
    {
        // B ook got thyself removed and it was not very good, please fix
        var bookToRemove = new Book("TITLE", "AUTHOR", "9191", 404);
        _libSys.AddBook(bookToRemove);
        _libSys.BorrowBook("9191");
        var result = _libSys.RemoveBook("9191");
        Assert.IsFalse(result); 
    }

    [TestMethod] //Author Search
    public void SearchForBook_TitleSearchNotBeCaseSensative_ReturnTrue()
    {
        var Book = new Book("TITLE", "AUTHOR", "9191", 404);
        _libSys.AddBook(Book);
        var SearchedAuthorBooks = _libSys.SearchByTitle("title");
        Assert.IsTrue(SearchedAuthorBooks.Count > 0);
    }

    [TestMethod] //Author Search
    public void SearchForBook_AuthorSearchNotBeCaseSensative_ReturnTrue()
    {
        var Book = new Book("TITLE", "AUTHOR", "9191", 404);
        _libSys.AddBook(Book);
        var SearchedAuthorBooks = _libSys.SearchByAuthor("author");
        Assert.IsTrue(SearchedAuthorBooks.Count > 0);
    }

    [TestMethod]
    public void SearchWithBrokenEnglish_TitleSearchingWithPartialyMatchingWordShouldFindResult_ReturnTrue()
    {
        var Book = new Book("TITLE", "AUTHOR", "9191", 404);
        _libSys.AddBook(Book);
        var SearchedAuthorBooks = _libSys.SearchByTitle("itle");
        Assert.IsTrue(SearchedAuthorBooks.Count > 0);
    }

    [TestMethod]
    public void SearchWithBrokenEnglish_AuthorSearchingWithPartialyMatchingWordShouldFindResult_ReturnTrue()
    {
        var Book = new Book("TITLE", "AUTHOR", "9191", 404);
        _libSys.AddBook(Book);
        var SearchedAuthorBooks = _libSys.SearchByAuthor("autho");
        Assert.IsTrue(SearchedAuthorBooks.Count > 0);
    }

    // PART 2

    

}
