import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import routes from "../routes/Routes";
import ScrollToTop from "./components/ui/ScrollToTop";
import { AuthProvider } from "./context/AuthContext";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const App: React.FC = () => {
  return (
    <Router>
      <AuthProvider>
        <ScrollToTop />
        <ToastContainer aria-label={undefined} />
        <Routes>
          {routes.map((route, index) => (
            <Route key={index} path={route.path} element={route.element} />
          ))}
        </Routes>
      </AuthProvider>
    </Router>
  );
};

export default App;
