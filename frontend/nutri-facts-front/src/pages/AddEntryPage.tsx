import { useState } from "react";
import { ProductService } from "../api/productService";
import ProductList from "../components/ProductList";
import { ProductSelection } from "../components/ProductSelection";
import type { Product } from "../types/product";
import { QuantitySelection } from "../components/QuantitySelection";
import { EntryService } from "../api/entryService";
import { useNavigate } from "react-router-dom";

export function AddEntryPage() 
{
    const navigate = useNavigate();
      async function handleProductSelected(product: Product) {
        setProduct(product);
        setStep(2);
      }
      async function handleQuantitySelected(quantity: number) {
        setQuantity(quantity);
        const reponse = await EntryService.setApi(product!.id, quantity/100);
        if (!reponse.ok)
            console.error("Failed to save entry" + reponse.statusText);
        navigate("/home");
      }

      const [step, setStep] = useState(1);
    //state 0 - you basically select the product 
    const [quantity, setQuantity] = useState(0);
    const [product, setProduct] = useState<Product | null>(null);
    const [query, setQuery] = useState("");
    return (
        <div>
            {(step === 1) && <ProductSelection onProductSelected={handleProductSelected} />}
            {(step === 2) && <QuantitySelection product={product} onSelectedQuantity={handleQuantitySelected} />}
        </div>
    );
}