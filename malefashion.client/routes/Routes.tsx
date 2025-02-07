import React from "react";
import HomePage from "../src/pages/client/Home/HomePage";
import ShopPage from "../src/pages/client/Shop/ShopPage";
import ProductDetailPage from "../src/pages/client/Product/ProductDetailPage";
import CartPage from "../src/pages/client/Cart/CartPage";
import CheckoutPage from "../src/pages/client/Checkout/CheckoutPage";
import Login from "../src/pages/auth/Login";
import Register from "../src/pages/auth/Register";

export interface Route {
  path: string;
  element: React.ReactNode;
}

const routes: Route[] = [
  { path: "/", element: <HomePage /> },
  { path: "/login", element: <Login/> },
  { path: "/register", element: <Register/> },
  { path: "/shop", element: <ShopPage/> },
  { path: "/product/:productId", element: <ProductDetailPage/> },
  { path: "/cart", element: <CartPage/> },
  { path: "/checkout", element: <CheckoutPage/> },
  
];

export default routes;
  