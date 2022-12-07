using BayraktarlarWebsite.UI.Models;
using FluentValidation;

namespace BayraktarlarWebsite.UI.ValidationRules.SeoInfoValidators
{
    public class SeoInfoValidation:AbstractValidator<SeoInfo>
    {
        public SeoInfoValidation()
        {
            RuleFor(s => s.Title).NotEmpty().WithMessage("Title alanı boş bırakılamaz").MinimumLength(5).WithMessage("En az 5 karakter olmalıdır").MaximumLength(50).WithMessage("En çok 50 karakter olabilir");

            RuleFor(s => s.SeoAuthor).NotEmpty().WithMessage("SeoAuthor alanı boş bırakılamaz").MinimumLength(5).WithMessage("En az 5 karakter olmalıdır").MaximumLength(50).WithMessage("En çok 50 karakter olabilir");

            RuleFor(s => s.MenuTitle).NotEmpty().WithMessage("MenuTitle alanı boş bırakılamaz").MinimumLength(5).WithMessage("En az 5 karakter olmalıdır").MaximumLength(50).WithMessage("En çok 50 karakter olabilir");

            RuleFor(s => s.SeoDescription).NotEmpty().WithMessage("SeoDescription alanı boş bırakılamaz").MinimumLength(5).WithMessage("En az 5 karakter olmalıdır").MaximumLength(50).WithMessage("En çok 50 karakter olabilir");

            RuleFor(s => s.SeoTags).NotEmpty().WithMessage("SeoTags alanı boş bırakılamaz").MinimumLength(5).WithMessage("En az 5 karakter olmalıdır").MaximumLength(50).WithMessage("En çok 50 karakter olabilir");
        }
    }
}
