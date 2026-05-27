import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from './assets/vite.svg'
import heroImg from './assets/hero.png'
import './App.css'
import LoginPage from './pages/LoginPage'
import {InitialPage} from './pages/InitialPage'
import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";
import { AddEntryPage } from './pages/AddEntryPage'

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/"
          element={<LoginPage />}
        />

        <Route
          path="/home"
          element={<InitialPage />}
        />
        <Route
          path="/add-entry"
          element={<AddEntryPage />}
        />
      </Routes>
    </BrowserRouter>
  );
}