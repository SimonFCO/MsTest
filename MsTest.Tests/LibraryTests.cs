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
    public void RemoveBorrowedBook_BorrowedBooksShouldNotBeAchiveblyRemoveableBySystematicalMeans_ReturnFalse()
    {
        var bookToRemove = new Book("TITLE", "AUTHOR", "9191", 404);
        _libSys.AddBook(bookToRemove);
        _libSys.BorrowBook("9191");
        var result = _libSys.RemoveBook("9191");
        Assert.IsFalse(result); 
    }

    [TestMethod]
    public void SearchForBook_TitleSearchNotBeCaseSensative_ReturnTrue()
    {
        var Book = new Book("TITLE", "AUTHOR", "9191", 404);
        _libSys.AddBook(Book);
        var SearchedAuthorBooks = _libSys.SearchByTitle("title");
        Assert.IsTrue(SearchedAuthorBooks.Count > 0);
    }

    [TestMethod]
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

    [TestMethod]
    public void BorrowedBookCheck_BorrowedBooksShouldBeMarkedBorrowed_ReturnTrue()
    {
        var Book = new Book("TITLE", "AUTHOR", "9191", 404);
        _libSys.AddBook(Book);
        _libSys.BorrowBook("9191");
        var IsBookBorrowed = _libSys.SearchByISBN("9191");
        Assert.IsTrue(IsBookBorrowed.IsBorrowed);
    }

    [TestMethod]
    public void BorrowAlreadyBorrowed_BorrowedBooksCantBeBorrowedAgain_ReturnFalse()
    {
        var Book = new Book("TITLE", "AUTHOR", "9191", 404);
        _libSys.AddBook(Book);
        _libSys.BorrowBook("9191");
        var borrowAgain = _libSys.BorrowBook("9191");
        Assert.IsFalse(borrowAgain);
    }

    [TestMethod]
    public void BorrowBook_CheckBorrowDateIsCorrect_ReturnTrue()
    {
        var book = new Book("TITLE", "AUTHOR", "404", 2026);
        _libSys.AddBook(book);
  
        DateTime before = DateTime.Now;
        _libSys.BorrowBook("404");
        DateTime after = DateTime.Now;

        bool checkDate = book.BorrowDate >= before && book.BorrowDate <= after;

        Assert.IsTrue(checkDate);

    }

    [TestMethod]
    public void ReturnBook_SeeIfReturnDateResets_ReturnTrue()
    {
        var Book = new Book("TITLE", "AUTHOR", "9191", 404);
        _libSys.AddBook(Book);
        _libSys.BorrowBook("9191");
        _libSys.ReturnBook("9191");

        Assert.IsNull(Book.BorrowDate);
    }

    [TestMethod]
    public void ReturnBook_OnlyBorrowedBooksCanBeReturned_ReturnFalse()
    {
        var Book = new Book("TITLE", "AUTHOR", "9191", 404);
        _libSys.AddBook(Book);
        var returnBookTest = _libSys.ReturnBook("9191");

        Assert.IsFalse(returnBookTest);
    }

    [TestMethod]
    public void CheckBookOverdue_CheckIfBookIsNotOverdue_ReturnFalse()
    {
        var book = new Book("TestBok", "Författare", "9191", 2026);
        _libSys.AddBook(book);
        _libSys.BorrowBook("9191");
        book.BorrowDate = DateTime.Now.AddDays(-5);

        bool actual = _libSys.IsBookOverdue("9191", 10);

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void CalculateLateFee_SeeIfLateFeeCalculationCorrect_ReturnTrue()
    {
        var book = new Book("TestBok", "Författare", "9191", 2026);
        _libSys.AddBook(book);
        _libSys.BorrowBook("9191");
        book.BorrowDate = DateTime.Now.AddDays(-15);

        decimal lateFees = _libSys.CalculateLateFee("9191", 5);
        decimal ActuallLateFee = 0.5m * 5;

        var FeeCorrect = lateFees == ActuallLateFee;
        Assert.IsTrue(FeeCorrect);
    }
}
