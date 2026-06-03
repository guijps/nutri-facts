import { useEffect, useState } from "react";
import { ProductService } from "../api/productService";
import ProductList from "./ProductList";
import type { Product } from "../types/product";

interface SearchProductByTextProps {
	onProductSelected: (product: Product) => void;
}

export function SearchProductByText({ onProductSelected }: SearchProductByTextProps) {
	const [products, setProducts] = useState<Product[]>([]);
	const [query, setQuery] = useState("");
	const [loading, setLoading] = useState(false);

	async function setHistory() {
		try {
			setLoading(true);
			const history = await ProductService.getHistory();
			console.log("Search history:", history);
			setProducts(history);
		} catch (error) {
			alert("Failed to fetch search history");
			console.error("Failed to fetch search history", error);
		} finally {
			setLoading(false);
		}

	}

	async function handleSave() {
		if (!query.trim()) {
			setProducts([]);
			return;
		}
		try {
			setLoading(true);
			const response = await ProductService.searchText(query);
			setProducts(response);
		} catch (error) {
			alert("Failed to search products by name");
			console.error("Failed to search products by name", error);
		} finally {
			setLoading(false);
		}
	}

	function handleProductClick(product: Product) {
		onProductSelected(product);
	}
	  useEffect(() => {
        void setHistory();
    }, []);
	return (
		<div>
			<input
				placeholder="Search product"
				value={query}
				onChange={(e) => setQuery(e.target.value)}
				className="w-full border rounded-2xl p-3 mb-4"
			/>
			<button disabled={loading} onClick={handleSave} className="w-full bg-black text-white rounded-2xl p-3">
				Search By Name
			</button>
			<ProductList products={products} onProductClick={handleProductClick} />
		</div>
	);
}
