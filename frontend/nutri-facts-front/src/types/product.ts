export interface NutritionFacts {
  calories: number;
  carbohydrates: number;
  fat: number;
  protein: number;
}

export interface Product {
  name: string;
  nutritionFacts: NutritionFacts;
}

export interface ProductEntry {
  id: string;
  product: Product;
  quantity: number;
  nutritionFacts: NutritionFacts;
}