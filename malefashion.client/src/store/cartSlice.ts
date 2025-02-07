import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { CartItem } from "../models/dtos/CartItem/CartItem";

interface CartState {
  items: CartItem[];
}

const loadCartFromLocalStorage = (): CartState => {
  const storedCart = localStorage.getItem("cart");
  return storedCart ? JSON.parse(storedCart) : { items: [] };
};

const initialState: CartState = loadCartFromLocalStorage();

const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addToCart(state, action: PayloadAction<CartItem>) {
      const existingItem = state.items.find(
        (item) =>
          item.productVariantId === action.payload.productVariantId
      );

      if (existingItem) {
        existingItem.quantity += action.payload.quantity;
      } else {
        state.items.push(action.payload);
      }

      localStorage.setItem("cart", JSON.stringify(state));
    },
    removeFromCart(state, action: PayloadAction<number>) {
      state.items = state.items.filter(item => item.productVariantId !== action.payload);

      localStorage.setItem("cart", JSON.stringify(state));
    },
    updateQuantity(state, action: PayloadAction<{productVariantId: number, newQuantity: number}>) {
      const { productVariantId, newQuantity } = action.payload;
      const item = state.items.find(item => item.productVariantId === productVariantId);
      if (item) {
        item.quantity = newQuantity;
      }

      localStorage.setItem("cart", JSON.stringify(state));
    },
  },
});

export const { addToCart, removeFromCart, updateQuantity } = cartSlice.actions;
export default cartSlice.reducer;
