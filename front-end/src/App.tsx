import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { Route, Routes } from "react-router-dom";
import { Fridge } from "./routes/fridge";
import { Info } from "./routes/info";
import { Login } from "./routes/login";
import { BarcodeTest } from "./routes/barcodeTest";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route path="/fridge" element={<Fridge />} />
      <Route path="/info" element={<Info />} />
      <Route path="/barcode-test" element={<BarcodeTest />} />
    </Routes>
  );
}

export default App;
