import { api } from "./api";
import { CACHE } from "./cacheService"

export const ProductService = 
{
    async getByBarcode(barcode: string) 
    {
        if (CACHE.has(barcode)) 
        {
            return CACHE.get(barcode);
        }
        
        const response = await api(`/products/${barcode}`);

        if (!response.ok) 
        {
            throw new Error(`Failed to fetch product with barcode ${barcode}`);
        }
        
        const product = await response.json();
        CACHE.setOrUpdate(barcode, product);
        console.log(`Product with barcode ${barcode} fetched from API and stored in cache.`, product);
        return product;
    }


}