import { Routes, Route } from "react-router-dom";
import Builder from "./pages/Builder";
import Dashboard from "./pages/Dashboard";
import Home from "./pages/Home";
import Layout from "./pages/Layout/Layout";

interface RouteInfo {
  name: string;
  path: string;
}

export const publicRoutes: RouteInfo[] = [
  {
    name: "Home",
    path: "/",
  },
  {
    name: "Dashboard",
    path: "/dashboard",
  },
  {
    name: "Builder",
    path: "/builder",
  },
];

const AppRoutes: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<Home />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/builder" element={<Builder />} />
      </Route>
    </Routes>
  );
};

export default AppRoutes;
