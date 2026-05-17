import type { ProductEntry } from "../types/product";

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
          <p className="font-medium">{entry.nutritionFacts.protein.toFixed(1)}g</p>
          <p className="text-gray-500">Protein</p>
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
  return (
    <div className="flex gap-4">
      <EntryCard entry={first} />
      <EntryCard entry={second} />
    </div>
  );
}
