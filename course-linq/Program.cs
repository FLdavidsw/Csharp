
LinqQueries queries = new LinqQueries();

//All collection
//PrintValues(queries.AllCollection());

//books published after 2000's
//PrintValues(queries.BooksAfter2000());

//books published after 2000's
//PrintValues(queries.BooksBefore2000());

//books that has the word "In Action" in their titles with 250 pages or more
//PrintValues(queries.ActionBooks250());

//All books have status
//Console.WriteLine($"Do all books have status? {queries.AllBooksHaveStatus()}");

//Is there any book from 2005?
//Console.WriteLine($"Is there any book from 2005? {queries.AnyBookFrom2005()}");

// Python Books
//PrintValues(queries.CategoryBooks("Python"));

// Java books ordered by name
//PrintValues(queries.CategoryBooksAscOrderedByName("Java"));

// Books with X pages or more in descending order
//PrintValues(queries.BooksWithXPagesDescOrdered(450));

//A specific amount of the most recent books of a specific category
//PrintValues(queries.TheXNewestBooksOfACategory("Java", 4));

// The third and fourth books with 400 pages or more
//PrintValues(queries.ThirdFourthBooks400OrMorePages());

// Books published before X year
//PrintValues(queries.BooksPublishedDateLessThanXYear(2012));

// Books published after X year
//PrintValues(queries.BooksPublishedDateGreaterThanXYear(2012));

//Three books with their titles and pages count
//PrintValues2(queries.XFirstBookFromtTheCollection());

//Books with a specific number of pages
//Console.WriteLine(queries.BooksCount(200, 500));

//the minimum published date
//Console.WriteLine($"the oldest published date: {queries.DatePublishedMin()}");

//the minimum published date
//Console.WriteLine($"the book with more pages: {queries.BookMaxPages()} pages");

//returning the book with less pages unlike 0
//var lessPagesBook = queries.BookLessPagesDiff0();
//Console.WriteLine($"{lessPagesBook.Title}, {lessPagesBook.PageCount}");

//returning the book with the most recent published date
//var MostRecentBook = queries.BookDatePublishedMax();
//Console.WriteLine($"{MostRecentBook.Title}, {MostRecentBook.publishedDate.ToShortDateString()}");

//Returning the pages sum of the page count from the books greater than 0 and smaller or equal to 500
//Console.WriteLine($"The total amount of pages from the books greater than 0 and smaller or equal to 500 is: {queries.SumPagesBooks0500()}");

//Book's titles published after 2015
//Console.WriteLine(queries.TitlesAfterXYear(2015));

//Title's Characters average of the books
//Console.WriteLine($"Title's Characters average of the books {queries.TitleCharactersAverages()}");

//PageCount average
//Console.WriteLine($"PageCount Average {queries.PageCountAverage()}");

//Books published after 2000 grouped by year
//PrintGroup(queries.BooksAfter2000GroupedByYear());

//Books dictionary grouped by their title first letter
//var lookUpDictionary = queries.BooksDictionaryPerLetter();
//PrintDictionary(lookUpDictionary, 'S');

//Books filtered with the join clause
PrintValues(queries.BooksAfterXWithMoreThanX(2005, 500));


void PrintValues(IEnumerable<Book> booklist)
{   //the minus number in the first formatting parameter means that the text will be justified to the left
    Console.WriteLine("{0,-60}, {1, 15}, {2, 11}, {3, 11}\n", "Title", "N. Paginas", "Fecha Publicación", "Categories");
    foreach(var item in booklist)
    {
        Console.WriteLine("{0,-60}, {1, 15}, {2, 11}, {3, 11}", item.Title, item.PageCount, item.publishedDate.ToShortDateString(), item.Categories[0]);
    }
}
void PrintValues2(IEnumerable<Item> booklist)
{
    Console.WriteLine("{0,-60}, {1, 15}\n", "Title", "N. Paginas");
    foreach(var item in booklist)
    {
        Console.WriteLine("{0,-60}, {1, 15}", item.Title, item.PageCount);
    }
}

void PrintGroup(IEnumerable<IGrouping<int,Book>> bookList)
{
    foreach(var group in bookList)
    {
        Console.WriteLine("");
        Console.WriteLine($"Grupo: { group.Key }");
        Console.WriteLine("{0,-60} {1, 15} {2, 15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
        foreach(var item in group)
        {
            Console.WriteLine("{0,-60} {1, 15} {2, 15}",item.Title,item.PageCount,item.publishedDate.Date.ToShortDateString()); 
        }
    }
}
void PrintDictionary(ILookup<char, Book> bookList, char letter)
{
	Console.WriteLine("{0,-60} {1, 15} {2, 15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
	foreach (var item in bookList[letter])
	{
        	Console.WriteLine("{0,-60} {1, 15} {2, 15}",item.Title,item.PageCount,item.publishedDate.Date.ToShortDateString()); 
	}

}