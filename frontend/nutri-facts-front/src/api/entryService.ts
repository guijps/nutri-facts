import { api } from "./api"
import { CACHE } from "./cacheService"

export const EntryService = 
{
    async setApi(barcode: string, quantity: number) 
    {
        let request = await api(`/set?code=${encodeURIComponent(barcode)}&quantity=${encodeURIComponent(quantity)}`, {
            method: "POST",
        });
        var response = await request.json();
        return response;
    },

    async updateApi(entryId: string, quantity: number)
    {
        await api(`/update?entryId=${encodeURIComponent(entryId)}&quantity=${encodeURIComponent(quantity)}`, {
            method: "POST",
        });
    },

    async deleteApi(entryId: string){
        await api(`/delete?entryId=${encodeURIComponent(entryId)}`, {
            method: "DELETE",
        });
    }
}