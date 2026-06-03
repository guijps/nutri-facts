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
        
        const response = await api(`/api/product/${barcode}`);

        if (!response.ok) 
        {
            throw new Error(`Failed to fetch product with barcode ${barcode}`);
        }
        
        const product = await response.json();
        CACHE.setOrUpdate(barcode, product);
        console.log(`Product with barcode ${barcode} fetched from API and stored in cache.`, product);
        return product;
    },

    async searchText(query: string)
    {
        const response = await api(`/search?query=${encodeURIComponent(query)}`);

        if (!response.ok) 
        {
            throw new Error(`Failed to search products with query ${query}`);
        }

        return response.json();
    },
    async getHistory()
    {
        const response = await api(`/history`);
        if (!response.ok)        {
            throw new Error(`Failed to fetch search history`);
        }
        const history = await response.json();
        return history;
    }

}