﻿namespace Readify.BLL.Features.BookCategories.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
