import {  useState } from "react";
import AddEntryModal from "../components/AddEntryModal";
import { EntryTable } from "../components/EntryTable";
import { GoalTable } from "../components/GoalTable";
import BarcodeScanner from "../components/CodeScanner";
import { useNavigate } from "react-router-dom";



export function InitialPage() {
    const navigate = useNavigate();
    const [open, setOpen] = useState(false);
    const [refreshKey, setRefreshKey] = useState(0);

 function handleScan(barcode: string) {
    console.log(barcode);

  }

  return (
    <div className="max-w-2xl mx-auto p-6">
      <h1 className="text-2xl font-bold mb-6">Today's Entries</h1>
      <GoalTable refreshKey={refreshKey} />
      <EntryTable refreshKey={refreshKey} onSaved={() => setRefreshKey((k) => k + 1)}/>
      
        <button
        onClick={() => {navigate("/add-entry")}}
        className="bg-black text-white px-5 py-3 rounded-2xl"
      >
        Add Entry
      </button>
    </div>
    
  );
}
