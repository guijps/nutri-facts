import type { NutritionFacts, ProductEntry } from "../types/product";

type Props = {
  first: ProductEntry;
  second: ProductEntry;
};

function EntryCard({ entry }: { entry: ProductEntry }) {
  return (
    <div className="border rounded-2xl p-4 shadow-sm flex-1">
      <h3 className="font-semibold text-lg mb-3">{entry.product.name}</h3>
      <p className="text-sm text-gray-500 mb-3">{entry.quantity}g</p>
      <div className="grid grid-cols-2 gap-2 text-sm text-center">
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
          <p className="text-gray-500">Proteins</p>
        </div>
        <div className="bg-gray-100 rounded-xl p-2">
          <p className="font-medium">{entry.nutritionFacts.fat.toFixed(1)}g</p>
          <p className="text-gray-500">Fat</p>
        </div>
      </div>
    </div>
  );
}

export function InformationWindow({ first, second }: Props) {

  function sanitizeEntry(entry: ProductEntry): ProductEntry {
    const n = (v: unknown) => (typeof v === "number" && isFinite(v) ? v : 0);
    const facts = (f: NutritionFacts) => ({
      calories: n(f?.calories),
      carbohydrates: n(f?.carbohydrates),
      fat: n(f?.fat),
      proteins: n(f?.proteins),
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
  
  return (
    <div className="flex gap-4">
      <EntryCard entry={sanitizeEntry(first)} />
      <EntryCard entry={sanitizeEntry(second)} />
    </div>
  );
}
