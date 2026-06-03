import { useState } from "react";
import { format, type Product } from "../types/product";

type QuantitySelectionProps = {
    product: Product | null;
    onSelectedQuantity: (quantity: number) => void;
}
export function QuantitySelection({ product, onSelectedQuantity }: QuantitySelectionProps) 
{

    const [quantity, setQuantity] = useState(100);
    return (
        <div>
            <h2>Selected Product: {product?.name}</h2>
            <p>Calories: {product ? format.format(product.nutritionFacts.calories * (quantity / 100)) : 0} kcal</p>
            <p>Carbohydrates: {product ? format.format(product.nutritionFacts.carbohydrates * (quantity / 100)) : 0} g</p>
            <p>Proteins: {product ? format.format(product.nutritionFacts.proteins * (quantity / 100)) : 0} g</p>
            <p>Fat: {product ? format.format(product.nutritionFacts.fat * (quantity / 100)) : 0} g</p>
            <input type="number" value={quantity} onChange={(e) => setQuantity(Number(e.target.value))} placeholder="Quantity in grams" className="w-full border rounded-2xl p-3 mb-4" />
            <button className="w-full bg-black text-white rounded-2xl p-3" onClick={() => onSelectedQuantity(quantity)}>Save</button>
        </div>
    );
}