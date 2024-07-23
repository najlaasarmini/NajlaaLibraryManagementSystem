﻿using System.ComponentModel.DataAnnotations;

namespace NajlaaLibraryManagementSystem.Dtos.Language
{
    public class CreateLanguageDto
    {
        [StringLength(50, MinimumLength = 4, ErrorMessage = "LanguageName must be between 4 and 50 characters.")]
        public string LanguageName { get; set; }
    }
}
