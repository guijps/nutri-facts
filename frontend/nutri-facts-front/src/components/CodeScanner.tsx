import { useEffect } from "react";

import {
  Html5QrcodeScanner,
  Html5QrcodeSupportedFormats,
} from "html5-qrcode";

type Props = {
  onScan: (barcode: string) => void;
};

export default function BarcodeScanner({
  onScan,
}: Props) {
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
        onScan(decodedText);

        scanner.clear();
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
    <div
      id="scanner"
      className="w-full"
    />
  );
}