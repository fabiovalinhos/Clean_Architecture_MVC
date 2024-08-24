using System.Reflection;
using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact(DisplayName = "Create Category with Valid State")]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Category(1, "Category Name");
        action.Should()
        .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateCategory_NegativeIdValue_DomainExcepdtionInvalidId()
    {
        Action action = () => new Category(-1, "Category Name");
        action.Should()
        .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
        .WithMessage("Invalid Id value");
    }

[Fact]
    public void CreateCategory_ShortNameValue_DomainExcepdtionShortName()
    {
        Action action = () => new Category(1, "Ca");
        action.Should()
        .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
        .WithMessage("Invalid name. Too short name, minimal 3 characters");
    }

    [Fact]
    public void CreateCategory_MissingNameValue_DomainExcepdtionRequiredName()
    {
        Action action = () => new Category(1, "Ca");
        action.Should()
        .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
        .WithMessage("Invalid name. Too short name, minimal 3 characters");
    }

    [Fact]
    public void CreateCategory_WithNullNameValue_DomainExcepdtionInvalidName()
    {
        Action action = () => new Category(1, null);
        action.Should()
        .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }
}