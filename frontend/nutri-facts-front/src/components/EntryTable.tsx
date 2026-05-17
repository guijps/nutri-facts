import {  useEffect, useState } from "react";
import type { NutritionFacts, ProductEntry } from "../types/product";
import { EntryRow } from "./EntryRow";


function sanitizeEntry(entry: ProductEntry): ProductEntry {
  const n = (v: unknown) => (typeof v === "number" && isFinite(v) ? v : 0);
  const facts = (f: NutritionFacts) => ({
    calories: n(f?.calories),
    carbohydrates: n(f?.carbohydrates),
    fat: n(f?.fat),
    protein: n(f?.protein),
  });
  return {
    ...entry,
    quantity: n(entry.quantity),
    nutritionFacts: facts(entry.nutritionFacts),
    product: {
      ...entry.product,
      nutritionFacts: facts(entry.product?.nutritionFacts),
    },
  };
}
export function EntryTable({ refreshKey, onSaved }: { refreshKey?: number; onSaved?: () => void }) {
    
      const [entries, setEntries] = useState<ProductEntry[]>([]);
      const [error, setError] = useState<string | null>(null);
      useEffect(() => {
        const token = localStorage.getItem("token");
    
        fetch("http://localhost:5294/all", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
          .then((res) => {
            if (!res.ok) throw new Error("Failed to fetch entries");
            return res.json();
          })
          .then((data: ProductEntry[]) => setEntries(data.map(sanitizeEntry)))
          .catch((err) => setError(err.message));
      }, [refreshKey]);

    return (<ul className="space-y-4">
        {entries.map((entry) => (<EntryRow key={entry.id} entry={entry} onSaved={onSaved} />))}
      </ul>);
}