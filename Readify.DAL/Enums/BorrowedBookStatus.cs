public enum BorrowedBookStatus
{
    Active = 0,       // Book is still with user
    Returned = 1,     // Book was returned
    Overdue = 2       // (Optional) Book is overdue based on DueDate
}