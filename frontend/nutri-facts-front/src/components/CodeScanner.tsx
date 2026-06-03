import { useEffect, useState } from "react";

import {
  Html5QrcodeScanner,
  Html5QrcodeSupportedFormats,
} from "html5-qrcode";

type Props = {
  onScan: (barcode: string) => void | Promise<void>;
};

export default function BarcodeScanner({
  onScan,
}: Props) {
  const [manualBarcode, setManualBarcode] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function submitBarcode(rawBarcode: string) {
    const barcode = rawBarcode.trim();
    if (!barcode) {
      return;
    }

    setIsSubmitting(true);
    try {
      await onScan(barcode);
      setManualBarcode("");
    } finally {
      setIsSubmitting(false);
    }
  }

  function handleManualSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    void submitBarcode(manualBarcode);
  }

  useEffect(() => {
    const scanner =
      new Html5QrcodeScanner(
        "scanner",
        {
          fps: 10,
          qrbox: {
            width: 250,
            height: 120,
          }, 
          formatsToSupport: [
            Html5QrcodeSupportedFormats.EAN_13,
            Html5QrcodeSupportedFormats.UPC_A,
            Html5QrcodeSupportedFormats.UPC_E,
          ],
    },    
   
        false
      );

    scanner.render(
      (decodedText) => {
        void submitBarcode(decodedText).finally(() => {
          scanner.clear().catch(console.error);
        });
      },

      (error) => {
        console.log(error);
      }
    );

    return () => {
      scanner.clear().catch(console.error);
    };
  }, []);

  return (
    <div className="space-y-3">
      <form onSubmit={handleManualSubmit} className="flex gap-2">
        <input
          type="text"
          inputMode="numeric"
          placeholder="Type barcode"
          value={manualBarcode}
          onChange={(event) => setManualBarcode(event.target.value)}
          className="w-full border rounded-2xl p-3"
        />
        <button
          type="submit"
          disabled={isSubmitting || !manualBarcode.trim()}
          className="bg-black text-white rounded-2xl px-4 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          {isSubmitting ? "Searching..." : "Search"}
        </button>
      </form>

      <div
        id="scanner"
        className="w-full"
      />
    </div>
  );
}