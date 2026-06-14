namespace NutriFacts.Domain.Exceptions;

/// <summary>
/// Base exception for domain-specific errors.
/// </summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}

/// <summary>
/// Thrown when a product is not found for a given barcode.
/// </summary>
public sealed class ProductNotFoundException(string barcode)
    : DomainException($"Product not found for barcode '{barcode}'.");

/// <summary>
/// Thrown when a product is invalid.
/// </summary>
public sealed class InvalidProductException(string text)
    : DomainException($"Invalid product: '{text}'.");

/// <summary>
/// Thrown when an entry ID is invalid or cannot be parsed.
/// </summary>
public sealed class InvalidEntryIdException(string value)
    : DomainException($"Invalid entry id: '{value}'.");

/// <summary>
/// Thrown when an entry is invalid or cannot be parsed.
/// </summary>
public sealed class InvalidEntryException()
    : DomainException("Entry object is invalid.");

/// <summary>
/// Thrown when a quantity is invalid (must be greater than zero).
/// </summary>
public sealed class InvalidQuantityException(double value)
    : DomainException($"Quantity must be greater than zero. Received: {value}.");

/// <summary>
/// Thrown when a barcode is missing or empty.
/// </summary>
public sealed class InvalidBarcodeException()
    : DomainException("Barcode is required and cannot be empty.");

/// <summary>
/// Thrown when a user ID is missing or invalid.
/// </summary>
public sealed class InvalidUserIdException()
    : DomainException("User ID is required and cannot be empty.");

/// <summary>
/// Thrown when an entry is not found for the given ID and user.
/// </summary>
public sealed class EntryNotFoundException(string entryId)
    : DomainException($"Entry '{entryId}' not found for this user.");
