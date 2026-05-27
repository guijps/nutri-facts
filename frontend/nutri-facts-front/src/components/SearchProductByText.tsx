import { useState } from "react";
import { ProductService } from "../api/productService";
import ProductList from "./ProductList";
import type { Product } from "../types/product";

interface SearchProductByTextProps {
	onProductSelected: (product: Product) => void;
}

export function SearchProductByText({ onProductSelected }: SearchProductByTextProps) {
	const [products, setProducts] = useState<Product[]>([]);
	const [query, setQuery] = useState("");

	async function handleSave() {
		if (!query.trim()) {
			setProducts([]);
			return;
		}

		try {
			const response = await ProductService.searchText(query);

			if (!response.ok) {
				console.error("Failed to search products by name" + response.statusText);
				return;
			}

			setProducts(await response.json());
		} catch (error) {
			console.error("Failed to search products by name", error);
		}
	}

	function handleProductClick(product: Product) {
		onProductSelected(product);
	}

	return (
		<div>
			<input
				placeholder="Search product"
				value={query}
				onChange={(e) => setQuery(e.target.value)}
				className="w-full border rounded-2xl p-3 mb-4"
			/>
			<button onClick={handleSave} className="w-full bg-black text-white rounded-2xl p-3">
				Search By Name
			</button>
			<ProductList products={products} onProductClick={handleProductClick} />
		</div>
	);
}
