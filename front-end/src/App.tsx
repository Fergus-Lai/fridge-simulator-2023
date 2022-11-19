import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { Route, Routes } from "react-router-dom";
import { Fridge } from "./routes/fridge";
import { Info } from "./routes/info";

function App() {
  return (
    <Routes>
      <Route
        path="/"
        loader={(params) => {
          return null;
        }}
        element={<Fridge />}
      />
      <Route path="/info" element={<Info />} />
    </Routes>
  );
}

export default App;
