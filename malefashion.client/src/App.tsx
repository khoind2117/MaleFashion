import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import routes from "../routes/Routes";
import ScrollToTop from "./components/ui/ScrollToTop";

const App: React.FC = () => {
  return (
    <Router>
      <ScrollToTop />
      <Routes>
        {routes.map((route, index) => (
          <Route key={index} path={route.path} element={route.element} />
        ))}
      </Routes>
    </Router>
  );
};

export default App;
