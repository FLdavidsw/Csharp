using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks.Dataflow;

public class LinqQueries
{
    private List<Book> BooksCollection = new List<Book>();
    public LinqQueries()
    {
        using(StreamReader reader = new StreamReader("books.json"))
        {
            string json = reader.ReadToEnd();
            this.BooksCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json, new System.Text.Json.JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
        }
    }

    public IEnumerable<Book> AllCollection()
    {
        return BooksCollection;
    }
    //Where operator: "Where" works as a filter that uses a conditional we declare within it.
    public IEnumerable<Book> BooksAfter2000()
    {
        return BooksCollection.Where(item => item.publishedDate.Year >= 2000);
    }
    public IEnumerable<Book> BooksBefore2000()
    {   
        //extension method
        // return BooksCollection.Where(item => item.publishedDate.Year < 2000);

        //query expression
        return from c in BooksCollection  where c.publishedDate.Year < 2000 select c;
    }
    public IEnumerable<Book> ActionBooks250()
    {
        //extension method
        //return BooksCollection.Where(book => book.PageCount >= 250 && book.Title.Contains("in Action"));

        //query expression
        return from c in BooksCollection where c.PageCount >= 250 && c.Title.Contains("in Action") select c;
    }

    // All Operator: it validates that a conditional is met in all elements. It returns a bool
    public bool AllBooksHaveStatus() 
    {
        return BooksCollection.All(book => book.Status!=string.Empty);
    }

    // Any Operator: It validates if at least one element meets a condition. It returns a bool
    public bool AnyBookFrom2005() 
    {
        return BooksCollection.Any(book => book.publishedDate.Year == 2005);
    }
    // Contains Operator: It validates if an element or a collection of elements contains an specific element
    public IEnumerable<Book> CategoryBooks(string category)
    {
        return BooksCollection.Where(book => book.Categories.Contains(category));
    }
    //OrderBy Operator: It order a collection in ascending order
    public IEnumerable<Book> CategoryBooksAscOrderedByName(string category)
    {
        //return BooksCollection.Where(book => book.Categories.Contains(category)).OrderBy(book => book.Title);
        //Reusing our CategoryBooks method to reutilize them rather than repeat the same
        return CategoryBooks(category).OrderBy(book => book.Title);
    }
    //OrderByDescending Operator: It order a collection in descending order
    public IEnumerable<Book> BooksWithXPagesDescOrdered(int pages)
    {
        return BooksCollection.Where(book => book.PageCount >= 450).OrderByDescending(book => book.PageCount);
    }
    // Take Operator: It takes a predetermined amount of elements from a collection or list
    public IEnumerable<Book> TheXNewestBooksOfACategory(string category, int numberOfBooks)
    {
        return CategoryBooks(category)
        .OrderByDescending(book => book.publishedDate)
        .Take(numberOfBooks);
        /*Also we have other Take Operators which are the following ones:
        - TakeLast: It takes elements from the end of the collection
        - TakeWhile: It takes all the elements that meet the condition till finds the first one that doesn't
        */
    }
    // Skip Operator: It skips a predetermined amound of elements from a collection or list
    public IEnumerable<Book> ThirdFourthBooks400OrMorePages() 
    {
        return BooksCollection
        .Where(book => book.PageCount >= 400)
        .Take(4)
        .Skip(2);
         /*Also we have other Skip Operators which are the following ones:
        - SkipLast: It Skips elements from the end of the collection
        - SkipWhile: It skips all the elements that meet the condition till finding the first one that doesn't meet it to return the ones left.
        */
    }
    //TakeWhile: It takes all the elements that meet the condition till finds the first one that doesn't
    public IEnumerable<Book> BooksPublishedDateLessThanXYear(int year) 
    {
        return BooksCollection
        .OrderBy(book => book.publishedDate.Year)
        .TakeWhile(book => book.publishedDate.Year < year);
    }
    /*SkipWhile: It skips all the elements that meet the condition till finding 
    the first one that doesn't meet it to return the ones left without mattering if 
    the ones left meet the condition or not.
    */
    public IEnumerable<Book> BooksPublishedDateGreaterThanXYear(int year) 
    {
        return BooksCollection
        .SkipWhile(book => book.publishedDate.Year < year);
    }
    /*
    Select Operator: Select Operator allows us to select only some specific columns rather than
    the whole collection.
    */

