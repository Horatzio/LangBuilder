import React from "react";
import { Routes, Route } from "react-router-dom";
import "./App.css";
import Layout from "./pages/Layout";
import Home from "./pages/Home";
import Dashboard from "./pages/Dashboard";
import Builder from "./pages/Builder";

export const App: React.FunctionComponent = () => {
  return (
    <>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/builder" element={<Builder />} />
        </Route>
      </Routes>
    </>
  );
};
