import React from 'react';
import type { Product } from '../types/product';


interface ProductListProps {
  products: Product[];
  onProductClick: (product: Product) => void;
}

const ProductList: React.FC<ProductListProps> = ({ products, onProductClick }) => {
  return (
    <ul>
      {products.map((product) => (
        <li
          key={product.name}
          style={{ cursor: 'pointer', padding: '8px', borderBottom: '1px solid #eee' }}
          onClick={() => onProductClick(product)}
        >
          {product.name}
          <div className="grid grid-cols-4 gap-2 text-sm text-center">
              <div className="bg-gray-100 rounded-xl p-2">
                <p className="font-medium">{product.nutritionFacts.calories.toFixed(1)}</p>
                <p className="text-gray-500">kcal</p>
              </div>
              <div className="bg-gray-100 rounded-xl p-2">
                <p className="font-medium">{product.nutritionFacts.carbohydrates.toFixed(1)}g</p>
                <p className="text-gray-500">Carbs</p>
              </div>
              <div className="bg-gray-100 rounded-xl p-2">
                <p className="font-medium">{product.nutritionFacts.proteins.toFixed(1)}g</p>
                <p className="text-gray-500">Protein</p>
              </div>
              <div className="bg-gray-100 rounded-xl p-2">
                <p className="font-medium">{product.nutritionFacts.fat.toFixed(1)}g</p>
                <p className="text-gray-500">Fat</p>
              </div>
            </div>
        </li>
      ))}
    </ul>
  );
};

export default ProductList;
