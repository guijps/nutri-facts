import { useEffect, useState } from "react";
import { api } from "../api/api";
import { InformationWindow } from "./InformationWindow";
import type { NutritionFacts, ProductEntry } from "../types/product";

const DAILY_GOAL: ProductEntry = {
  id: "goal",
  product: { name: "Daily Goal", nutritionFacts: { calories: 2000, carbohydrates: 250, fat: 65, protein: 50 } },
  quantity: 0,
  nutritionFacts: { calories: 2000, carbohydrates: 250, fat: 65, protein: 50 },
};

function buildTodayEntry(facts: NutritionFacts): ProductEntry {
  return {
    id: "today",
    product: { name: "Today's Total", nutritionFacts: facts },
    quantity: 0,
    nutritionFacts: facts,
  };
}

export function GoalTable({ refreshKey }: { refreshKey?: number }) {
  const [todayEntry, setTodayEntry] = useState<ProductEntry | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    api("/all-facts")
      .then((res) => {
        if (!res.ok) throw new Error("Failed to fetch facts");
        return res.json();
      })
      .then((facts: NutritionFacts) => setTodayEntry(buildTodayEntry(facts)))
      .catch((err) => setError(err.message));
  }, [refreshKey]);

  if (error) return <p className="text-red-500 text-sm">{error}</p>;
  if (!todayEntry) return null;

  return <InformationWindow first={todayEntry} second={DAILY_GOAL} />;
}
