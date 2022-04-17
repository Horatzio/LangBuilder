import { Outlet } from "react-router-dom";
import Navigation from "../Layout/Navigation";
import Footer from "./Footer";

export default function Example() {
  return (
    <>
      <Navigation />
      <div className="min-h-full">
        <main>
          <div className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
            <Outlet />
          </div>
        </main>
      </div>
      <Footer />
    </>
  );
}
