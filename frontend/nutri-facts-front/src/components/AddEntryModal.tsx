import { useState } from "react";
import { EntryService } from "../api/entryService";

type Props = {
  open: boolean;
  onClose: () => void;
  onSaved?: () => void;
};

export default function AddEntryModal({
  open,
  onClose,
  onSaved,
}: Props) {
  const [code, setCode] = useState("");
  const [quantity, setQuantity] = useState("");

  async function handleSave() {
    await EntryService.setApi(code, Number(quantity));
    onSaved?.();
    onClose();
  }

  if (!open) return null;

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center">
      <div className="bg-white p-6 rounded-3xl w-full max-w-md shadow-2xl">
        <div className="flex justify-between items-center mb-4">
          <h2 className="text-2xl font-bold">
            Add Entry
          </h2>

          <button
            onClick={onClose}
            className="text-gray-500 hover:text-black">
            X
          </button>
        </div>

        <input
          placeholder="Product name"
          value={code}
          onChange={(e) => setCode(e.target.value)}
          className="w-full border rounded-2xl p-3 mb-4"
        />
        
        <input
          placeholder="Quantity"
          value={quantity}
          onChange={(e) => setQuantity(e.target.value)}
          className="w-full border rounded-2xl p-3 mb-4"
        />
        <button onClick={handleSave} className="w-full bg-black text-white rounded-2xl p-3">
          Save
        </button>
      </div>
    </div>
  );
}