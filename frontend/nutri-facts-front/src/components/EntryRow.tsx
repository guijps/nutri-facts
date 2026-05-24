import { useState } from "react";
import type { NutritionFacts, ProductEntry } from "../types/product";
import { EntryService } from "../api/entryService";

export function EntryRow
({ entry, onSaved }: { entry: ProductEntry, onSaved?: () => void }) {
    const [editing, setEditing] = useState(false);
    const [quantity, setQuantity] = useState(String(entry.quantity));

    async function handleSave() {
        await EntryService.updateApi(entry.id, Number(quantity));
        setEditing(false);
        onSaved?.();
    }

    async function handleDelete() {
        await EntryService.deleteApi(entry.id);
        onSaved?.();
    }

    return (
    <li
            key={entry.id}
            className="border rounded-2xl p-4 shadow-sm"
          >
            <div className="flex justify-between items-center mb-2">
              <span className="font-semibold text-lg">
                {entry.product.name}
              </span>
              <div className="flex items-center gap-2">
                <input
                  type="number"
                  value={quantity}
                  disabled={!editing}
                  onChange={(e) => setQuantity(e.target.value)}
                  className="w-20 text-sm text-gray-500 border rounded-xl px-2 py-1 disabled:bg-transparent disabled:border-transparent"
                />
                <button
                  onClick={() => setEditing((e) => !e)}
                  className="text-xs px-2 py-1 rounded-xl border hover:bg-gray-100"
                >
                  {editing ? "Cancel" : "Edit"}
                </button>
                {editing && (
                  <button
                    onClick={handleSave}
                    className="text-xs px-2 py-1 rounded-xl border bg-black text-white hover:bg-gray-800"
                  >
                    Save
                  </button>
                )}
                <button
                  onClick={handleDelete}
                  className="text-xs px-2 py-1 rounded-xl border border-red-400 text-red-500 hover:bg-red-50"
                >
                  Delete
                </button>
              </div>
            </div>

            <div className="grid grid-cols-4 gap-2 text-sm text-center">
              <div className="bg-gray-100 rounded-xl p-2">
                <p className="font-medium">{entry.nutritionFacts.calories.toFixed(1)}</p>
                <p className="text-gray-500">kcal</p>
              </div>
              <div className="bg-gray-100 rounded-xl p-2">
                <p className="font-medium">{entry.nutritionFacts.carbohydrates.toFixed(1)}g</p>
                <p className="text-gray-500">Carbs</p>
              </div>
              <div className="bg-gray-100 rounded-xl p-2">
                <p className="font-medium">{entry.nutritionFacts.proteins.toFixed(1)}g</p>
                <p className="text-gray-500">Protein</p>
              </div>
              <div className="bg-gray-100 rounded-xl p-2">
                <p className="font-medium">{entry.nutritionFacts.fat.toFixed(1)}g</p>
                <p className="text-gray-500">Fat</p>
              </div>
            </div>
          </li>
    );
}