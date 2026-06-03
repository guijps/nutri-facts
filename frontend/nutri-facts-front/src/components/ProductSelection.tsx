import { useState } from "react";
import type { Product } from "../types/product";
import BarcodeScanner from "./CodeScanner";
import { ProductService } from "../api/productService";
import { SearchProductByText } from "./SearchProductByText";

interface ProductSelectionProps {
  onProductSelected: (product: Product) => void;
}
export function ProductSelection({ onProductSelected }: ProductSelectionProps) 
{
  const [selectionMode, setSelectionMode] = useState<"text" | "barcode">("text");

  async function handleBarcodeScanned(barcode: string) {
    try {
      const product = await ProductService.getByBarcode(barcode);
      onProductSelected(product);
    } catch (error) {
      alert("Failed to fetch product by barcode");
      console.error("Failed to fetch product by barcode", error);
    }
  }

    return (
        <div>
            <h1 className="text-2xl font-bold mb-6">Search product</h1>
      <div className="grid grid-cols-2 gap-2 mb-4">
        <button
          onClick={() => setSelectionMode("text")}
          className={`rounded-2xl p-3 ${selectionMode === "text" ? "bg-black text-white" : "bg-gray-100 text-black"}`}
        >
          Search by Text
        </button>
        <button
          onClick={() => setSelectionMode("barcode")}
          className={`rounded-2xl p-3 ${selectionMode === "barcode" ? "bg-black text-white" : "bg-gray-100 text-black"}`}
        >
          Scan Barcode
        </button>
      </div>

      {selectionMode === "text" && <SearchProductByText onProductSelected={onProductSelected} />}
      {selectionMode === "barcode" && <BarcodeScanner onScan={handleBarcodeScanned} />}
        </div>
    );
}