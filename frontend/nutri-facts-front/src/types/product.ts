export interface NutritionFacts {
  calories: number;
  carbohydrates: number;
  fat: number;
  proteins: number;
}

export interface Product {
  name: string;
  id: string;
  nutritionFacts: NutritionFacts;
}

export interface ProductEntry {
  id: string;
  product: Product;
  quantity: number;
  nutritionFacts: NutritionFacts;
}