    public IEnumerable<Item> XFirstBookFromtTheCollection() 
    {
        return BooksCollection.Take(3)
        .Select(book => new Item() { Title = book.Title, PageCount = book.PageCount});// it's not necessary to return a nickname 
    
    }
    /*
    Count and LongCount Operator: It counts the amount of elements regarding a conditional, filter and so on.
    It's unnecesary to use them with a where previously, as Count and LongCount have their own filters.
    */
    public int BooksCount(int minPages, int maxPages)
    {
        return BooksCollection.Count(book => book.PageCount >= minPages && book.PageCount <= maxPages);
        //it exist LongCount as well, but It turns out innecesary when the number is small,
        //as the only difference between long and long count is the variable size
    }
    /*
    Min Operator: It returns the smallest number 
    */
    public DateTime DatePublishedMin()
    {
        return BooksCollection.Min(book => book.publishedDate);
    }
    /*
    Max Operator: It returns the greatest number 
    */
    public int BookMaxPages()
    {
        return BooksCollection.Max(book => book.PageCount);
    }
    /*
    MinBy Operator: MinBy Operator returns the element that has the smallest value 
    in a specific column
    */
    public Book BookLessPagesDiff0() 
    {
        return BooksCollection.Where(book => book.PageCount > 0).MinBy(book => book.PageCount);
    }
    /*
    MaxBy Operator: MaxBy Operator returns the element that has the greatest value 
    in a specific column
    */
    public Book BookDatePublishedMax()
    {
        return BooksCollection.MaxBy(book => book.publishedDate);
    }
    /*
    Sum Operator: It makes a sum of all the elements that meet a condition.
    */
    public int SumPagesBooks0500()
    {
        return BooksCollection.Where(book => book.PageCount > 0 && book.PageCount <= 500).Sum(book => book.PageCount);
    }
    /*
    Aggregate Operator: It add or concatenate every element that is iterated.
    */
    public string TitlesAfterXYear(int year)
    {
        return BooksCollection
                .Where(book => book.publishedDate.Year >= 2015)
                .Aggregate("", (BooksTitles, next) =>
                {
                    if(BooksTitles != string.Empty)
                        BooksTitles += " - " + next.Title;
                    else
                        BooksTitles += next.Title;

                    return BooksTitles;
                });
    }
    //Average Operator: It calculate the average of an specific set of data
    public double TitleCharactersAverage()
    {
        return BooksCollection.Average(book => book.Title.Length);
    }

    public double PageCountAverage()
    {
        return BooksCollection
        .Where(book => book.PageCount > 0)
        .Average(book => book.PageCount);
    }
    //GroupBy Operator: It is a clause to group elements regarding a specific property
    public IEnumerable<IGrouping<int, Book>> BooksAfter2000GroupedByYear()
    {
        return BooksCollection
                .Where(book => book.publishedDate.Year >= 2000)
                .GroupBy(book => book.publishedDate.Year);
    }
    /*
    //ToLookUp Operator: It is a clause to group elements regarding a specific 
    property in a dictionary rather than an IEnumerable
    */
    public ILookup<char, Book> BooksDictionaryPerLetter()
    {
        return BooksCollection.ToLookup(book => book.Title[0], book => book);
    }
    public IEnumerable<Book> BooksAfterXWithMoreThanX(int year, int pageCount)
    {
        var BooksAfterX = BooksCollection.Where(book => book.publishedDate.Year >= year);
        var BooksMore500Pages = BooksCollection.Where(book => book.PageCount > pageCount);
        
        return BooksAfterX.Join(BooksMore500Pages, x => x.Title, y => y.Title, (p, x) => p);
    }
} 