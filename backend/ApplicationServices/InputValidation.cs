using NutriFacts.Domain.Exceptions;

public static class InputValidation
{
    public static void validateUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new InvalidUserIdException();
        }
    }

    public static void validateEntryId(Guid entryIdString)
    {
        if (entryIdString == Guid.Empty)    
        {
            throw new InvalidEntryIdException(entryIdString.ToString());
        }
    }

    public static void validateQuantity(double quantity)
    {
        if (quantity <= 0)
        {
            throw new InvalidQuantityException(quantity);
        }
    }

    public static void validateBarcode(string barcode)
    {
        if (string.IsNullOrWhiteSpace(barcode))
        {
            throw new InvalidBarcodeException();
        }
    }

    public static void validateEntry(IProductEntry entry)
    {
        if (entry == null)
        {
            throw new InvalidEntryException();
        }
    }

    public static void validateProduct(IProduct product)
    {
        if (product == null)
        {
            throw new InvalidProductException("null");
        }

        if(product is not Product)
        {
            throw new InvalidProductException("not of type Product");
        }
    }

    public static void validateProductId(string productId)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            throw new InvalidProductException(productId);
        }
    }
}