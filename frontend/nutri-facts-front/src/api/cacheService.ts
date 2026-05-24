const cache  = new Map<string, any>();
export const CACHE = {
    get: (key: string) => {
        return cache.get(key);
    },
    setOrUpdate: (key: string, value: any) => {
        cache.set(key, value);
    },
    has: (key: string) => {
        return cache.has(key);
    },
    remove: (key: string) => {
        cache.delete(key);
    },
    clear: () => {        
        cache.clear();
    }



}
